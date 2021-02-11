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

namespace BiliBili_dl
{
    internal class Program
    {
        private const string PlayerInfoStart = "window.__playinfo__=";
        private const string InitialStateStart = "window.__INITIAL_STATE__=";
        private const string InitialStateEnd = ";(function(){var s;(s=document.currentScript||document.scripts[document.scripts.length-1]).parentNode.removeChild(s);}());";

        private const string FFMPEG_PATH = "ffmpeg.exe";

        private const string DownloadPath = "Downloads";

        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36";

        private static readonly WebClient Client = new();

        private static string Request(string url)
        {
            Client.Headers["User-Agent"] = UserAgent;

            GZipStream responseStream = new(Client.OpenRead(url), CompressionMode.Decompress);
            StreamReader reader = new(responseStream);
            return reader.ReadToEnd();
        }

        private static async Task<string> DownloadBest(IEnumerable<Media> source, string title)
        {
            Console.WriteLine("Finding best media...");

            Media bestMedia = source.OrderByDescending(a => a.Bandwidth).First();


            string url = bestMedia.BaseUrl;
            Console.WriteLine("Url: " + url);
            Console.WriteLine("MimeType: " + bestMedia.MimeType);

            string fileName = title;

            string fileExt = $"{MimeTypesMap.GetExtension(bestMedia.MimeType)}";

            Client.Headers["User-Agent"] = UserAgent;

            if (!Directory.Exists(DownloadPath))
            {
                Directory.CreateDirectory(DownloadPath);
            }

            Console.WriteLine("Downloading media...");


            string targetPath = Path.Combine(DownloadPath, $"{fileName}.{fileExt}");
            int tryNum = 0;
            while (File.Exists(targetPath))
            {
                tryNum++;

                targetPath = Path.Combine(DownloadPath, $"{fileName} ({tryNum}).{fileExt}");
            }

            byte[] data = Array.Empty<byte>();

            bool completed = false;

            WebResponse response = null;

            do
            {
                using MemoryStream ms = new();
                
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                    if(data.Length != 0)
                    {
                        request.AddRange(data.Length + 1, long.Parse(response.Headers["Content-Length"]));
                    }

                    request.Headers["User-Agent"] = UserAgent;

                    response = await request.GetResponseAsync();

                    using Stream stream = response.GetResponseStream();

                    stream.CopyTo(ms);

                    completed = true;

                    response.Dispose();

                }
                catch (IOException ex) when (ex.Message.Contains("The response ended prematurely"))
                {
                    Console.WriteLine($"Connection dropped with {data.Length + ms.Length}/{response.Headers["Content-Length"]} bytes downloaded. Attempting continue...");
                }

                byte[] newData = new byte[data.Length + ms.Length];

                int i = 0;

                for (; i < data.Length; i++)
                {
                    newData[i] = data[i];
                }

                ms.Seek(0, SeekOrigin.Begin);

                int b = 0;

                while ((b = ms.ReadByte()) != -1)
                {
                    newData[i++] = (byte)b;
                }

                data = newData;

            } while (!completed);

            Console.WriteLine("Download complete. Writing file...");

            File.WriteAllBytes(targetPath, data);

            Console.WriteLine($"{bestMedia.MimeType} downloaded to: " + targetPath);

            return new FileInfo(targetPath).FullName;
        }

        private static void MergeMedia(string audioPath, string videoPath)
        {
            Console.WriteLine("Merging audio and video...");

            string tempOutput = Path.Combine(new FileInfo(videoPath).Directory.FullName, $"{Path.GetFileNameWithoutExtension(videoPath)}_Temp{Path.GetExtension(videoPath)}");
            ProcessStartInfo startInfo = new(FFMPEG_PATH)
            {
                WindowStyle = ProcessWindowStyle.Hidden,

                Arguments = $"-i \"{videoPath}\" -i \"{audioPath}\" -c:v copy -c:a aac \"{tempOutput}\""
            };

            Process p = Process.Start(startInfo);

            p.WaitForExit();

            if (p.ExitCode != 0)
            {
                Console.WriteLine("Something went wrong during the merge.");
                Console.WriteLine("Press any key to exit");
                Environment.Exit(1);
            }

            Console.WriteLine("Cleaning up merge...");
            File.Delete(audioPath);
            File.Delete(videoPath);
            File.Move(tempOutput, videoPath);
        }

        private static TReturn ExtractObject<TReturn>(string source, string start, string end = "</script>")
        {
            string jsonData = source.From(start).To(end);
            Console.WriteLine("Parsed Json: " + jsonData);

            return JsonConvert.DeserializeObject<TReturn>(jsonData);
        }

        private static async Task Main(string[] args)
        {
            foreach (string url in args)
            {
                Console.WriteLine("Checking playlist: " + url);

                int page = 1;
                int? maxPage = null;

                string requestUrl = url;

                do
                {
                    Console.WriteLine("Downloading source for: " + requestUrl);

                    string pageSource = Request(requestUrl);

                    InitialState initialState = ExtractObject<InitialState>(pageSource, InitialStateStart, InitialStateEnd);

                    if (maxPage is null)
                    {
                        maxPage = initialState.VideoData.Pages.Count;
                    }


                    PlayerInfo playerInfo = ExtractObject<PlayerInfo>(pageSource, PlayerInfoStart);


                    string title = initialState.VideoData.Pages[page - 1].Part;

                    Task<string> audioFilePath = DownloadBest(playerInfo.Data.Dash.Audio, title);
                    Task<string> videoFilePath = DownloadBest(playerInfo.Data.Dash.Video, title);


                    MergeMedia(await audioFilePath, await videoFilePath);
                    //MergeMedia("", await videoFilePath);
                    page++;

                    requestUrl = $"{url}?p={page}";

                } while (maxPage != null && page <= maxPage);
            }
        }
    }
}
