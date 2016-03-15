using BBA.VisualComponents;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using GmailToYoutube.BBA.VisualComponents;
using GmailToYoutube.BBA;
using System.Drawing.Drawing2D;

namespace GmailToYoutube
{
    /// <summary>
    /// A pair of values (a more convenient form of KeyValuePair).
    /// </summary>
    /// <typeparam name="T">the data type of the values (unrestricted)</typeparam>
    public class Pair<T>
    {
        private KeyValuePair<T, T> pair;

        public Pair(T normal, T highlighted)
        {
            this.pair = new KeyValuePair<T, T>(normal, highlighted);
        }

        public T Normal
        {
            get { return this.pair.Key; }
            set { this.pair = new KeyValuePair<T, T>(value, this.pair.Value); }
        }

        public T Highlighted
        {
            get { return this.pair.Value; }
            set { this.pair = new KeyValuePair<T, T>(this.pair.Key, value); }
        }

        public T GetValue(bool highlighted)
        {
            return highlighted ? this.Highlighted : this.Normal;
        }
    }

    /// <summary>
    /// Utility functions and extension methods.
    /// </summary>
    public static class Utils
    {
        private static string NullString = "null";

        public static bool MessageIsYoutubeUpload(Message message)
        {
            return (message.Payload.Headers.First(header => header.Name.Equals("From")).Value.Contains("noreply@youtube.com")
                 && message.Payload.Headers.First(header => header.Name.Equals("Subject")).Value.Contains("just uploaded a video"));
        }

        public static string ExtractEmailAddress(string from)
        {
            if (!(from.Contains('<') && from.Contains('>')))
                return from;
            from = from.Substring(from.IndexOf('<') + 1);
            return from.Substring(0, from.IndexOf('>'));
        }

