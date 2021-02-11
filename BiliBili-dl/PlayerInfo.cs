using Newtonsoft.Json;
using System.Collections.Generic;

namespace BiliBili_dl
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SegmentBase
    {
        [JsonProperty("Initialization")]
        public string Initialization { get; set; }

        [JsonProperty("indexRange")]
        public string IndexRange { get; set; }
    }

    public class SegmentBase2
    {
        [JsonProperty("initialization")]
        public string Initialization { get; set; }

        [JsonProperty("index_range")]
        public string IndexRange { get; set; }
    }

    public class Media
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("base_url")]
        public string Base_Url { get; set; }

        [JsonProperty("backupUrl")]
        public object BackupUrl { get; set; }

        [JsonProperty("backup_url")]
        public object Backup_Url { get; set; }

        [JsonProperty("bandwidth")]
        public int Bandwidth { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("mime_type")]
        public string Mime_Type { get; set; }

        [JsonProperty("codecs")]
        public string Codecs { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("frameRate")]
        public string FrameRate { get; set; }

        [JsonProperty("frame_rate")]
        public string Frame_Rate { get; set; }

        [JsonProperty("sar")]
        public string Sar { get; set; }

        [JsonProperty("startWithSap")]
        public int StartWithSap { get; set; }

        [JsonProperty("start_with_sap")]
        public int Start_With_Sap { get; set; }

        [JsonProperty("SegmentBase")]
        public SegmentBase SegmentBase { get; set; }

        [JsonProperty("segment_base")]
        public SegmentBase Segment_Base { get; set; }

        [JsonProperty("codecid")]
        public int Codecid { get; set; }
    }

    public class Dash
    {
        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("minBufferTime")]
        public double MinBufferTime { get; set; }

        [JsonProperty("min_buffer_time")]
        public double Min_Buffer_Time { get; set; }

        [JsonProperty("video")]
        public List<Media> Video { get; set; }

        [JsonProperty("audio")]
        public List<Media> Audio { get; set; }
    }

    public class SupportFormat
    {
        [JsonProperty("quality")]
        public int Quality { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("new_description")]
        public string NewDescription { get; set; }

        [JsonProperty("display_desc")]
        public string DisplayDesc { get; set; }

        [JsonProperty("superscript")]
        public string Superscript { get; set; }
    }

    public class Data
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("quality")]
        public int Quality { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("timelength")]
        public int Timelength { get; set; }

        [JsonProperty("accept_format")]
        public string AcceptFormat { get; set; }

        [JsonProperty("accept_description")]
        public List<string> AcceptDescription { get; set; }

        [JsonProperty("accept_quality")]
        public List<int> AcceptQuality { get; set; }

        [JsonProperty("video_codecid")]
        public int VideoCodecid { get; set; }

        [JsonProperty("seek_param")]
        public string SeekParam { get; set; }

        [JsonProperty("seek_type")]
        public string SeekType { get; set; }

        [JsonProperty("dash")]
        public Dash Dash { get; set; }

        [JsonProperty("support_formats")]
        public List<SupportFormat> SupportFormats { get; set; }
    }

    public class PlayerInfo
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("session")]
        public string Session { get; set; }
    }
}