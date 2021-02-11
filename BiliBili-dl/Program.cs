using HeyRed.Mime;
using Newtonsoft.Json;
using Penguin.Extensions.Strings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

//Package Source: http://nuget.penguinms.com/v3/index.json
namespace BiliBili_dl
{
    internal class Program
    {
        /// <summary>
        /// Start of json object for player info in page source
        /// </summary>
        private const string PlayerInfoStart = "window.__playinfo__=";

        /// <summary>
        /// Start of window state (playlist) info in page source
        /// </summary>
        private const string InitialStateStart = "window.__INITIAL_STATE__=";

        /// <summary>
        /// End of window state (playlist) info in page source
        /// </summary>
        private const string InitialStateEnd = ";(function(){var s;(s=document.currentScript||document.scripts[document.scripts.length-1]).parentNode.removeChild(s);}());";

        /// <summary>
        /// Path to FFMPEG for mixing audio and video streams after download
        /// </summary>
        private const string FFMPEG_PATH = "ffmpeg.exe";

        /// <summary>
        /// Path that files will be downloaded to
        /// </summary>
        private const string DownloadPath = "Downloads";

        /// <summary>
        /// User agent used when pulling data from server
        /// </summary>
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36";

        private static readonly WebClient Client = new();

        /// <summary>
        /// Sets header and returns un-gziped page source.
        /// </summary>
        /// <param name="url">Page to download source from</param>
        /// <returns></returns>
        private static string Request(string url)
        {
            Client.Headers["User-Agent"] = UserAgent;

            GZipStream responseStream = new(Client.OpenRead(url), CompressionMode.Decompress);
            StreamReader reader = new(responseStream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Takes Path.Combine parts and appends standard (int) to file name until free path is found
        /// </summary>
        /// <param name="pathParts">Parameters for Path.Combine used to build file path</param>
        /// <returns>A file Info representing a free path found for the file</returns>
        public static FileInfo FindFreeFile(params string[] pathParts)
        {
            //Obvious first try
            FileInfo attemptedFile = new(Path.Combine(pathParts));

            string dir = attemptedFile.DirectoryName;

            //If the directory doesn't exist create it,
            //and return since we know theres no file there if we
            //Just created the dir
            if (!attemptedFile.Directory.Exists)
            {
                attemptedFile.Directory.Create();
                return attemptedFile;
            }

            string fName = Path.GetFileNameWithoutExtension(attemptedFile.FullName);
            string ext = Path.GetExtension(attemptedFile.FullName).Trim('.');

            int tryNum = 0;

            //As long as the file exists, bump the int and check again
            while (attemptedFile.Exists)
            {
                tryNum++;

                attemptedFile = new FileInfo(Path.Combine(dir, $"{fName} ({tryNum}).{ext}"));
            }

            return attemptedFile;
        }

        /// <summary>
        /// Downloads a file from a url, to a specified target.
        /// Attempts to resume the download if the connection is broken
        /// </summary>
        /// <param name="url">The url to download the file from</param>
        /// <param name="target">The FileInfo target to download the file to</param>
        private static async Task SafeDownload(string url, FileInfo target)
        {
            //Open a write stream for the file
            using FileStream outputStream = target.OpenWrite();
            WebResponse response = null;

            do
            {
                try
                {
                    //Create a request for the Media
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                    //Tricky bit. If the position isn't 0 then we've already written data, which means its not our first request for the file.
                    //If this is the case then the last loop was disconnected during download, so we're going to set the Range header for the
                    //Request to attempted to start where the last download failed
                    if (outputStream.Position != 0)
                    {
                        //Should this be + 1?                       //Use the content length specified in the last request
                        request.AddRange(outputStream.Position + 1, long.Parse(response.Headers["Content-Length"]));
                    }

                    request.Headers["User-Agent"] = UserAgent;

                    response = await request.GetResponseAsync();

                    using Stream stream = response.GetResponseStream();

                    //Copy to the file output stream
                    stream.CopyTo(outputStream);

                    response.Dispose();

                    //If we made it this far, then we didn't disconnect and its safe to break the loop
                    break;
                }
                catch (IOException ex) when (ex.Message.Contains("The response ended prematurely"))
                {
                    //If we hit this line, then we're not breaking and were going to loop back around for the request again
                    Console.WriteLine($"Connection dropped with {outputStream.Position}/{response.Headers["Content-Length"]} bytes downloaded. Attempting continue...");
                }
            } while (true);
        }

        /// <summary>
        /// Downloads the highest bitrate file specified in a Media IEnumerable to a file specified by the title.
        /// File Extension is determined by the MimeType and should be guaranteed to be compatible as a result
        /// </summary>
        /// <param name="source">IEnumerable of Media Definitions retrieved from BiliBili page</param>
        /// <param name="title">The title of the video</param>
        /// <returns>A task representing the return of the absolute path of the downloaded file</returns>
        private static async Task<string> DownloadBest(IEnumerable<Media> source, string title)
        {
            //Grab highest bitrate source
            Console.WriteLine("Finding best media...");
            Media bestMedia = source.OrderByDescending(a => a.Bandwidth).First();

            Console.WriteLine("Url: " + bestMedia.BaseUrl);
            Console.WriteLine("MimeType: " + bestMedia.MimeType);

            //Find a free place to put the file
            FileInfo targetFile = FindFreeFile(DownloadPath, $"{title}.{MimeTypesMap.GetExtension(bestMedia.MimeType)}");

            Console.WriteLine("Downloading media...");

            await SafeDownload(bestMedia.BaseUrl, targetFile);

            Console.WriteLine($"{bestMedia.MimeType} downloaded to: " + targetFile.FullName);

            return targetFile.FullName;
        }

        /// <summary>
        /// Runs a command line process and returns the exit code
        /// </summary>
        /// <param name="fName">The process to run</param>
        /// <param name="args">The arguments to pass to the process</param>
        /// <returns>The exit code for the process</returns>
        private static int RunProcess(string fName, string args)
        {
            ProcessStartInfo startInfo = new(fName)
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = args
            };

            Process p = Process.Start(startInfo);

            p.WaitForExit();

            return p.ExitCode;
        }

        /// <summary>
        /// Merges an audio and video file into a single file containing both
        /// </summary>
        /// <param name="audioPath">The path to the audio file</param>
        /// <param name="videoPath">The path to the video file</param>
        private static void MergeMedia(string audioPath, string videoPath)
        {
            Console.WriteLine("Merging audio and video...");

            string tempOutput = Path.Combine(new FileInfo(videoPath).Directory.FullName, $"{Path.GetFileNameWithoutExtension(videoPath)}_Temp{Path.GetExtension(videoPath)}");

            if (RunProcess(FFMPEG_PATH, $"-i \"{videoPath}\" -i \"{audioPath}\" -c:v copy -c:a aac \"{tempOutput}\"") == 0)
            {
                //Clean up the individual audio/video files and move the new output over the original video file.
                //The video file should be MP4 which supports both audio and video so its the obvious option
                Console.WriteLine("Cleaning up merge...");
                File.Delete(audioPath);
                File.Delete(videoPath);
                File.Move(tempOutput, videoPath);
            }
            else
            {
                //No idea what happened, so we bail completely
                Console.WriteLine("Something went wrong during the merge.");
                Console.WriteLine("Press any key to exit");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Returns a deserialized object from the given source code as delimited by start and end strings
        /// </summary>
        /// <typeparam name="TReturn">The deserialized type to return</typeparam>
        /// <param name="source">The page source the object is being extracted from</param>
        /// <param name="start">The non-inclusive start delimiter of the object</param>
        /// <param name="end">The non-inclusive end delimiter of the object</param>
        /// <returns>A Deserialized version of the object as extracted from the source</returns>
        private static TReturn ExtractObject<TReturn>(string source, string start, string end = "</script>")
        {
            //Snip out the json
            string jsonData = source.From(start).To(end);

            Console.WriteLine("Parsed Json: " + jsonData);

            //Deserialize
            return JsonConvert.DeserializeObject<TReturn>(jsonData);
        }

        private static async Task Main(string[] args)
        {
            //Loop through each playlist
            foreach (string url in args)
            {
                Console.WriteLine("Checking playlist: " + url);

                int page = 1;
                int? maxPage = null;

                string requestUrl = url;

                InitialState playlistInfo = new InitialState();

                do
                {
                    Console.WriteLine("Downloading source for: " + requestUrl);

                    string pageSource = Request(requestUrl);

                    //Grab the playlist information if we haven't already
                    playlistInfo ??= ExtractObject<InitialState>(pageSource, InitialStateStart, InitialStateEnd);

                    //Grab information relevant to this video stream
                    PlayerInfo playerInfo = ExtractObject<PlayerInfo>(pageSource, PlayerInfoStart);

                    //Use the title as specified in the playlist
                    string title = playlistInfo.VideoData.Pages[page - 1].Part;

                    //Download the best audio
                    Task<string> audioFilePath = DownloadBest(playerInfo.Data.Dash.Audio, title);

                    //Download the best video
                    Task<string> videoFilePath = DownloadBest(playerInfo.Data.Dash.Video, title);

                    //Merge the audio and the video streams
                    MergeMedia(await audioFilePath, await videoFilePath);

                    //Bump the page
                    page++;

                    //update the next request URL
                    requestUrl = $"{url}?p={page}";

                    //And continue, as long as we have something to continue to
                } while (maxPage != null && page <= playlistInfo.VideoData.Pages.Count);
            }
        }
    }
}