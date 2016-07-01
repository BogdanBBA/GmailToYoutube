using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BBA.VisualComponents;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using GmailToYoutube.BBA.VisualComponents;
using System.Drawing.Drawing2D;

namespace GmailToYoutube
{
    public partial class FViewer : MyForm
    {
        public Database Database { get; private set; }
        public YoutuberAndVideoManager yvManager { get; private set; }
        public SimpleVideoManager playlistManager { get; private set; }
        public VideoSummary SelectedVideo { get; private set; }
        public VideoSummary PlayingVideo { get; private set; }
        public AbstractYoutuberVideoControl LastYoutuberVideoControl { get; internal set; }

        public FViewer(Database database)
            : base()
        {
            InitializeComponent();
            this.RefreshThemeColorsOfOtherwiseNonRefreshableControls();

            this.Database = database;
            this.SelectedVideo = null;
            this.PlayingVideo = null;
        }

        internal void RefreshThemeColorsOfOtherwiseNonRefreshableControls()
        {
            SetControlForegroundColor(MyGUIs.Accent.Highlighted.Color, videoTitleL, playlistL);
            SetControlForegroundColor(MyGUIs.Text.Normal.Color, subTitleL);
            SetControlForegroundColor(MyGUIs.Text.Highlighted.Color, subsubTitleL);
            SetControlBackgroundColor(MyGUIs.Background.Normal.Color, screenshotPB);
        }