        public static string DecodeMessageBody(string base64url)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64url.Replace('-', '+').Replace('_', '/')));
        }

        public static string ExtractVideoIDFromMessageBody(string body)
        {
            const string markerA = "?v=", markerB = "&feature=";
            body = body.Substring(body.IndexOf(markerA) + markerA.Length);
            return body.Substring(0, body.IndexOf(markerB));
        }

        public static void UpdateYoutuberInfo(ref Youtuber youtuber, Channel channel, ListOfIDObjects<VideoSummary> videos)
        {
            youtuber.ChannelTitle = channel.Snippet.Title;
            youtuber.Description = channel.Snippet.Description;
            youtuber.Published = channel.Snippet.PublishedAt;
            youtuber.Views = channel.Statistics.ViewCount;
            youtuber.Comments = channel.Statistics.CommentCount;
            youtuber.TotalUploads = channel.Statistics.VideoCount;
            ThumbnailDetails thumb = channel.Snippet.Thumbnails;
            youtuber.ThumbnailURL = thumb.High != null ? thumb.High.Url : (thumb.Medium != null ? thumb.Medium.Url : thumb.Default__.Url);
            youtuber.UploadsPlaylistID = channel.ContentDetails.RelatedPlaylists.Uploads;
            youtuber.Subscribers = new KeyValuePair<bool, ulong?>(!channel.Statistics.HiddenSubscriberCount.GetValueOrDefault(false), channel.Statistics.SubscriberCount);
            youtuber.LastUpdated = DateTime.Now;
            foreach (VideoSummary video in videos)
                if ((video.Youtuber != null && video.Youtuber.ID.Equals(youtuber.ID)) || video.Channel.Key != null && video.Channel.Key.Equals(youtuber.ID))
                    youtuber.Videos.Add(video);
        }

        public static void AddAttribute(this XmlNode node, XmlDocument doc, string key, string value)
        {
            XmlAttribute attribute = doc.CreateAttribute(key);
            attribute.Value = value != null ? value : Utils.NullString;
            node.Attributes.Append(attribute);
        }

        public static void AddAttribute(this XmlNode node, XmlDocument doc, string key, ulong? value)
        { node.AddAttribute(doc, key, value.HasValue ? value.Value.ToString() : null); }

        public static void AddAttribute(this XmlNode node, XmlDocument doc, string key, DateTime? value)
        { node.AddAttribute(doc, key, value.HasValue ? value.Value.ToString() : null); }

        public static void AddAttribute(this XmlNode node, XmlDocument doc, Pair<ColorResource> value)
        {
            node.AddAttribute(doc, "normal", ColorTranslator.ToHtml(value.Normal.Color));
            node.AddAttribute(doc, "highlighted", ColorTranslator.ToHtml(value.Highlighted.Color));
        }

        public static string DecodeNullableString(string text)
        { return text == null || text.Equals(Utils.NullString) ? null : text; }

        public static DateTime? DecodeNullableDateTime(string text)
        { return text.Equals(Utils.NullString) ? (DateTime?) null : DateTime.Parse(text); }

        public static ulong? DecodeNullableUnsignedLong(string text)
        { return text.Equals(Utils.NullString) ? (ulong?) null : ulong.Parse(text); }

        public static Point MinimumPointValues(Point a, Point b)
        { return new Point(a.X < b.X ? a.X : b.X, a.Y < b.Y ? a.Y : b.Y); }

        public static Point MaximumPointValues(Point a, Point b)
        { return new Point(a.X > b.X ? a.X : b.X, a.Y > b.Y ? a.Y : b.Y); }

        public static string Plural(string singularForm, long quantity, bool includeQuantity)
        {
            string form = quantity == 1 ? singularForm : singularForm + "s";
            return includeQuantity ? Utils.FormatNumber(quantity) + " " + form : form;
        }

        public static Size ScaleRectangle(int width, int height, int maxWidth, int maxHeight)
        {
            var ratioX = (double) maxWidth / width;
            var ratioY = (double) maxHeight / height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (width * ratio);
            var newHeight = (int) (height * ratio);

            return new Size(newWidth, newHeight);
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight, InterpolationMode mode, bool disposeOldImage)
        {
            Size newSize = ScaleRectangle(image.Width, image.Height, maxWidth, maxHeight);
            Image newImage = new Bitmap(newSize.Width, newSize.Height);
            Graphics g = Graphics.FromImage(newImage);
            g.InterpolationMode = mode;
            g.DrawImage(image, 0, 0, newSize.Width, newSize.Height);
            if (disposeOldImage)
                image.Dispose();
            return newImage;
        }

        public static Image GetScaledImageOrScaledDefault(string imagePath, int maxWidth, int maxHeight, InterpolationMode mode, Image defaultImg)
        {
            try
            { return Utils.ScaleImage(new Bitmap(imagePath), maxWidth, maxHeight, mode, true); }
            catch (Exception)
            { return Utils.ScaleImage(defaultImg, maxWidth, maxHeight, mode, false); }
        }

        public static Image GetBlankImage(Size size)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(MyGUIs.Background.Normal.Color);
            return bmp;
        }

        public static string FormatNumber(long number)
        {
            return number.ToString("#,##0");
        }

        public static string FormatDuration(TimeSpan duration)
        {
            return (int) duration.TotalMinutes + ":" + duration.Seconds.ToString("D2");
        }

        public static string ToStats(this ulong? number)
        {
            return number.HasValue ? Utils.FormatNumber((long) number.Value) : "unknown";
        }

        public static DateTime GetMostRecentlyPublishedVideo(this ListOfIDObjects<VideoSummary> list, DateTime defaultIfNoneExist)
        {
            DateTime result = defaultIfNoneExist;
            foreach (VideoSummary video in list)
                if (video.Published.HasValue && video.Published.Value.CompareTo(result) > 0)
                    result = video.Published.Value;
            return result;
        }

        public static void SortVideos(this ListOfIDObjects<VideoSummary> videos, Settings.VideoSortingOptions sortingOption)
        {
            if (videos.Count < 2)
                return;
            switch (sortingOption)
            {
                case Settings.VideoSortingOptions.Alphabetically:
                    for (int vA = 0; vA < videos.Count - 1; vA++)
                        for (int vB = vA + 1; vB < videos.Count; vB++)
                            if (videos[vA].Title.CompareTo(videos[vB].Title) > 0)
                                videos.SwapItemsAtPositions(vA, vB);
                    break;
                case Settings.VideoSortingOptions.Chronologically:
                    DateTime defaultDateTime = new DateTime(2000, 1, 1, 0, 0, 0);
                    for (int vA = 0; vA < videos.Count - 1; vA++)
                        for (int vB = vA + 1; vB < videos.Count; vB++)
                            if (videos[vA].Published.GetValueOrDefault(defaultDateTime).CompareTo(videos[vB].Published.GetValueOrDefault(defaultDateTime)) < 0)
                                videos.SwapItemsAtPositions(vA, vB);
                    break;
                case Settings.VideoSortingOptions.ByDuration:
                    for (int vA = 0; vA < videos.Count - 1; vA++)
                        for (int vB = vA + 1; vB < videos.Count; vB++)
                            if (videos[vA].Duration.CompareTo(videos[vB].Duration) < 0)
                                videos.SwapItemsAtPositions(vA, vB);
                    break;
            }
        }

        public static void SortYoutubers(this ListOfIDObjects<Youtuber> youtubers, Settings.YoutuberSortingOptions sortingOption)
        {
            if (youtubers.Count < 2)
                return;
            DateTime defaultDateTime = new DateTime(2000, 1, 1);
            switch (sortingOption)
            {
                case Settings.YoutuberSortingOptions.Alphabetically:
                    for (int yA = 0; yA < youtubers.Count - 1; yA++)
                        for (int yB = yA + 1; yB < youtubers.Count; yB++)
                            if (youtubers[yA].ChannelTitle.CompareTo(youtubers[yB].ChannelTitle) > 0)
                                youtubers.SwapItemsAtPositions(yA, yB);
                    break;
                case Settings.YoutuberSortingOptions.Chronologically:
                    for (int yA = 0; yA < youtubers.Count - 1; yA++)
                    {
                        for (int yB = yA + 1; yB < youtubers.Count; yB++)
                        {
                            DateTime lastPublishYA = youtubers[yA].Videos.GetMostRecentlyPublishedVideo(defaultDateTime);
                            DateTime lastPublishYB = youtubers[yB].Videos.GetMostRecentlyPublishedVideo(defaultDateTime);
                            if (lastPublishYA.CompareTo(lastPublishYB) < 0)
                                youtubers.SwapItemsAtPositions(yA, yB);
                        }
                    }
                    break;
                case Settings.YoutuberSortingOptions.ByNumberOfVideos:
                    for (int yA = 0; yA < youtubers.Count - 1; yA++)
                        for (int yB = yA + 1; yB < youtubers.Count; yB++)
                            if (youtubers[yA].Videos.Count < youtubers[yB].Videos.Count)
                                youtubers.SwapItemsAtPositions(yA, yB);
                            else if (youtubers[yA].Videos.Count == youtubers[yB].Videos.Count)
                            {
                                DateTime lastPublishYA = youtubers[yA].Videos.GetMostRecentlyPublishedVideo(defaultDateTime);
                                DateTime lastPublishYB = youtubers[yB].Videos.GetMostRecentlyPublishedVideo(defaultDateTime);
                                if (lastPublishYA.CompareTo(lastPublishYB) < 0)
                                    youtubers.SwapItemsAtPositions(yA, yB);
                            }
                    break;
                case Settings.YoutuberSortingOptions.ByTotalDuration:
                    for (int yA = 0; yA < youtubers.Count - 1; yA++)
                        for (int yB = yA + 1; yB < youtubers.Count; yB++)
                            if (youtubers[yA].TotalVideoDuration.CompareTo(youtubers[yB].TotalVideoDuration) < 0)
                                youtubers.SwapItemsAtPositions(yA, yB);
                    break;
            }
        }

        public static Image ConvertToGrayscale(Image original)
        {
            //create a blank bitmap the same size as original and get a graphics object 
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][] {
                    new float[] { .3f, .3f, .3f, 0, 0 },
                    new float[] { .59f, .59f, .59f, 0, 0 },
                    new float[] { .11f, .11f, .11f, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { 0, 0, 0, 0, 1 } });

            //create some image attributes and set the color matrix attribute
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image using the grayscale color matrix
            g.DrawImage(original,
                new Rectangle(0, 0, original.Width, original.Height),
                0, 0, original.Width, original.Height,
                GraphicsUnit.Pixel,
                attributes);

            //dispose the Graphics object and return the result
            g.Dispose();
            return newBitmap;
        }

        public static void ApplyAlphaMask(Bitmap bmp, Bitmap alphaMaskImage)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            BitmapData dataAlphaMask = alphaMaskImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try
            {
                BitmapData data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                try
                {
                    unsafe //using pointer requires the unsafe keyword
                    {
                        byte* pData0Mask = (byte*) dataAlphaMask.Scan0;
                        byte* pData0 = (byte*) data.Scan0;

                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                byte* pData = pData0 + (y * data.Stride) + (x * 4);
                                byte* pDataMask = pData0Mask + (y * dataAlphaMask.Stride) + (x * 4);

                                byte maskBlue = pDataMask[0];
                                byte maskGreen = pDataMask[1];
                                byte maskRed = pDataMask[2];

                                //the closer the color is to black the more opaque it will be.
                                byte alpha = (byte) (255 - (maskRed + maskBlue + maskGreen) / 3);

                                //respect the original alpha value
                                byte originalAlpha = pData[3];
                                pData[3] = (byte) (((float) (alpha * originalAlpha)) / 255f);
                            }
                        }
                    }
                }
                finally
                {
                    bmp.UnlockBits(data);
                }
            }
            finally
            {
                alphaMaskImage.UnlockBits(dataAlphaMask);
            }
        }

        public static void SizeAndPositionControlsInPanel(System.Windows.Forms.Panel container, IList<System.Windows.Forms.Control> controls, bool horizontally, int padding)
        {
            int newControlSize = (int) (((horizontally ? container.Width : container.Height) - (controls.Count - 1) * padding) / (double) controls.Count);
            for (int index = 0, lastPos = 0; index < controls.Count; index++, lastPos += newControlSize + padding)
                if (horizontally)
                    controls[index].SetBounds(lastPos, 0, newControlSize, container.Height);
                else
                    controls[index].SetBounds(0, lastPos, container.Width, newControlSize);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class StaticImages
    {
        public static Bitmap UpdateIcon { get; private set; }
        public static Bitmap AllIcon { get; private set; }
        public static Bitmap PlaylistIcon { get; private set; }
        public static Bitmap HideIcon { get; private set; }
        public static Bitmap SettingsIcon { get; private set; }
        public static Bitmap MoreIcon { get; private set; }
        public static Bitmap ExitIcon { get; private set; }

        public static Bitmap GmailIcon { get; private set; }
        public static Bitmap YoutubeIcon { get; private set; }
        public static Bitmap YoutuberIcon { get; private set; }
        public static Bitmap YoutuberLargeIcon { get; private set; }
        public static Bitmap VideoIcon { get; private set; }
        public static Bitmap VideoLargeIcon { get; private set; }
        public static Bitmap VideoNewLargeIcon { get; private set; }
        public static Bitmap VideoMarkedToDeleteLargeIcon { get; private set; }

        public static Bitmap NotSeenIcon { get; private set; }
        public static Bitmap DeleteQueryIcon { get; private set; }
        public static Bitmap IconMask { get; private set; }

        public static string LoadStaticImages()
        {
            try
            {
                // load 

                StaticImages.UpdateIcon = new Bitmap(Paths.UpdateIconFile);
                StaticImages.AllIcon = new Bitmap(Paths.AllIconFile);
                StaticImages.PlaylistIcon = new Bitmap(Paths.PlaylistIconFile);
                StaticImages.HideIcon = new Bitmap(Paths.HideIconFile);
                StaticImages.SettingsIcon = new Bitmap(Paths.SettingsIconFile);
                StaticImages.MoreIcon = new Bitmap(Paths.MoreIconFile);
                StaticImages.ExitIcon = new Bitmap(Paths.ExitIconFile);

                StaticImages.GmailIcon = new Bitmap(Paths.GmailIconFile);
                StaticImages.YoutubeIcon = new Bitmap(Paths.YoutubeIconFile);
                StaticImages.YoutuberIcon = new Bitmap(Paths.YoutuberIconFile);
                StaticImages.YoutuberLargeIcon = new Bitmap(Paths.YoutuberLargeIconFile);
                StaticImages.VideoIcon = new Bitmap(Paths.VideoIconFile);
                StaticImages.VideoLargeIcon = new Bitmap(Paths.VideoLargeIconFile);
                StaticImages.VideoNewLargeIcon = new Bitmap(Paths.VideoNewLargeIconFile);
                StaticImages.VideoMarkedToDeleteLargeIcon = new Bitmap(Paths.VideoMarkedToDeleteLargeIconFile);

                StaticImages.NotSeenIcon = new Bitmap(Paths.NotSeenIconFile);
                StaticImages.DeleteQueryIcon = new Bitmap(Paths.DeleteQueryIconFile);
                StaticImages.IconMask = new Bitmap(Paths.IconMaskFile);

                // set 'resolution' to match each other (for some reason, some don't with others, whatever)

                SizeF res = new SizeF(StaticImages.YoutuberIcon.HorizontalResolution, StaticImages.YoutuberIcon.VerticalResolution);

                StaticImages.UpdateIcon.SetResolution(res.Width, res.Height);
                StaticImages.AllIcon.SetResolution(res.Width, res.Height);
                StaticImages.PlaylistIcon.SetResolution(res.Width, res.Height);
                StaticImages.HideIcon.SetResolution(res.Width, res.Height);
                StaticImages.SettingsIcon.SetResolution(res.Width, res.Height);
                StaticImages.MoreIcon.SetResolution(res.Width, res.Height);
                StaticImages.ExitIcon.SetResolution(res.Width, res.Height);

                StaticImages.YoutubeIcon.SetResolution(res.Width, res.Height);
                StaticImages.YoutuberIcon.SetResolution(res.Width, res.Height);
                StaticImages.YoutuberLargeIcon.SetResolution(res.Width, res.Height);
                StaticImages.VideoIcon.SetResolution(res.Width, res.Height);
                StaticImages.VideoLargeIcon.SetResolution(res.Width, res.Height);
                StaticImages.VideoNewLargeIcon.SetResolution(res.Width, res.Height);
                StaticImages.VideoMarkedToDeleteLargeIcon.SetResolution(res.Width, res.Height);

                StaticImages.NotSeenIcon.SetResolution(res.Width, res.Height);
                StaticImages.DeleteQueryIcon.SetResolution(res.Width, res.Height);
                StaticImages.IconMask.SetResolution(res.Width, res.Height);

                return "";
            }
            catch (Exception E)
            { return E.ToString(); }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Paths
    {
        public static readonly string ProgramFilesFolder = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + "/program-files/";
        public static readonly string ResourcesFolder = ProgramFilesFolder + "resources/";
        public static readonly string VideoImagesFolder = ProgramFilesFolder + "video-images/";
        public static readonly string YoutuberImagesFolder = ProgramFilesFolder + "youtuber-images/";

        public static readonly string ApiFile = ProgramFilesFolder + "api.json";
        public static readonly string DatabaseFile = ProgramFilesFolder + "database.xml";
        public static readonly string ThemeFile = ProgramFilesFolder + "theme.xml";
        public static readonly string AppIconPngFile = ResourcesFolder + "icon.png";

        public static readonly string UpdateIconFile = ResourcesFolder + "menu update.png";
        public static readonly string AllIconFile = ResourcesFolder + "menu all.png";
        public static readonly string PlaylistIconFile = ResourcesFolder + "menu playlist.png";
        public static readonly string HideIconFile = ResourcesFolder + "menu hide.png";
        public static readonly string SettingsIconFile = ResourcesFolder + "menu settings.png";
        public static readonly string MoreIconFile = ResourcesFolder + "menu more.png";
        public static readonly string ExitIconFile = ResourcesFolder + "menu exit.png";

        public static readonly string GmailIconFile = ResourcesFolder + "gmail.png";
        public static readonly string YoutubeIconFile = ResourcesFolder + "youtube.png";
        public static readonly string YoutuberIconFile = ResourcesFolder + "youtuber.png";
        public static readonly string YoutuberLargeIconFile = ResourcesFolder + "youtuberLarge.png";
        public static readonly string VideoIconFile = ResourcesFolder + "video.png";
        public static readonly string VideoLargeIconFile = ResourcesFolder + "videoLarge.png";
        public static readonly string VideoNewLargeIconFile = ResourcesFolder + "videoNewLarge.png";
        public static readonly string VideoMarkedToDeleteLargeIconFile = ResourcesFolder + "videoMarkedToDeleteLarge.png";

        public static readonly string NotSeenIconFile = ResourcesFolder + "new.png";
        public static readonly string DeleteQueryIconFile = ResourcesFolder + "deleteQuery.png";
        public static readonly string IconMaskFile = ResourcesFolder + "mask.png";

        public static readonly string[] Folders = { ProgramFilesFolder, ResourcesFolder, VideoImagesFolder, YoutuberImagesFolder };
        public static readonly string[] Files = { ApiFile, DatabaseFile, ThemeFile, AppIconPngFile,
            UpdateIconFile, AllIconFile, PlaylistIconFile, HideIconFile, SettingsIconFile, MoreIconFile, ExitIconFile,
            GmailIconFile, YoutubeIconFile, YoutuberIconFile, YoutuberLargeIconFile, VideoIconFile, VideoLargeIconFile,
            VideoNewLargeIconFile, VideoMarkedToDeleteLargeIconFile,
            NotSeenIconFile, DeleteQueryIconFile, IconMaskFile };

        public static string CheckPaths(bool tryToCreateMissingFolders)
        {
            string phase = "initializing";
            try
            {
                phase = "checking folders";

                foreach (string folder in Folders)
                {
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    if (!Directory.Exists(folder))
                        throw new Exception("Folder '" + folder + "' does not exist!");
                }

                foreach (string file in Files)
                    if (!File.Exists(file))
                        throw new Exception("File '" + file + "' does not exist!");

                return "";
            }
            catch (Exception E)
            {
                return "ERROR: Paths.CheckPaths(), phase '" + phase + "'\n\n" + E.ToString();
            }
        }
    }
}
