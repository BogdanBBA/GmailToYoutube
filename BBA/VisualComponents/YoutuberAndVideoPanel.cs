using GmailToYoutube;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GmailToYoutube.BBA.VisualComponents
{
    /// <summary>
    /// Base class of information controls for videos and youtubers.
    /// </summary>
    public abstract class AbstractYoutuberVideoControl : Control
    {
        public enum ControlSizes { Small, Large };
        internal static string[] ControlSizesDescriptions = { "Small", "Large" };

        protected Settings settings;
        protected EventHandler mouseOver_SetLastAbstractControl_EventHandler;
        protected bool mouseIsOver = false;
        protected bool mouseIsClicked = false;

        public AbstractYoutuberVideoControl(Settings settings, EventHandler mouseOver_SetLastAbstractControl_EventHandler)
            : base()
        {
            this.settings = settings;
            this.mouseOver_SetLastAbstractControl_EventHandler = mouseOver_SetLastAbstractControl_EventHandler;
            this.Cursor = Cursors.Hand;
        }

        public abstract int DefaultControlHeight { get; }

        public Image Thumbnail { get; protected set; }
        public abstract void LoadThumbnail(Settings settings);

        public abstract string GetTitle { get; }
        public abstract string GetSubtitle { get; }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.mouseIsOver = true;
            if (this.mouseOver_SetLastAbstractControl_EventHandler != null)
                this.mouseOver_SetLastAbstractControl_EventHandler(this, e);
            base.OnMouseEnter(e);
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.mouseIsOver = false;
            base.OnMouseLeave(e);
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.mouseIsClicked = true;
            base.OnMouseDown(mevent);
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            this.mouseIsClicked = false;
            base.OnMouseUp(mevent);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
    }

    /// <summary>
    /// Displays information about a Youtuber.
    /// </summary>
    public class YoutuberControl : AbstractYoutuberVideoControl
    {
        public static ControlSizes ControlSize = ControlSizes.Large;
        protected static readonly Font TitleFont = new Font("Segoe UI", 16, FontStyle.Bold);
        protected static readonly Font SubtitleFont = new Font("Segoe UI", 12, FontStyle.Bold);

        private Youtuber youtuber;
        public Youtuber Youtuber
        {
            get { return this.youtuber; }
            set { this.youtuber = value; this.Invalidate(); }
        }

        public bool Expanded { get; set; }

        public YoutuberControl(Settings settings, EventHandler mouseOver_SetLastAbstractControl_EventHandler)
            : base(settings, mouseOver_SetLastAbstractControl_EventHandler)
        {
            this.Expanded = settings.YoutuberControlStateOnStartupOrUpdateIsExpanded;
        }

        public override int DefaultControlHeight
        { get { return YoutuberControl.ControlSize == ControlSizes.Large ? 80 : 32; } }

        public override void LoadThumbnail(Settings settings)
        {
            if (settings.ShowYoutuberThumbnailsInsteadOfIcons)
                this.Thumbnail = Utils.GetScaledImageOrScaledDefault(this.youtuber.ThumbnailPath,
                    (int) (this.DefaultControlHeight * 16.0 / 9.0), this.DefaultControlHeight, InterpolationMode.HighQualityBicubic, StaticImages.YoutuberIcon);
            else
                this.Thumbnail = Utils.ScaleImage(StaticImages.YoutuberIcon, (int) (this.DefaultControlHeight * 16.0 / 9.0), this.DefaultControlHeight, InterpolationMode.HighQualityBicubic, false);
            if (settings.ShowYoutuberThumbnailAsCircle)
                Utils.ApplyAlphaMask((Bitmap) this.Thumbnail, StaticImages.IconMask);
        }

        public override string GetTitle
        { get { return this.youtuber.ChannelTitle; } }

        public override string GetSubtitle
        {
            get
            {
                string text = string.Format("{0} ({1})",
                        Utils.Plural("video", this.youtuber.Videos.Count, true),
                        Utils.FormatDuration(this.youtuber.TotalVideoDuration));
                return this.Expanded ? text : string.Format("{0} [click to expand]", text);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(MyGUIs.Background.GetValue(this.mouseIsOver).Brush, this.DefaultControlHeight + 6, 0, this.Width, this.Height);
            e.Graphics.DrawImage(this.Thumbnail, Point.Empty);

            Size titleSize = e.Graphics.MeasureString(this.GetTitle, YoutuberControl.TitleFont).ToSize();
            Size subtitleSize = e.Graphics.MeasureString(this.GetSubtitle, YoutuberControl.SubtitleFont).ToSize();

            e.Graphics.DrawString(this.GetTitle, YoutuberControl.TitleFont,
                MyGUIs.Youtuber.GetValue(this.mouseIsOver).Brush,
                new Point(this.DefaultControlHeight + 8, YoutuberControl.ControlSize == ControlSizes.Large
                    ? this.DefaultControlHeight / 4 - titleSize.Height / 2
                    : this.DefaultControlHeight / 2 - titleSize.Height / 2));

            e.Graphics.DrawString(this.GetSubtitle, YoutuberControl.SubtitleFont,
                MyGUIs.Text.GetValue(this.mouseIsClicked).Brush,
                YoutuberControl.ControlSize == ControlSizes.Large
                    ? new Point(this.DefaultControlHeight + 8 + 2, this.DefaultControlHeight / 2 + this.DefaultControlHeight / 4 - subtitleSize.Height / 2)
                    : new Point(this.Width - subtitleSize.Width - 4, this.DefaultControlHeight / 2 - subtitleSize.Height / 2));
        }
    }

    /// <summary>
    /// Displays information about a VideoSummary.
    /// </summary>
    public class VideoControl : AbstractYoutuberVideoControl
    {
        public static ControlSizes ControlSize = ControlSizes.Large;
        protected static readonly Font TitleFont = new Font("Segoe UI Light", 15, FontStyle.Regular);
        protected static readonly Font SubtitleFont = new Font("Segoe UI", 8, FontStyle.Regular);
        protected static readonly Font StatsFont = new Font("Segoe UI", 12, FontStyle.Bold);

        private bool indented;

        private VideoSummary videoSummary;
        public VideoSummary VideoSummary
        {
            get { return this.videoSummary; }
            set { this.videoSummary = value; this.Invalidate(); }
        }

        public VideoControl(Settings settings, bool indented, EventHandler mouseOver_SetLastAbstractControl_EventHandler)
            : base(settings, mouseOver_SetLastAbstractControl_EventHandler)
        {
            this.indented = indented;
        }

        public override int DefaultControlHeight
        { get { return VideoControl.ControlSize == ControlSizes.Large ? 54 : 30; } }

        public override void LoadThumbnail(Settings settings)
        {
            if (settings.ShowVideoThumbnailsInsteadOfIcons)
                this.Thumbnail = Utils.GetScaledImageOrScaledDefault(this.videoSummary.ThumbnailPath,
                    (int) ((this.DefaultControlHeight - 2) * 16.0 / 9.0), this.DefaultControlHeight - 2, InterpolationMode.HighQualityBicubic, StaticImages.VideoIcon);
            else
                this.Thumbnail = Utils.ScaleImage(StaticImages.VideoIcon, (int) ((this.DefaultControlHeight - 2) * 16.0 / 9.0), this.DefaultControlHeight - 2, InterpolationMode.HighQualityBicubic, false);
        }

        public override string GetTitle
        { get { return this.videoSummary.Title; } }

        public override string GetSubtitle
        {
            get
            {
                return string.Format("Published: {0} / {1}",
                    this.videoSummary.Published.HasValue ? this.videoSummary.Published.Value.ToString("dddd, d'/'MMM'/'yyyy, HH:mm") : "unknown",
                    Utils.Plural("view", (this.videoSummary.Views.HasValue ? (long) this.videoSummary.Views.Value : 0), true));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle thumbAreaRect = new Rectangle(this.indented ? (int) (0.7 * this.DefaultControlHeight) : 0, 0, (int) (this.Height * 16.0 / 9.0), this.Height);
            e.Graphics.FillRectangle(MyGUIs.Background.GetValue(this.mouseIsOver).Brush, thumbAreaRect.Right + 6, 0, this.Width, this.Height);

            bool convertToGrayscale = false;
            switch (this.settings.VideoThumbnailGrayscale)
            {
                case Settings.VideoThumbnailGrayscaleOptions.Never:
                    break;
                case Settings.VideoThumbnailGrayscaleOptions.WhenNew:
                    if (this.videoSummary.NotSeen)
                        convertToGrayscale = true;
                    break;
                case Settings.VideoThumbnailGrayscaleOptions.WhenNotNew:
                    if (!this.videoSummary.NotSeen)
                        convertToGrayscale = true;
                    break;
                case Settings.VideoThumbnailGrayscaleOptions.WhenMarkedToDelete:
                    if (this.videoSummary.MarkedToDelete)
                        convertToGrayscale = true;
                    break;
                case Settings.VideoThumbnailGrayscaleOptions.Always:
                    convertToGrayscale = true;
                    break;
            }
            Image thumb = convertToGrayscale ? Utils.ConvertToGrayscale(this.Thumbnail) : this.Thumbnail;
            e.Graphics.DrawImage(thumb, thumbAreaRect.Left + thumbAreaRect.Width / 2 - thumb.Width / 2, thumbAreaRect.Height / 2 - thumb.Height / 2);

            if (this.videoSummary.NotSeen)
                e.Graphics.DrawImage(StaticImages.NotSeenIcon,
                    new Point(thumbAreaRect.Left - StaticImages.NotSeenIcon.Width / 4,
                        VideoControl.ControlSize == ControlSizes.Large
                            ? thumbAreaRect.Bottom - StaticImages.NotSeenIcon.Height
                            : this.DefaultControlHeight / 2 - StaticImages.NotSeenIcon.Height / 2));

            if (this.videoSummary.MarkedToDelete)
                e.Graphics.DrawImage(StaticImages.DeleteQueryIcon,
                    new Point(thumbAreaRect.Right - StaticImages.DeleteQueryIcon.Width + StaticImages.DeleteQueryIcon.Width / 4,
                        VideoControl.ControlSize == ControlSizes.Large
                            ? thumbAreaRect.Bottom - StaticImages.DeleteQueryIcon.Height
                            : this.DefaultControlHeight / 2 - StaticImages.NotSeenIcon.Height / 2));

            string text = this.GetTitle;
            Font font = VideoControl.TitleFont;
            Brush brush = MyGUIs.Video.GetValue(this.mouseIsOver).Brush;
            Size size = e.Graphics.MeasureString(text, font).ToSize();
            e.Graphics.DrawString(text, font, brush,
                new Point(thumbAreaRect.Right + 8, VideoControl.ControlSize == ControlSizes.Large ? 2 : this.DefaultControlHeight / 2 - size.Height / 2));

            if (VideoControl.ControlSize == ControlSizes.Large)
            {
                text = this.GetSubtitle;
                font = VideoControl.SubtitleFont;
                brush = MyGUIs.Text.GetValue(this.mouseIsClicked).Brush;
                size = e.Graphics.MeasureString(text, font).ToSize();
                e.Graphics.DrawString(text, font, brush, new Point(thumbAreaRect.Right + 8 + 2, this.DefaultControlHeight - size.Height - 4));
            }

            text = Utils.FormatDuration(this.videoSummary.Duration);
            font = VideoControl.StatsFont;
            brush = MyGUIs.Text.GetValue(this.mouseIsClicked).Brush;
            size = e.Graphics.MeasureString(text, font).ToSize();
            e.Graphics.FillRectangle(MyGUIs.Background.GetValue(this.mouseIsOver).Brush,
                VideoControl.ControlSize == ControlSizes.Large
                    ? new Rectangle(this.Width - size.Width - 4, this.DefaultControlHeight - size.Height, size.Width + 4, size.Height + 2)
                    : new Rectangle(this.Width - size.Width - 4, 0, size.Width + 4, this.DefaultControlHeight));
            e.Graphics.DrawString(text, font, brush,
                new Point(this.Width - size.Width, VideoControl.ControlSize == ControlSizes.Large ? this.DefaultControlHeight - size.Height + 2 : this.DefaultControlHeight / 2 - size.Height / 2));
        }
    }

    /// <summary>
    /// Basic manager of AbstractYoutuberVideoControl implementations (VideoControl or YoutuberControl). Should be used alongside a MyScrollPanel.
    /// </summary>
    public class YoutubeVideoControlManager<TYPE> where TYPE : AbstractYoutuberVideoControl
    {
        internal List<TYPE> items;
        private Func<Control, Point, bool, Control> addControlToScrollPanel_Function;
        private Func<AbstractYoutuberVideoControl> createNewControl_Function;

        public YoutubeVideoControlManager(Func<Control, Point, bool, Control> addControlToScrollPanel_Function, Func<TYPE> createNewControl_Function)
        {
            this.items = new List<TYPE>();
            this.addControlToScrollPanel_Function = addControlToScrollPanel_Function;
            this.createNewControl_Function = createNewControl_Function;
        }

        public int GetCurrentCount
        { get { return this.items.Count; } }

        public TYPE GetItemAtIndex(int index)
        {
            while (index >= this.items.Count)
            {
                TYPE item = this.createNewControl_Function() as TYPE;
                this.items.Add(item);
                this.addControlToScrollPanel_Function(item, Point.Empty, false);
            }
            return this.items[index];
        }

        public void ShowSoManyItems(int count)
        {
            for (int index = 0; index < this.items.Count; index++)
                this.items[index].Visible = index < count;
        }

        public void LoadThumbnails(Settings settings)
        {
            for (int index = 0; index < this.items.Count; index++)
                if (this.items[index].Visible)
                    this.items[index].LoadThumbnail(settings);
        }
    }

    /// <summary>
    /// Manages a database-referenced tree list of youtubers and their videos.
    /// </summary>
    public class YoutuberAndVideoManager
    {
        public MyScrollPanel ScrollPanel { get; private set; }

        private Database database;
        private EventHandler youtuberControl_Click;
        private EventHandler videoControl_Click;
        private EventHandler mouseOver_SetLastAbstractControl_EventHandler;
        private YoutubeVideoControlManager<YoutuberControl> youtuberManager;
        private YoutubeVideoControlManager<VideoControl> videoManager;

        public YoutuberAndVideoManager(Database database, EventHandler youtuberControl_Click, EventHandler videoControl_Click, EventHandler mouseOver_SetLastAbstractControl_EventHandler, Panel container)
        {
            this.ScrollPanel = new MyScrollPanel(container, MyScrollBar.ScrollBarPosition.Right, 12, 125);

            this.database = database;
            this.youtuberControl_Click = youtuberControl_Click;
            this.videoControl_Click = videoControl_Click;
            this.mouseOver_SetLastAbstractControl_EventHandler = mouseOver_SetLastAbstractControl_EventHandler;
            this.youtuberManager = new YoutubeVideoControlManager<YoutuberControl>(this.ScrollPanel.AddControl, this.GetNewYoutuberControl);
            this.videoManager = new YoutubeVideoControlManager<VideoControl>(this.ScrollPanel.AddControl, this.GetNewVideoControl);
        }

        public YoutuberControl GetNewYoutuberControl()
        {
            YoutuberControl control = new YoutuberControl(database.Settings, this.mouseOver_SetLastAbstractControl_EventHandler);
            control.Click += this.youtuberControl_Click;
            //control.MouseWheel += this.ScrollPanel.MouseWheelScroll_EventHandler;
            return control;
        }

        public VideoControl GetNewVideoControl()
        {
            VideoControl control = new VideoControl(database.Settings, true, this.mouseOver_SetLastAbstractControl_EventHandler);
            control.Click += this.videoControl_Click;
            control.DoubleClick += this.videoControl_Click;
            //control.MouseWheel += this.ScrollPanel.MouseWheelScroll_EventHandler;
            return control;
        }

        public YoutuberControl GetYoutuberControlByYoutuber(Youtuber youtuber)
        {
            foreach (YoutuberControl yC in this.youtuberManager.items)
                if (yC.Youtuber != null && yC.Youtuber.ID.Equals(youtuber.ID))
                    return yC;
            return null;
        }

        public VideoControl GetVideoControlByVideoSummary(VideoSummary videoSummary)
        {
            foreach (VideoControl vC in this.videoManager.items)
                if (vC.VideoSummary != null && vC.VideoSummary.ID.Equals(videoSummary.ID))
                    return vC;
            return null;
        }

        public void SetYoutuberControlsExpandedState(bool expanded)
        {
            for (int iYC = 0; iYC < this.youtuberManager.GetCurrentCount; iYC++)
                this.youtuberManager.GetItemAtIndex(iYC).Expanded = expanded;
        }

        public void UpdatePanelAndContents(bool resetYoutuberExpandedState)
        {
            // hide existing
            this.youtuberManager.ShowSoManyItems(this.database.Youtubers.Count);
            if (resetYoutuberExpandedState)
                SetYoutuberControlsExpandedState(this.database.Settings.YoutuberControlStateOnStartupOrUpdateIsExpanded);
            this.videoManager.ShowSoManyItems(0);

            // create and set properties for the visible ones
            for (int iY = 0, iV = 0, lastTop = 0, videosToShow = 0; iY < this.database.Youtubers.Count; iY++)
            {
                YoutuberControl yC = this.youtuberManager.GetItemAtIndex(iY);
                yC.Bounds = new Rectangle(0, lastTop, this.ScrollPanel.VisibleSize.Width, yC.DefaultControlHeight);
                yC.Youtuber = this.database.Youtubers[iY];
                lastTop += yC.Height;

                if (yC.Expanded)
                {
                    videosToShow += this.database.Youtubers[iY].Videos.Count;
                    this.videoManager.ShowSoManyItems(videosToShow);

                    for (int iYV = 0; iYV < this.database.Youtubers[iY].Videos.Count; iYV++)
                    {
                        VideoControl vC = this.videoManager.GetItemAtIndex(iV++);
                        vC.Bounds = new Rectangle(0, lastTop, this.ScrollPanel.VisibleSize.Width, vC.DefaultControlHeight);
                        vC.VideoSummary = this.database.Youtubers[iY].Videos[iYV];
                        lastTop += vC.Height;
                    }
                }
            }

            // refresh
            this.ScrollPanel.UpdatePanelSize();
            this.youtuberManager.LoadThumbnails(this.database.Settings);
            this.videoManager.LoadThumbnails(this.database.Settings);
        }

        public MouseEventHandler MouseWheelScroll_EventHandler
        {
            get { return this.ScrollPanel.MouseWheelScroll_EventHandler; }
        }
    }

    /// <summary>
    /// Manages a simple list of videos.
    /// </summary>
    public class SimpleVideoManager
    {
        public MyScrollPanel ScrollPanel { get; private set; }
        private YoutubeVideoControlManager<VideoControl> videoManager;
        private Settings settings;
        private EventHandler videoControl_Click;
        private ListOfIDObjects<VideoSummary> videos;

        private Label statsLabel;

        public SimpleVideoManager(Panel container, Label statsLabel, Settings settings, EventHandler videoControl_Click, ListOfIDObjects<VideoSummary> videos)
        {
            this.ScrollPanel = new MyScrollPanel(container, MyScrollBar.ScrollBarPosition.Right, 0, 100);
            this.videoManager = new YoutubeVideoControlManager<VideoControl>(this.ScrollPanel.AddControl, this.GetNewVideoControl);
            this.settings = settings;
            this.videoControl_Click = videoControl_Click;
            this.videos = videos;

            this.statsLabel = statsLabel;
        }

        public VideoControl GetNewVideoControl()
        {
            VideoControl control = new VideoControl(this.settings, false, null);
            control.Click += this.videoControl_Click;
            control.DoubleClick += this.videoControl_Click;
            //control.MouseWheel += this.ScrollPanel.MouseWheelScroll_EventHandler;
            return control;
        }

        public VideoControl GetVideoControlByVideoSummary(VideoSummary videoSummary)
        {
            foreach (VideoControl vC in this.videoManager.items)
                if (vC.VideoSummary != null && vC.VideoSummary.ID.Equals(videoSummary.ID))
                    return vC;
            return null;
        }

        public void UpdatePanelAndContents()
        {
            this.videoManager.ShowSoManyItems(this.videos.Count);

            // create and set properties 
            for (int iV = 0, lastTop = 0; iV < this.videos.Count; iV++)
            {
                VideoControl vC = this.videoManager.GetItemAtIndex(iV);
                vC.Bounds = new Rectangle(0, lastTop, this.ScrollPanel.VisibleSize.Width, vC.DefaultControlHeight);
                vC.VideoSummary = this.videos[iV];
                lastTop += vC.Height;
            }

            // refresh
            this.statsLabel.Text = this.videos.Count == 0 ? "Playlist" : "Playlist (" + this.videos.Count + ")";
            this.ScrollPanel.UpdatePanelSize();
            this.videoManager.LoadThumbnails(settings);
        }
    }
}