        private void FViewer_Load(object sender, EventArgs e)
        {
            int maxButtonImgSize = (int) (linkGmailB.Height * 0.8);
            this.updateB.Image = Utils.ScaleImage(StaticImages.UpdateIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.allB.Image = Utils.ScaleImage(StaticImages.AllIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.playlistB.Image = Utils.ScaleImage(StaticImages.PlaylistIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.hideB.Image = Utils.ScaleImage(StaticImages.HideIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.settingsB.Image = Utils.ScaleImage(StaticImages.SettingsIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.moreB.Image = Utils.ScaleImage(StaticImages.MoreIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.exitB.Image = Utils.ScaleImage(StaticImages.ExitIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.linkGmailB.Image = Utils.ScaleImage(StaticImages.GmailIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.linkYoutubeB.Image = Utils.ScaleImage(StaticImages.YoutubeIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.linkVideoB.Image = Utils.ScaleImage(StaticImages.VideoLargeIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.linkChannelB.Image = Utils.ScaleImage(StaticImages.YoutuberLargeIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);
            this.playlistOpenB.Image = Utils.ScaleImage(StaticImages.PlaylistIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false);

            maxButtonImgSize = (int) (linkGmailB.Height * 0.9);
            StatsView.SetImages(Utils.ScaleImage(StaticImages.GmailIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false),
                Utils.ScaleImage(StaticImages.YoutuberLargeIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false),
                Utils.ScaleImage(StaticImages.VideoLargeIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false),
                Utils.ScaleImage(StaticImages.VideoNewLargeIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false),
                Utils.ScaleImage(StaticImages.VideoMarkedToDeleteLargeIcon, maxButtonImgSize, maxButtonImgSize, InterpolationMode.HighQualityBicubic, false));

            this.yvManager = new YoutuberAndVideoManager(this.Database, this.YoutuberControl_Click, this.VideoControl_Click, this.mouseOver_SetLastAbstractControl_EventHandler, leftP);
            YoutuberControl.ControlSize = this.Database.Settings.ControlSize;
            VideoControl.ControlSize = this.Database.Settings.ControlSize;
            this.MouseWheel += this.yvManager.MouseWheelScroll_EventHandler;

            this.playlistManager = new SimpleVideoManager(this.playlistScrollP, this.playlistL, this.Database.Settings, this.PlaylistVideoControl_Click, this.Database.PlaylistVideos);
            this.playlistP.Visible = this.Database.PlaylistVideos.Count > 0;

            webWB.BringToFront();

            this.ResizeViewerForm();
            this.RefreshInformation(true);
            this.Database.Settings.StillInitializing = false;
        }

        public void ResizeViewerForm()
        {
            const int pad = 8, buttonHeight = 60;

            // main elements 
            mainMenuP.SetBounds(pad, pad, this.Width - 2 * pad, buttonHeight);

            int twPad = pad * (this.playlistP.Visible ? 4 : 3), wUsable = this.Width - twPad;
            int leftW = (int) (wUsable * this.Database.Settings.ListWidthPercentage / 100.0);
            int playlistW = this.playlistP.Visible ? (int) (wUsable * this.Database.Settings.PlaylistWidthPercentage / 100.0) : 0;
            int rightW = wUsable - leftW - playlistW;

            leftHeaderP.SetBounds(pad, mainMenuP.Bottom + pad, leftW, buttonHeight);
            leftP.SetBounds(pad, leftHeaderP.Bottom + pad, leftHeaderP.Width, this.Height - 4 * pad - leftHeaderP.Bottom);
            rightHeaderP.SetBounds(leftHeaderP.Right + pad, leftHeaderP.Top, this.Width - 3 * pad - leftHeaderP.Width, leftHeaderP.Height);
            rightP.SetBounds(rightHeaderP.Left, leftP.Top, rightW, leftP.Height);
            if (this.playlistP.Visible)
                playlistP.SetBounds(rightP.Right + pad, rightHeaderP.Bottom + pad, playlistW, this.Height - 4 * pad - rightHeaderP.Bottom);

            // main menu panel
            Utils.SizeAndPositionControlsInPanel(mainMenuP, new Control[] { updateB, allB, playlistB, hideB, settingsB, moreB, exitB }, true, 0);

            // left header panel
            this.totalsSV.SetBounds(0, 0, leftHeaderP.Width, leftHeaderP.Height);

            // left panel
            this.yvManager.ScrollPanel.ContainerResizeHasHappened();

            // right header panel
            Utils.SizeAndPositionControlsInPanel(rightHeaderP, new Control[] { linkGmailB, linkYoutubeB, linkVideoB, linkChannelB }, true, 0);

            // right panel
            this.ResizeScreenshotAndSubtitles(screenshotPB.Image == null ? new Size(16, 9) : screenshotPB.Image.Size);
            webWB.SetBounds(0, 0, rightP.Width, rightP.Height);

            // playlist panel
            playlistOpenB.SetBounds(0, playlistOpenB.Parent.Height - playlistOpenB.Height, playlistOpenB.Parent.Width, playlistOpenB.Height);
            playlistClearB.SetBounds(0, playlistOpenB.Top - playlistClearB.Height, playlistOpenB.Width, playlistClearB.Height);
            playlistScrollP.SetBounds(0, playlistL.Bottom + pad, playlistOpenB.Width, playlistClearB.Top - playlistL.Bottom - 2 * pad);
            playlistManager.ScrollPanel.ContainerResizeHasHappened();
        }

        /// <summary>Refreshes information on the viewer form, including totals, list panel contents, playlist contents and right info panel (which itself includes resizing screenshot and subtitles).</summary>
        /// <param name="resetYoutuberExpandedState">whether to set youtuber control expanded state to the value specified in the settings</param>
        public void RefreshInformation(bool resetYoutuberExpandedState)
        {
            this.RefreshTotalsInformation();

            this.yvManager.UpdatePanelAndContents(resetYoutuberExpandedState);

            this.playlistManager.UpdatePanelAndContents();

            VideoSummary selectedVideo = this.Database.Videos.GetItem(this.SelectedVideo);
            this.RefreshRightPanel(selectedVideo != null && this.Database.Videos.GetItem(selectedVideo) != null
                ? selectedVideo
                : (this.Database.Youtubers.Count > 0 && this.Database.Youtubers[0].Videos.Count > 0 ? this.Database.Youtubers[0].Videos[0] : null));
        }

        /// <summary>Refreshes totals information on the viewer form. Refresh-wise, is called from RefreshInformation().</summary>
        public void RefreshTotalsInformation()
        {
            totalsSV.RefreshInformation(this.Database.EmailsFromYoutube, this.Database.Youtubers.Count, this.Database.TotalVideos, this.Database.TotalNewVideos, this.Database.TotalMarkedToDeleteVideos, this.Database.TotalVideoDuration);
        }

        /// <summary>Refreshes right info panel information on the viewer form, including resizing screenshot and subtitles. Refresh-wise, is called from RefreshInformation().</summary>
        private void RefreshRightPanel(VideoSummary video)
        {
            this.SelectedVideo = video;
            if (video == null)
            {
                videoTitleL.Text = "";
                screenshotPB.Image = Utils.GetBlankImage(screenshotPB.Size);
                subTitleL.Text = "";
                subsubTitleL.Text = "";
                linkChannelB.Text = "Channel";
                this.webWB.Hide();
            }
            else
            {
                videoTitleL.Text = video.Title.Replace("&", "&&");
                screenshotPB.Image = File.Exists(video.ThumbnailPath) ? new Bitmap(video.ThumbnailPath) : null;
                subTitleL.Text = string.Format("Published {0} / Video resolution: {1} / Duration: {2}",
                    video.Published.HasValue ? video.Published.Value.ToString("dddd, d MMMM yyyy, 'at' HH:mm:ss") : "at some point in time",
                    video.Resolution.Key.HasValue && video.Resolution.Value.HasValue ? (video.Resolution.Key.Value + "x" + video.Resolution.Value.Value) : "never big enough",
                    Utils.FormatDuration(video.Duration));
                subsubTitleL.Text = string.Format("Views: {0} / Likes-dislikes: {1}-{2} / Comments: {3} / Last updated: {4}",
                    video.Views.ToStats(), video.Likes.Key.ToStats(), video.Likes.Value.ToStats(), video.Comments.ToStats(),
                    video.LastUpdated.HasValue ? video.LastUpdated.Value.ToString("ddd, d MMMM, 'at' HH:mm:ss") : "at some unknown point");
                linkChannelB.Text = this.Database.Settings.DisplayChannelNameOnChannelLinkButton ? video.Youtuber.ChannelTitle : "Channel";
                if (this.PlayingVideo != null /*&& this.PlayingVideo.ID.Equals(video.ID)*/)
                    this.webWB.Show();
                else
                {
                    if (this.Database.Settings.CloseInternalBrowserPageWhenVideoUnfocused)
                        OpenInternetPlace(@"about:blank", null);
                    this.webWB.Hide();
                }
            }
            this.ResizeScreenshotAndSubtitles(screenshotPB.Image == null ? new Size(16, 9) : screenshotPB.Image.Size);
        }

        /// <summary>Resizes the screenshot and subtitles in the right info panel on the viewer form. Refresh-wise, is called from RefreshRightPanel().</summary>
        public void ResizeScreenshotAndSubtitles(Size thumbnailSize)
        {
            int pad = mainMenuP.Left;

            videoTitleL.Bounds = new Rectangle(0, 0, rightP.Width, 120);
            subsubTitleL.Location = new Point(0, rightP.Height - subsubTitleL.Height);
            subTitleL.Location = new Point(0, subsubTitleL.Top - subTitleL.Height);
            screenshotPB.SetBounds(0, 120 + pad, rightP.Width, subTitleL.Top - 120 - 2 * pad);
        }

        private void updateB_Click(object sender, EventArgs e)
        {
            new FUpdate(this).ShowDialog(this);
        }

        private void allB_Click(object sender, EventArgs e)
        {
            allMCMS.Show(allB.Left, allB.Bottom);
        }

        private void playlistB_Click(object sender, EventArgs e)
        {
            this.playlistP.Visible = !this.playlistP.Visible;
            this.ResizeViewerForm();
            if (this.playlistP.Visible)
                this.playlistManager.UpdatePanelAndContents();
        }

        private void markAllInDatabaseAsREADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (VideoSummary video in this.Database.Videos)
            {
                video.NotSeen = false;
                VideoControl control = this.yvManager.GetVideoControlByVideoSummary(video);
                if (control != null && control.Visible)
                    control.Invalidate();
            }
            this.RefreshTotalsInformation();
            this.playlistManager.UpdatePanelAndContents();
        }

        private void markAllInDatabaseToDELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Database.Videos.Count == 0)
                return;
            bool uniformDeletionQueryStatus = true;
            for (int iV = 0; iV < this.Database.Videos.Count - 1; iV++)
                if (this.Database.Videos[iV].MarkedToDelete != this.Database.Videos[iV + 1].MarkedToDelete)
                {
                    uniformDeletionQueryStatus = false;
                    break;
                }
            bool newDeletionQueryStatus = uniformDeletionQueryStatus ? !this.Database.Videos[0].MarkedToDelete : true;
            foreach (VideoSummary video in this.Database.Videos)
            {
                video.NotSeen = false;
                video.MarkedToDelete = newDeletionQueryStatus;
                VideoControl control = this.yvManager.GetVideoControlByVideoSummary(video);
                if (control != null && control.Visible)
                    control.Invalidate();
            }
            this.RefreshTotalsInformation();
            this.playlistManager.UpdatePanelAndContents();
        }

        private void clearDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Database.PlaylistVideos.Clear();
            this.Database.ClearDatabase();
            this.Database.SaveToDatabase(Paths.DatabaseFile);
            this.RefreshInformation(true);
        }

        private void collapseAllYoutubersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.yvManager.SetYoutuberControlsExpandedState(false);
            this.yvManager.UpdatePanelAndContents(false);
        }

        private void expandAllYoutubersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.yvManager.SetYoutuberControlsExpandedState(true);
            this.yvManager.UpdatePanelAndContents(false);
        }

        private void mouseOver_SetLastAbstractControl_EventHandler(object sender, EventArgs e)
        {
            this.LastYoutuberVideoControl = sender as AbstractYoutuberVideoControl;
        }

        public void YoutuberControl_Click(object sender, EventArgs e)
        {
            YoutuberControl control = sender as YoutuberControl;
            switch ((e as MouseEventArgs).Button)
            {
                case MouseButtons.Left:
                    control.Expanded = !control.Expanded;
                    this.yvManager.UpdatePanelAndContents(false);
                    break;
                case MouseButtons.Right:
                    youtuberMCMS.Show(Cursor.Position);
                    break;
            }
        }

        public void VideoControl_Click(object sender, EventArgs e)
        {
            VideoControl control = sender as VideoControl;
            switch ((e as MouseEventArgs).Button)
            {
                case MouseButtons.Left:
                    control.VideoSummary.NotSeen = false;
                    switch ((e as MouseEventArgs).Clicks)
                    {
                        case 1:
                            this.RefreshRightPanel(control.VideoSummary);
                            break;
                        case 2:
                            switch (this.Database.Settings.VideoDoubleClickAction)
                            {
                                case Settings.VideoDoubleClickActions.MarkToDelete:
                                    this.markToDELETEToolStripMenuItem_Click(sender, e);
                                    break;
                                case Settings.VideoDoubleClickActions.OpenYoutube:
                                    this.linkYoutubeB_Click(sender, e);
                                    break;
                                case Settings.VideoDoubleClickActions.OpenVideo:
                                    this.linkVideoB_Click(sender, e);
                                    break;
                                case Settings.VideoDoubleClickActions.AddToPlaylist:
                                    this.addToPlaylistToolStripMenuItem_Click(sender, e);
                                    break;
                            }
                            break;
                    }
                    break;
                case MouseButtons.Right:
                    videoMCMS.Show(Cursor.Position);
                    break;
            }
            this.RefreshTotalsInformation();
        }

        public void PlaylistVideoControl_Click(object sender, EventArgs e)
        {
            VideoControl control = sender as VideoControl;
            switch ((e as MouseEventArgs).Button)
            {
                case MouseButtons.Left:
                    switch ((e as MouseEventArgs).Clicks)
                    {
                        case 1:
                            this.RefreshRightPanel(control.VideoSummary);
                            break;
                        case 2:
                            this.Database.PlaylistVideos.RemoveAt(this.Database.PlaylistVideos.GetIndexOfItem(control.VideoSummary));
                            this.playlistManager.UpdatePanelAndContents();
                            break;
                    }
                    break;
                case MouseButtons.Right:
                    break;
            }
        }

        private void markAsREADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VideoControl control = this.LastYoutuberVideoControl as VideoControl;
            control.VideoSummary.NotSeen = false;
            control.Invalidate();
            this.RefreshTotalsInformation();
            this.playlistManager.UpdatePanelAndContents();
        }

        private void markToDELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VideoControl control = this.LastYoutuberVideoControl as VideoControl;
            control.VideoSummary.NotSeen = false;
            control.VideoSummary.MarkedToDelete = !control.VideoSummary.MarkedToDelete;
            control.Invalidate();
            this.RefreshTotalsInformation();
            this.playlistManager.UpdatePanelAndContents();
        }

        private void addToPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VideoControl control = this.LastYoutuberVideoControl as VideoControl;
            control.VideoSummary.NotSeen = false;
            control.Invalidate();
            this.Database.PlaylistVideos.Add(control.VideoSummary);
            this.playlistManager.UpdatePanelAndContents();
        }

        private void markAllAsREADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListOfIDObjects<VideoSummary> videos = (this.LastYoutuberVideoControl as YoutuberControl).Youtuber.Videos;
            foreach (VideoSummary video in videos)
            {
                video.NotSeen = false;
                VideoControl control = this.yvManager.GetVideoControlByVideoSummary(video);
                if (control != null && control.Visible)
                    control.Invalidate();
            }
            this.RefreshTotalsInformation();
            this.playlistManager.UpdatePanelAndContents();
        }

