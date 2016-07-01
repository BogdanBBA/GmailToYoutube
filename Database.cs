using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GmailToYoutube
{
    /// <summary>
    /// Defines the database containing all the information relevant to the app, including youtuber and videos, settings and others.
    /// </summary>
    public class Database
    {
        /// <summary>The number of emails from noreply@youtube.com or whatever, that exist in the Gmail inbox. Keep in mind that not all lead to valid videos.</summary>
        public int EmailsFromYoutube { get; internal set; }
        /// <summary>The full list of videos in the database. Do not use to add new videos!</summary>
        public ListOfIDObjects<VideoSummary> Videos { get; private set; }
        /// <summary>The subset list of videos that compose the playlist. You may add videos here directly from the database, as only the list of video IDs is saved to file and when reading, inexisting videos are scrapped from the playlist.</summary>
        public ListOfIDObjects<VideoSummary> PlaylistVideos { get; private set; }
        /// <summary>The full list of youtubers in the database.</summary>
        public ListOfIDObjects<Youtuber> Youtubers { get; private set; }
        /// <summary>The app settings tied with this database instance.</summary>
        public Settings Settings { get; private set; }

        /// <summary>Constructs a new, empty database.</summary>
        public Database()
        {
            this.EmailsFromYoutube = -1;
            this.Videos = new ListOfIDObjects<VideoSummary>();
            this.PlaylistVideos = new ListOfIDObjects<VideoSummary>();
            this.Youtubers = new ListOfIDObjects<Youtuber>();
            this.Settings = new Settings();
        }

        public void AddVideos(List<VideoSummary> videoSummaries)
        {
            foreach (VideoSummary videoSummary in videoSummaries)
                this.AddVideo(videoSummary);
        }

        public void AddVideo(VideoSummary video)
        {
            if (this.Videos.GetItem(video) != null)
                return;
            Youtuber youtuber = new Youtuber(video.Channel.Key, video.Channel.Value, null);
            this.Youtubers.Add(youtuber);
            video.Youtuber = youtuber;
            this.Videos.AddOrUpdate(video);
        }

        public int TotalVideos
        {
            get
            {
                int result = 0;
                foreach (Youtuber youtuber in this.Youtubers)
                    result += youtuber.Videos.Count;
                return result;
            }
        }

        public int TotalNewVideos
        {
            get
            {
                int result = 0;
                foreach (VideoSummary video in this.Videos)
                    if (video.NotSeen)
                        result++;
                return result;
            }
        }

        public int TotalMarkedToDeleteVideos
        {
            get
            {
                int result = 0;
                foreach (VideoSummary video in this.Videos)
                    if (video.MarkedToDelete)
                        result++;
                return result;
            }
        }

        public TimeSpan TotalVideoDuration
        {
            get
            {
                TimeSpan duration = new TimeSpan();
                foreach (VideoSummary video in this.Videos)
                    duration = duration.Add(video.Duration);
                return duration;
            }
        }

        public VideoSummary GetVideoByEmailID(string id)
        {
            foreach (VideoSummary video in this.Videos)
                if (video.EmailID.Equals(id))
                    return video;
            return null;
        }

        public void RemoveVideoFromVideoListAndYoutuberVideoList(VideoSummary video)
        {
            foreach (Youtuber youtuber in this.Youtubers)
            {
                int yVIndex = youtuber.Videos.GetIndexOfItem(video);
                if (yVIndex != -1)
                    youtuber.Videos.RemoveAt(yVIndex);
            }
            int vIndex = this.Videos.GetIndexOfItem(video);
            if (vIndex != -1)
                this.Videos.RemoveAt(vIndex);
        }

        public ListOfIDObjects<VideoSummary> GetToDeleteVideos()
        {
            ListOfIDObjects<VideoSummary> result = new ListOfIDObjects<VideoSummary>();
            foreach (VideoSummary video in this.Videos)
                if (video.MarkedToDelete)
                    result.Add(video);
            return result;
        }

        public void ClearDatabase()
        {
            this.Videos.Clear();
            this.Youtubers.Clear();
            this.EmailsFromYoutube = 0;
        }

        public string LoadFromDatabase(string filePath)
        {
            string phase = "initializing";
            try
            {
                phase = "opening XML";
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                phase = "decoding emailsFromYoutube";
                this.EmailsFromYoutube = Int32.Parse(doc.SelectSingleNode("Database").Attributes["emailsFromYoutube"].Value);

                phase = "decoding settings";
                this.Settings.LoadFromXmlNode(doc.SelectSingleNode("Database/Settings"));

                phase = "decoding Youtubers";
                XmlNodeList youtuberNodes = doc.SelectNodes("Database/Youtubers/Youtuber");
                foreach (XmlNode youtuberNode in youtuberNodes)
                {
                    string id = youtuberNode.Attributes["ID"].Value;
                    DateTime? lastUpdated = Utils.DecodeNullableDateTime(youtuberNode.Attributes["lastUpdated"].Value);
                    string title = youtuberNode.Attributes["title"].Value;
                    string description = youtuberNode.Attributes["description"].Value;
                    DateTime? published = Utils.DecodeNullableDateTime(youtuberNode.Attributes["published"].Value);
                    ulong? views = Utils.DecodeNullableUnsignedLong(youtuberNode.Attributes["views"].Value);
                    ulong? comments = Utils.DecodeNullableUnsignedLong(youtuberNode.Attributes["comments"].Value);
                    ulong? totalUploads = Utils.DecodeNullableUnsignedLong(youtuberNode.Attributes["totalUploads"].Value);
                    bool subsVisible = bool.Parse(youtuberNode.Attributes["subscribersVisible"].Value);
                    string thumbnailURL = youtuberNode.Attributes["thumbnailURL"].Value;
                    string uploadsPlaylistURL = youtuberNode.Attributes["uploadsPlaylistURL"].Value;
                    ulong? subscribers = Utils.DecodeNullableUnsignedLong(youtuberNode.Attributes["subscribers"].Value);
                    this.Youtubers.Add(new Youtuber(id, lastUpdated, title, description, published, views, comments, totalUploads, subsVisible, thumbnailURL, uploadsPlaylistURL, subscribers));
                }

                phase = "decoding videos";
                XmlNodeList videoNodes = doc.SelectNodes("Database/Videos/Video");
                foreach (XmlNode videoNode in videoNodes)
                {
                    string id = videoNode.Attributes["ID"].Value;
                    DateTime? lastUpdated = Utils.DecodeNullableDateTime(videoNode.Attributes["lastUpdated"].Value);
                    string emailId = videoNode.Attributes["emailID"].Value;
                    string title = videoNode.Attributes["title"].Value;
                    DateTime? published = Utils.DecodeNullableDateTime(videoNode.Attributes["published"].Value);
                    TimeSpan duration = TimeSpan.Parse(videoNode.Attributes["duration"].Value);
                    KeyValuePair<ulong?, ulong?> resolution = new KeyValuePair<ulong?, ulong?>(
                        Utils.DecodeNullableUnsignedLong(videoNode.Attributes["resWidth"].Value),
                        Utils.DecodeNullableUnsignedLong(videoNode.Attributes["resHeight"].Value));
                    ulong? views = Utils.DecodeNullableUnsignedLong(videoNode.Attributes["views"].Value);
                    ulong? comments = Utils.DecodeNullableUnsignedLong(videoNode.Attributes["comments"].Value);
                    ulong? likes = Utils.DecodeNullableUnsignedLong(videoNode.Attributes["likes"].Value);
                    ulong? dislikes = Utils.DecodeNullableUnsignedLong(videoNode.Attributes["dislikes"].Value);
                    string thumbnailURL = videoNode.Attributes["thumbnailURL"].Value;
                    Youtuber youtuber = this.Youtubers.GetItemByID(videoNode.Attributes["channelID"].Value);
                    bool notSeen = bool.Parse(videoNode.Attributes["notSeen"].Value);
                    bool markedToDelete = bool.Parse(videoNode.Attributes["markedToDelete"].Value);
                    VideoSummary video = new VideoSummary(id, emailId, lastUpdated, title, published, duration, resolution, views, comments, likes, dislikes, thumbnailURL, youtuber, notSeen, markedToDelete);
                    youtuber.Videos.Add(video);
                    this.Videos.Add(video);
                }

                phase = "sorting videos";
                this.Videos.SortVideos(this.Settings.VideoSorting);
                foreach (Youtuber youtuber in this.Youtubers)
                    youtuber.Videos.SortVideos(this.Settings.VideoSorting);
                this.Youtubers.SortYoutubers(this.Settings.YoutuberSorting);

                phase = "decoding playlist";
                XmlNodeList playlistVideoNodes = doc.SelectNodes("Database/Playlist/VideoReference");
                foreach (XmlNode videoReferenceNode in playlistVideoNodes)
                {
                    VideoSummary video = this.Videos.GetItemByID(videoReferenceNode.Attributes["ID"].Value);
                    if (video != null)
                        this.PlaylistVideos.Add(video);
                }

                return "";
            }
            catch (Exception E)
            {
                return "ERROR: Database.LoadFromDatabase(), phase '" + phase + "'\n\n" + E.ToString();
            }
        }

        public string SaveToDatabase(string filePath)
        {
            string phase = "initializing";
            try
            {
                phase = "initializing XML";
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.AppendChild(doc.CreateElement("Database"));
                root.AddAttribute(doc, "emailsFromYoutube", this.EmailsFromYoutube.ToString());

                phase = "saving settings";
                root.AppendChild(this.Settings.SaveToXmlNode(doc));

                phase = "saving Youtubers";
                XmlNode youtubersNode = root.AppendChild(doc.CreateElement("Youtubers"));
                youtubersNode.AddAttribute(doc, "count", this.Youtubers.Count.ToString());
                foreach (Youtuber youtuber in this.Youtubers)
                {
                    XmlNode yNode = youtubersNode.AppendChild(doc.CreateElement("Youtuber"));
                    yNode.AddAttribute(doc, "ID", youtuber.ID);
                    yNode.AddAttribute(doc, "title", youtuber.ChannelTitle);
                    yNode.AddAttribute(doc, "lastUpdated", youtuber.LastUpdated);
                    yNode.AddAttribute(doc, "description", youtuber.Description);
                    yNode.AddAttribute(doc, "published", youtuber.Published);
                    yNode.AddAttribute(doc, "views", youtuber.Views);
                    yNode.AddAttribute(doc, "comments", youtuber.Comments);
                    yNode.AddAttribute(doc, "totalUploads", youtuber.TotalUploads);
                    yNode.AddAttribute(doc, "subscribersVisible", youtuber.Subscribers.Key.ToString());
                    yNode.AddAttribute(doc, "thumbnailURL", youtuber.ThumbnailURL);
                    yNode.AddAttribute(doc, "uploadsPlaylistURL", youtuber.UploadsPlaylistID);
                    yNode.AddAttribute(doc, "subscribers", youtuber.Subscribers.Value);
                }

                phase = "saving videos";
                XmlNode videosNode = root.AppendChild(doc.CreateElement("Videos"));
                videosNode.AddAttribute(doc, "count", this.Videos.Count.ToString());
                foreach (VideoSummary video in this.Videos)
                {
                    XmlNode vNode = videosNode.AppendChild(doc.CreateElement("Video"));
                    vNode.AddAttribute(doc, "ID", video.ID);
                    vNode.AddAttribute(doc, "lastUpdated", video.LastUpdated);
                    vNode.AddAttribute(doc, "title", video.Title);
                    vNode.AddAttribute(doc, "emailID", video.EmailID);
                    vNode.AddAttribute(doc, "published", video.Published);
                    vNode.AddAttribute(doc, "duration", video.Duration.ToString());
                    vNode.AddAttribute(doc, "resWidth", video.Resolution.Key);
                    vNode.AddAttribute(doc, "resHeight", video.Resolution.Value);
                    vNode.AddAttribute(doc, "views", video.Views);
                    vNode.AddAttribute(doc, "comments", video.Comments);
                    vNode.AddAttribute(doc, "likes", video.Likes.Key);
                    vNode.AddAttribute(doc, "dislikes", video.Likes.Value);
                    vNode.AddAttribute(doc, "thumbnailURL", video.ThumbnailURL);
                    vNode.AddAttribute(doc, "channelID", video.Youtuber.ID);
                    vNode.AddAttribute(doc, "notSeen", video.NotSeen.ToString());
                    vNode.AddAttribute(doc, "markedToDelete", video.MarkedToDelete.ToString());
                }

                phase = "saving playlist";
                XmlNode playlistNode = root.AppendChild(doc.CreateElement("Playlist"));
                playlistNode.AddAttribute(doc, "count", this.PlaylistVideos.Count.ToString());
                foreach (VideoSummary video in this.PlaylistVideos)
                {
                    XmlNode vrNode = playlistNode.AppendChild(doc.CreateElement("VideoReference"));
                    vrNode.AddAttribute(doc, "ID", video.ID);
                }

                phase = "saving XML to file";
                doc.Save(filePath);
                return "";
            }
            catch (Exception E)
            {
                return "ERROR: Database.SaveToDatabase(), phase '" + phase + "'\n\n" + E.ToString();
            }
        }
    }
}
