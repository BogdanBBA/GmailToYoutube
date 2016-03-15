using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.YouTube.v3.Data;

namespace GmailToYoutube
{
    /// <summary>
    /// A stripped-down version of the <code>Google.Apis.Gmail.v1.Data.Message</code> class.
    /// </summary>
    public class EmailSummary
    {
        public string ID { get; private set; }
        public string From { get; private set; }
        public string Subject { get; private set; }
        public DateTime Date { get; private set; }
        public string Body { get; private set; }

        public EmailSummary(Message message)
        {
            this.ID = message.Id;
            this.From = Utils.ExtractEmailAddress(message.Payload.Headers.First(header => header.Name.Equals("From")).Value);
            this.Subject = message.Payload.Headers.First(header => header.Name.Equals("Subject")).Value;
            this.Date = DateTime.Parse(message.Payload.Headers.First(header => header.Name.Equals("Date")).Value);
            this.Body = Utils.DecodeMessageBody(message.Payload.Parts[0].Body.Data);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class ObjectWithID
    {
        public string ID { get; private set; }

        public ObjectWithID(string id, DateTime? lastUpdated)
        {
            this.ID = id;
            this.LastUpdated = lastUpdated;
        }

        public DateTime? LastUpdated
        {
            get;
            set;
        }

        public abstract string ThumbnailPath { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TYPE"></typeparam>
    public class ListOfIDObjects<TYPE> : List<TYPE> where TYPE : ObjectWithID
    {
        public new void Add(TYPE item)
        {
            if (this.GetIndexOfItemByID(item.ID) == -1)
                base.Add(item);
        }

        public void AddOrUpdate(TYPE item)
        {
            item.LastUpdated = DateTime.Now;
            int index = this.GetIndexOfItemByID(item.ID);
            if (index == -1)
                this.Add(item);
            else
                this[index] = item;
        }

        public int GetIndexOfItemByID(string id)
        {
            for (int iItem = 0; iItem < this.Count; iItem++)
                if (this[iItem].ID.Equals(id))
                    return iItem;
            return -1;
        }

        public int GetIndexOfItem(TYPE item)
        {
            return this.GetIndexOfItemByID(item.ID);
        }

        public TYPE GetItemByID(string id)
        {
            int index = this.GetIndexOfItemByID(id);
            return index != -1 ? this[index] : null;
        }

        public TYPE GetItem(TYPE item)
        {
            return item != null ? this.GetItemByID(item.ID) : null;
        }

        public ListOfIDObjects<TYPE> GetDeepCopy()
        {
            ListOfIDObjects<TYPE> result = new ListOfIDObjects<TYPE>();
            foreach (TYPE item in this)
                result.Add(item);
            return result;
        }

        public void SwapItemsAtPositions(int posA, int posB)
        {
            if (posA >= 0 && posA < this.Count && posB >= 0 && posB < this.Count)
            {
                TYPE aux = this[posA];
                this[posA] = this[posB];
                this[posB] = aux;
            }
        }
    }

    /// <summary>
    /// A class that contains all the information relevant to a YouTube video.
    /// </summary>
    public class VideoSummary : ObjectWithID
    {
        public string EmailID { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime? Published { get; private set; }
        public TimeSpan Duration { get; private set; }
        public KeyValuePair<ulong?, ulong?> Resolution { get; private set; }
        public ulong? Views { get; private set; }
        public ulong? Comments { get; private set; }
        public KeyValuePair<ulong?, ulong?> Likes { get; private set; }
        public string ThumbnailURL { get; private set; }
        public KeyValuePair<string, string> Channel { get; private set; }
        protected internal Youtuber Youtuber { get; set; }

        public bool NotSeen { get; set; }
        public bool MarkedToDelete { get; set; }

        /// <summary>Constructs a new VideoSummary object from the given video ID and (Google) Video object. To be used after getting email.</summary>
        public VideoSummary(string emailID, string id, DateTime? lastUpdated, Video video)
            : base(id, lastUpdated)
        {
            this.EmailID = emailID;
            this.Title = video.Snippet.Title;
            this.Published = video.Snippet.PublishedAt;
            this.Duration = XmlConvert.ToTimeSpan(video.ContentDetails.Duration);
            ThumbnailDetails nails = video.Snippet.Thumbnails;
            Thumbnail thumb = nails.Maxres != null ? nails.Maxres : (nails.High != null ? nails.High : (nails.Medium != null ? nails.Medium : nails.Default__));
            this.Resolution = new KeyValuePair<ulong?, ulong?>((ulong) thumb.Width, (ulong) thumb.Height);
            this.Views = video.Statistics.ViewCount;
            this.Comments = video.Statistics.CommentCount;
            this.Likes = new KeyValuePair<ulong?, ulong?>(video.Statistics.LikeCount, video.Statistics.DislikeCount);
            this.ThumbnailURL = thumb.Url;
            this.Channel = new KeyValuePair<string, string>(video.Snippet.ChannelId, video.Snippet.ChannelTitle);
            this.Youtuber = null;
            this.NotSeen = true;
            this.MarkedToDelete = false;
        }

        /// <summary>Constructs a new VideoSummary object from the given attributes. To be used when reading from the local database.</summary>
        public VideoSummary(string id, string emailID, DateTime? lastUpdated, string title, DateTime? published, TimeSpan duration, KeyValuePair<ulong?, ulong?> resolution, ulong? views, ulong? comments, ulong? likes, ulong? dislikes, string url, Youtuber youtuber, bool notSeen, bool markedToDelete)
            : base(id, lastUpdated)
        {
            this.EmailID = emailID;
            this.Title = title;
            this.Published = published;
            this.Duration = duration;
            this.Resolution = resolution;
            this.Views = views;
            this.Comments = comments;
            this.Likes = new KeyValuePair<ulong?, ulong?>(likes, dislikes);
            this.ThumbnailURL = url;
            this.Channel = new KeyValuePair<string, string>(null, null);
            this.Youtuber = youtuber;
            this.NotSeen = notSeen;
            this.MarkedToDelete = markedToDelete;
        }

        public override string ThumbnailPath
        { get { return Paths.VideoImagesFolder + this.ID + ".jpg"; } }

        public string GmailLink(Settings settings)
        {
            return string.Format(@"https://mail.google.com/mail/u/0/#inbox/{0}", this.EmailID);
        }

        public string YoutubeLink(Settings settings)
        {
            return string.Format(@"https://www.youtube.com/watch?v={0}&start={1}", this.ID, settings.SecondsToSkipInVideo);
        }

        public string VideoLink(Settings settings)
        {
            return string.Format(@"https://www.youtube.com/embed/{0}?vq={1}&autoplay=1&showinfo=0&start={2}",
                this.ID, Settings.VideoResolutions[settings.VideoResolutionIndex], settings.SecondsToSkipInVideo);
        }

        public override string ToString()
        { return string.Format("VideoSummary: ID=\"{0}\", Youtuber.ChannelTitle=\"{1}\", Title=\"{2}\"", this.ID, this.Youtuber.ChannelTitle, this.Title); }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Youtuber : ObjectWithID
    {
        public string ChannelTitle { get; internal set; }
        public string Description { get; internal set; }
        public DateTime? Published { get; internal set; }
        public ulong? Views { get; internal set; }
        public ulong? Comments { get; internal set; }
        public ulong? TotalUploads { get; internal set; }
        public KeyValuePair<bool, ulong?> Subscribers { get; internal set; }
        public string ThumbnailURL { get; internal set; }
        public string UploadsPlaylistID { get; internal set; }
        public ListOfIDObjects<VideoSummary> Videos { get; private set; }

        /// <summary>Constructs a new Youtuber object from the given channel ID and title. To be used after getting email and before checking with Youtube.</summary>
        public Youtuber(string id, string channelTitle, DateTime? lastUpdated)
            : base(id, lastUpdated)
        {
            this.ChannelTitle = channelTitle;
            this.Description = null;
            this.Published = null;
            this.Views = null;
            this.Comments = null;
            this.TotalUploads = null;
            this.ThumbnailURL = null;
            this.UploadsPlaylistID = null;
            this.Subscribers = new KeyValuePair<bool, ulong?>(false, 0);
            this.Videos = new ListOfIDObjects<VideoSummary>();
        }

        /// <summary>Constructs a new Youtuber object from the given attributes. To be used when reading from the local database.</summary>
        public Youtuber(string id, DateTime? lastUpdated, string channelTitle, string description, DateTime? published, ulong? views, ulong? comments,
            ulong? totalUploads, bool subsVisible, string url, string uploadsPlaylistURL, ulong? subscribers)
            : base(id, lastUpdated)
        {
            this.ChannelTitle = channelTitle;
            this.Description = description;
            this.Published = published;
            this.Views = views;
            this.Comments = comments;
            this.TotalUploads = totalUploads;
            this.ThumbnailURL = url;
            this.UploadsPlaylistID = uploadsPlaylistURL;
            this.Subscribers = new KeyValuePair<bool, ulong?>(subsVisible, subscribers);
            this.Videos = new ListOfIDObjects<VideoSummary>();
        }

        public override string ThumbnailPath
        { get { return Paths.YoutuberImagesFolder + this.ID + ".jpg"; } }

        public string ChannelLink(Settings settings)
        {
            return string.Format(@"https://www.youtube.com/playlist?list={0}", this.UploadsPlaylistID);
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

        public override string ToString()
        { return string.Format("Youtuber: ID=\"{0}\", Title=\"{1}\"", this.ID, this.ChannelTitle); }
    }
}