        private void markAllToDELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListOfIDObjects<VideoSummary> videos = (this.LastYoutuberVideoControl as YoutuberControl).Youtuber.Videos;
            if (videos.Count == 0)
                return;
            bool uniformDeletionQueryStatus = true;
            for (int iV = 0; iV < videos.Count - 1; iV++)
                if (videos[iV].MarkedToDelete != videos[iV + 1].MarkedToDelete)
                {
                    uniformDeletionQueryStatus = false;
                    break;
                }
            bool newDeletionQueryStatus = uniformDeletionQueryStatus ? !videos[0].MarkedToDelete : true;
            foreach (VideoSummary video in videos)
            {
                video.NotSeen = false;
                video.MarkedToDelete = newDeletionQueryStatus;
                VideoControl control = this.yvManager.GetVideoControlByVideoSummary(video);
                if (control != null && control.Visible)
                    control.Invalidate();
            }
            this.RefreshTotalsInformation();
            this.playlistManager.UpdatePanelAndContents();
        }

        private void addAllToPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListOfIDObjects<VideoSummary> videos = (this.LastYoutuberVideoControl as YoutuberControl).Youtuber.Videos;
            ListOfIDObjects<VideoSummary> videosCopy = videos.GetDeepCopy();
            videosCopy.SortVideos(Settings.VideoSortingOptions.Chronologically);
            for (int i = videosCopy.Count - 1; i >= 0; i--)
            {
                VideoSummary video = videos.GetItemByID(videosCopy[i].ID);
                video.NotSeen = false;
                VideoControl control = this.yvManager.GetVideoControlByVideoSummary(video);
                if (control != null && control.Visible)
                    control.Invalidate();
                this.Database.PlaylistVideos.Add(video);
                this.playlistManager.UpdatePanelAndContents();
            }
        }

        internal void hideB_Click(object sender, EventArgs e)
        {
            if ((this.Visible && this.Database.Settings.AltTabWindowInsteadOfHiding) || sender is FUpdate)
            {
                WindowHelper.SetForegroundWindow(this.Handle);
                KeyboardHelper.AltTab(sender is FUpdate ? 2 : 1);
            }
            else
                this.Visible = !this.Visible;
        }

        private void settingsB_Click(object sender, EventArgs e)
        {
            new FSettings(this).ShowDialog(this);
        }

        private void moreB_Click(object sender, EventArgs e)
        {
            moreMenuMCMS.Show(moreB.Left, moreB.Bottom);
        }

        private void aboutTSMI_Click(object sender, EventArgs e)
        {
            new FAbout().ShowDialog(this);
        }

        private void openWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Paths.ProgramFilesFolder);
        }

        private void editThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FTheme(this).ShowDialog(this);
        }

        private void deleteLocalImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to do this?\n\nImages for youtuber channels and video thumbnails are downloaded locally upon update at the highest available resolution. "
                + "When videos are deleted from the database, these images are not deleted (intended behavior as of 1.0). If you think you might like to keep some of them, click \"No\" and look through them manually.",
                "Confirm local images deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int nY = 0, nV = 0;

            try
            {
                string[] files = Directory.GetFiles(Paths.YoutuberImagesFolder, "*.jpg");
                foreach (string file in files)
                {
                    FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    nY++;
                }

                foreach (Youtuber youtuber in this.Database.Youtubers)
                {
                    YoutuberControl control = this.yvManager.GetYoutuberControlByYoutuber(youtuber);
                    control.LoadThumbnail(this.Database.Settings);
                    control.Invalidate();
                }
            }
            catch (Exception E)
            { MessageBox.Show("An error occured while deleting youtuber files. The app can continue to function normally.\n\n" + E.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            try
            {
                screenshotPB.Image.Dispose();
                screenshotPB.Image = null;

                string[] files = Directory.GetFiles(Paths.VideoImagesFolder, "*.jpg");
                foreach (string file in files)
                {
                    FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    nV++;
                }

                foreach (VideoSummary video in this.Database.Videos)
                {
                    VideoControl control = this.yvManager.GetVideoControlByVideoSummary(video);
                    if (control != null)
                    {
                        control.LoadThumbnail(this.Database.Settings);
                        if (control.Visible)
                            control.Invalidate();
                    }
                    control = this.playlistManager.GetVideoControlByVideoSummary(video);
                    if (control != null)
                    {
                        control.LoadThumbnail(this.Database.Settings);
                        if (control.Visible)
                            control.Invalidate();
                    }
                }
            }
            catch (Exception E)
            { MessageBox.Show("An error occured while deleting video thumbnail files. The app can continue to function normally.\n\n" + E.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            int nEY = Directory.GetFiles(Paths.YoutuberImagesFolder, "*.jpg").Length, nEV = Directory.GetFiles(Paths.VideoImagesFolder, "*.jpg").Length;
            MessageBox.Show("There were deleted " + nY + " youtuber image(s) and " + nV + " video image(s) successfully.\n\n"
                + "There currently still exist in their respective folders " + nEY + " youtuber image(s) and " + nEV + " video image(s).",
                "Images deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void screenshotPB_Click(object sender, EventArgs e)
        {
            if (this.SelectedVideo != null)
                switch (this.Database.Settings.ThumbnailClickAction)
                {
                    case Settings.ThumbnailClickActions.OpenLocalFile:
                        OpenInternetPlace(this.SelectedVideo.ThumbnailPath, null);
                        break;
                    case Settings.ThumbnailClickActions.OpenImageURL:
                        OpenInternetPlace(this.SelectedVideo.ThumbnailURL, null);
                        break;
                    case Settings.ThumbnailClickActions.OpenYoutube:
                        this.linkYoutubeB_Click(sender, e);
                        break;
                    case Settings.ThumbnailClickActions.OpenVideo:
                        this.linkVideoB_Click(sender, e);
                        break;
                }
        }

        private void linkGmailB_Click(object sender, EventArgs e)
        {
            if (this.SelectedVideo != null)
                OpenInternetPlace(this.SelectedVideo.GmailLink(this.Database.Settings), null);
        }

        private void linkYoutubeB_Click(object sender, EventArgs e)
        {
            if (this.SelectedVideo != null)
            {
                OpenInternetPlace(this.SelectedVideo.YoutubeLink(this.Database.Settings), this.SelectedVideo);
                if (this.Database.Settings.MarkToDeleteVideoAfterWatching)
                {
                    this.SelectedVideo.MarkedToDelete = true;
                    VideoControl control = this.yvManager.GetVideoControlByVideoSummary(this.SelectedVideo);
                    if (control != null && control.Visible)
                        control.Invalidate();
                    this.RefreshTotalsInformation();
                }
            }

        }

        private void linkVideoB_Click(object sender, EventArgs e)
        {
            // https://developers.google.com/youtube/player_parameters?hl=en

            if (this.SelectedVideo != null)
            {
                OpenInternetPlace(this.SelectedVideo.VideoLink(this.Database.Settings), this.SelectedVideo);
                if (this.Database.Settings.MarkToDeleteVideoAfterWatching)
                {
                    this.SelectedVideo.MarkedToDelete = true;
                    VideoControl control = this.yvManager.GetVideoControlByVideoSummary(this.SelectedVideo);
                    if (control != null && control.Visible)
                        control.Invalidate();
                    this.RefreshTotalsInformation();
                }
            }
        }

        private void linkChannelB_Click(object sender, EventArgs e)
        {
            if (this.SelectedVideo != null && this.SelectedVideo.Youtuber != null)
                OpenInternetPlace(this.SelectedVideo.Youtuber.ChannelLink(this.Database.Settings), null);
        }

        private void playlistClearB_Click(object sender, EventArgs e)
        {
            this.Database.PlaylistVideos.Clear();
            this.playlistManager.UpdatePanelAndContents();
        }

        private void playlistOpenB_Click(object sender, EventArgs e)
        {
            switch (this.Database.Settings.PlaylistOpenAction)
            {
                case Settings.PlaylistOpenActions.MarkToDelete:
                    foreach (VideoSummary video in this.Database.PlaylistVideos)
                    {
                        video.MarkedToDelete = true;
                        VideoControl control = this.yvManager.GetVideoControlByVideoSummary(video);
                        if (control != null && control.Visible)
                            control.Invalidate();
                    }
                    break;
            }
            this.playlistManager.UpdatePanelAndContents();
            new FPlaylistCreateAndOpen(this).ShowDialog();
        }

        /// <summary>Opens any URL according to settings (what browser and whether to hide), and manages anything else related to the internal browser.</summary>
        /// <param name="url">the URL to navigate to</param>
        /// <param name="playingVideo">the video that is to be passed if video URL; pass null otherwise</param>
        internal void OpenInternetPlace(string url, VideoSummary playingVideo)
        {
            if (this.Database.Settings.OpenLinksInInternalBrowser)
            {
                if (playingVideo == null || this.PlayingVideo == null || !playingVideo.ID.Equals(this.PlayingVideo.ID))
                {
                    this.PlayingVideo = playingVideo;
                    webWB.AllowNavigation = true;
                    webWB.Navigate(url);
                }
                webWB.Show();
            }
            else
            {
                this.PlayingVideo = null;
                System.Diagnostics.Process.Start(url);
                if (this.Database.Settings.HideAppWhileWatchingVideo && !this.Database.Settings.AltTabWindowInsteadOfHiding)
                    this.hideB_Click(null, null);
            }
        }

        private void webWB_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webWB.AllowNavigation = this.Database.Settings.AllowInternalBrowserNavigation;
        }

        private void exitB_Click(object sender, EventArgs e)
        {
            if (this.Database.Settings.OpenLinksInInternalBrowser)
                OpenInternetPlace(@"about:blank", null);
            taskbarIconNI.Dispose();
            string saveResult = this.Database.SaveToDatabase(Paths.DatabaseFile);
            if (!saveResult.Equals(""))
                MessageBox.Show(saveResult, "Database save ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
