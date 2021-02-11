using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliBili_dl
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Rights
    {
        [JsonProperty("bp")]
        public int Bp { get; set; }

        [JsonProperty("elec")]
        public int Elec { get; set; }

        [JsonProperty("download")]
        public int Download { get; set; }

        [JsonProperty("movie")]
        public int Movie { get; set; }

        [JsonProperty("pay")]
        public int Pay { get; set; }

        [JsonProperty("hd5")]
        public int Hd5 { get; set; }

        [JsonProperty("no_reprint")]
        public int NoReprint { get; set; }

        [JsonProperty("autoplay")]
        public int Autoplay { get; set; }

        [JsonProperty("ugc_pay")]
        public int UgcPay { get; set; }

        [JsonProperty("is_cooperation")]
        public int IsCooperation { get; set; }

        [JsonProperty("ugc_pay_preview")]
        public int UgcPayPreview { get; set; }

        [JsonProperty("no_background")]
        public int NoBackground { get; set; }

        [JsonProperty("clean_mode")]
        public int CleanMode { get; set; }

        [JsonProperty("is_stein_gate")]
        public int IsSteinGate { get; set; }
    }

    public class Owner
    {
        [JsonProperty("mid")]
        public int Mid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("face")]
        public string Face { get; set; }
    }

    public class Stat
    {
        [JsonProperty("aid")]
        public int Aid { get; set; }

        [JsonProperty("view")]
        public int View { get; set; }

        [JsonProperty("danmaku")]
        public int Danmaku { get; set; }

        [JsonProperty("reply")]
        public int Reply { get; set; }

        [JsonProperty("favorite")]
        public int Favorite { get; set; }

        [JsonProperty("coin")]
        public int Coin { get; set; }

        [JsonProperty("share")]
        public int Share { get; set; }

        [JsonProperty("now_rank")]
        public int NowRank { get; set; }

        [JsonProperty("his_rank")]
        public int HisRank { get; set; }

        [JsonProperty("like")]
        public int Like { get; set; }

        [JsonProperty("dislike")]
        public int Dislike { get; set; }

        [JsonProperty("evaluation")]
        public string Evaluation { get; set; }

        [JsonProperty("argue_msg")]
        public string ArgueMsg { get; set; }

        [JsonProperty("viewseo")]
        public int Viewseo { get; set; }
    }

    public class Dimension
    {
        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("rotate")]
        public int Rotate { get; set; }
    }

    public class Page
    {
        [JsonProperty("cid")]
        public int Cid { get; set; }

        [JsonProperty("page")]
        public int PageNum { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("part")]
        public string Part { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("vid")]
        public string Vid { get; set; }

        [JsonProperty("weblink")]
        public string Weblink { get; set; }

        [JsonProperty("dimension")]
        public Dimension Dimension { get; set; }
    }

    public class Subtitle
    {
        [JsonProperty("allow_submit")]
        public bool AllowSubmit { get; set; }

        [JsonProperty("list")]
        public List<object> List { get; set; }
    }

    public class UserGarb
    {
        [JsonProperty("url_image_ani_cut")]
        public string UrlImageAniCut { get; set; }
    }

    public class VideoData
    {
        [JsonProperty("bvid")]
        public string Bvid { get; set; }

        [JsonProperty("aid")]
        public int Aid { get; set; }

        [JsonProperty("videos")]
        public int Videos { get; set; }

        [JsonProperty("tid")]
        public int Tid { get; set; }

        [JsonProperty("tname")]
        public string Tname { get; set; }

        [JsonProperty("copyright")]
        public int Copyright { get; set; }

        [JsonProperty("pic")]
        public string Pic { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("pubdate")]
        public int Pubdate { get; set; }

        [JsonProperty("ctime")]
        public int Ctime { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("rights")]
        public Rights Rights { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("stat")]
        public Stat Stat { get; set; }

        [JsonProperty("dynamic")]
        public string Dynamic { get; set; }

        [JsonProperty("cid")]
        public int Cid { get; set; }

        [JsonProperty("dimension")]
        public Dimension Dimension { get; set; }

        [JsonProperty("no_cache")]
        public bool NoCache { get; set; }

        [JsonProperty("pages")]
        public List<Page> Pages { get; set; }

        [JsonProperty("subtitle")]
        public Subtitle Subtitle { get; set; }

        [JsonProperty("user_garb")]
        public UserGarb UserGarb { get; set; }

        [JsonProperty("embedPlayer")]
        public string EmbedPlayer { get; set; }
    }

    public class LevelInfo
    {
        [JsonProperty("current_level")]
        public int CurrentLevel { get; set; }

        [JsonProperty("current_min")]
        public int CurrentMin { get; set; }

        [JsonProperty("current_exp")]
        public int CurrentExp { get; set; }

        [JsonProperty("next_exp")]
        public int NextExp { get; set; }
    }

    public class Pendant
    {
        [JsonProperty("pid")]
        public int Pid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("expire")]
        public int Expire { get; set; }

        [JsonProperty("image_enhance")]
        public string ImageEnhance { get; set; }

        [JsonProperty("image_enhance_frame")]
        public string ImageEnhanceFrame { get; set; }
    }

    public class Nameplate
    {
        [JsonProperty("nid")]
        public int Nid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("image_small")]
        public string ImageSmall { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("condition")]
        public string Condition { get; set; }
    }

    public class Official
    {
        [JsonProperty("role")]
        public int Role { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }
    }

    public class OfficialVerify
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }
    }

    public class Vip
    {
        [JsonProperty("vipType")]
        public int VipType { get; set; }

        [JsonProperty("dueRemark")]
        public string DueRemark { get; set; }

        [JsonProperty("accessStatus")]
        public int AccessStatus { get; set; }

        [JsonProperty("vipStatus")]
        public int VipStatus { get; set; }

        [JsonProperty("vipStatusWarn")]
        public string VipStatusWarn { get; set; }

        [JsonProperty("theme_type")]
        public int ThemeType { get; set; }
    }

    public class UpData
    {
        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("approve")]
        public bool Approve { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("face")]
        public string Face { get; set; }

        [JsonProperty("DisplayRank")]
        public string DisplayRank { get; set; }

        [JsonProperty("regtime")]
        public int Regtime { get; set; }

        [JsonProperty("spacesta")]
        public int Spacesta { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("article")]
        public int Article { get; set; }

        [JsonProperty("attentions")]
        public List<object> Attentions { get; set; }

        [JsonProperty("fans")]
        public int Fans { get; set; }

        [JsonProperty("friend")]
        public int Friend { get; set; }

        [JsonProperty("attention")]
        public int Attention { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("level_info")]
        public LevelInfo LevelInfo { get; set; }

        [JsonProperty("pendant")]
        public Pendant Pendant { get; set; }

        [JsonProperty("nameplate")]
        public Nameplate Nameplate { get; set; }

        [JsonProperty("Official")]
        public Official Official { get; set; }

        [JsonProperty("official_verify")]
        public OfficialVerify OfficialVerify { get; set; }

        [JsonProperty("vip")]
        public Vip Vip { get; set; }

        [JsonProperty("archiveCount")]
        public int ArchiveCount { get; set; }
    }

    public class Count
    {
        [JsonProperty("view")]
        public int View { get; set; }

        [JsonProperty("use")]
        public int Use { get; set; }

        [JsonProperty("atten")]
        public int Atten { get; set; }
    }

    public class Tag
    {
        [JsonProperty("tag_id")]
        public int TagId { get; set; }

        [JsonProperty("tag_name")]
        public string TagName { get; set; }

        [JsonProperty("cover")]
        public string Cover { get; set; }

        [JsonProperty("head_cover")]
        public string HeadCover { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("short_content")]
        public string ShortContent { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("ctime")]
        public int Ctime { get; set; }

        [JsonProperty("count")]
        public Count Count { get; set; }

        [JsonProperty("is_atten")]
        public int IsAtten { get; set; }

        [JsonProperty("likes")]
        public int Likes { get; set; }

        [JsonProperty("hates")]
        public int Hates { get; set; }

        [JsonProperty("attribute")]
        public int Attribute { get; set; }

        [JsonProperty("liked")]
        public int Liked { get; set; }

        [JsonProperty("hated")]
        public int Hated { get; set; }

        [JsonProperty("extra_attr")]
        public int ExtraAttr { get; set; }

        [JsonProperty("tag_type")]
        public string TagType { get; set; }

        [JsonProperty("is_activity")]
        public bool IsActivity { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("alpha")]
        public int Alpha { get; set; }

        [JsonProperty("is_season")]
        public bool IsSeason { get; set; }

        [JsonProperty("subscribed_count")]
        public int SubscribedCount { get; set; }

        [JsonProperty("archive_count")]
        public string ArchiveCount { get; set; }

        [JsonProperty("featured_count")]
        public int FeaturedCount { get; set; }

        [JsonProperty("showDetail")]
        public bool ShowDetail { get; set; }

        [JsonProperty("showReport")]
        public bool ShowReport { get; set; }

        [JsonProperty("timeOut")]
        public object TimeOut { get; set; }
    }

    public class Related
    {
        [JsonProperty("aid")]
        public int Aid { get; set; }

        [JsonProperty("cid")]
        public int Cid { get; set; }

        [JsonProperty("bvid")]
        public string Bvid { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("pic")]
        public string Pic { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("stat")]
        public Stat Stat { get; set; }
    }

    public class Error
    {
    }

    public class Playurl
    {
    }

    public class User
    {
    }

    public class Cids
    {
        [JsonProperty("1")]
        public int _1 { get; set; }
    }

    public class _584628034
    {
        [JsonProperty("aid")]
        public int Aid { get; set; }

        [JsonProperty("bvid")]
        public string Bvid { get; set; }

        [JsonProperty("cids")]
        public Cids Cids { get; set; }
    }

    public class BV1Vz4y1Z7ED
    {
        [JsonProperty("aid")]
        public int Aid { get; set; }

        [JsonProperty("bvid")]
        public string Bvid { get; set; }

        [JsonProperty("cids")]
        public Cids Cids { get; set; }
    }

    public class CidMap
    {
        [JsonProperty("584628034")]
        public _584628034 _584628034 { get; set; }

        [JsonProperty("BV1Vz4y1Z7ED")]
        public BV1Vz4y1Z7ED BV1Vz4y1Z7ED { get; set; }
    }

    public class OtherData
    {
        [JsonProperty("pic")]
        public string Pic { get; set; }

        [JsonProperty("show")]
        public bool Show { get; set; }
    }

    public class ElecFullInfo
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("data")]
        public OtherData Data { get; set; }
    }

    public class EmbeddedData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("contract_id")]
        public string ContractId { get; set; }

        [JsonProperty("pos_num")]
        public int PosNum { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pic")]
        public string Pic { get; set; }

        [JsonProperty("litpic")]
        public string Litpic { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("style")]
        public int Style { get; set; }

        [JsonProperty("agency")]
        public string Agency { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("creative_type")]
        public int CreativeType { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("src_id")]
        public int SrcId { get; set; }

        [JsonProperty("area")]
        public int Area { get; set; }

        [JsonProperty("is_ad_loc")]
        public bool IsAdLoc { get; set; }

        [JsonProperty("ad_cb")]
        public string AdCb { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("server_type")]
        public int ServerType { get; set; }

        [JsonProperty("cm_mark")]
        public int CmMark { get; set; }

        [JsonProperty("stime")]
        public int Stime { get; set; }

        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("activity_type")]
        public int ActivityType { get; set; }

        [JsonProperty("epid")]
        public int Epid { get; set; }

        [JsonProperty("season")]
        public object Season { get; set; }

        [JsonProperty("room")]
        public object Room { get; set; }

        [JsonProperty("sub_title")]
        public string SubTitle { get; set; }

        [JsonProperty("ad_desc")]
        public string AdDesc { get; set; }

        [JsonProperty("adver_name")]
        public string AdverName { get; set; }

        [JsonProperty("null_frame")]
        public bool NullFrame { get; set; }
    }

    public class BofqiParams
    {
    }

    public class InitialState
    {
        [JsonProperty("aid")]
        public int Aid { get; set; }

        [JsonProperty("bvid")]
        public string Bvid { get; set; }

        [JsonProperty("p")]
        public int P { get; set; }

        [JsonProperty("episode")]
        public string Episode { get; set; }

        [JsonProperty("videoData")]
        public VideoData VideoData { get; set; }

        [JsonProperty("upData")]
        public UpData UpData { get; set; }

        [JsonProperty("staffData")]
        public List<object> StaffData { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("related")]
        public List<Related> Related { get; set; }

        [JsonProperty("spec")]
        public object Spec { get; set; }

        [JsonProperty("isClient")]
        public bool IsClient { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }

        [JsonProperty("player")]
        public string Player { get; set; }

        [JsonProperty("playurl")]
        public Playurl Playurl { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("cidMap")]
        public CidMap CidMap { get; set; }

        [JsonProperty("isRecAutoPlay")]
        public string IsRecAutoPlay { get; set; }

        [JsonProperty("autoPlayNextVideo")]
        public object AutoPlayNextVideo { get; set; }

        [JsonProperty("elecFullInfo")]
        public ElecFullInfo ElecFullInfo { get; set; }

        [JsonProperty("adData")]
        public Dictionary<string, List<EmbeddedData>> AdData { get; set; }

        [JsonProperty("bofqiParams")]
        public BofqiParams BofqiParams { get; set; }

        [JsonProperty("insertScripts")]
        public List<string> InsertScripts { get; set; }
    }


}
