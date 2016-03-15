using BBA.VisualComponents;
using System;
using System.Drawing;
using GmailToYoutube.BBA.VisualComponents;

namespace GmailToYoutube
{
    public partial class FSettings : MyForm
    {
        private FViewer viewerForm;

        public FSettings(FViewer viewerForm)
        {
            InitializeComponent();
            this.viewerForm = viewerForm;

            SetControlForegroundColor(MyGUIs.Accent.Highlighted.Color,
                label1);
            SetControlForegroundColor(MyGUIs.Accent.Normal.Color,
                label8, label9, label10, label11);
            SetControlForegroundColor(MyGUIs.Text.Normal.Color,
                autoLoginAtStartupChB, collapseYoutubersOnStartupChB, openLinksInInternalBrowserChB,
                forceUpdateOldVideosChB, forceThumbnailUpdateChB, markEmailsAsReadChB,
                showYoutuberThumbnailAsCircleChB, showYoutuberThumbnailChB, showVideoThumbnailChB,
                displayChannelNameOnChannelLinkButtonChB, label31, label24, label12, label14,
                label34, label26, label37, label17, label19, label22, label39, label42,
                hideWhileWatchingChB, markToDeleteVideosAfterWatchingChB, hideWindowInsteadOfAltTabChB,
                openLinksInInternalBrowserChB, allowInternalBrowserNavigationChB, closeInternalBrowserPageWhenVideoUnfocusedChB);
            SetControlForegroundColor(MyGUIs.Text.Highlighted.Color,
                label20, label27, label21, label18, label16, label36, label25, label33,
                label15, label13, label23, label30, label32, label4, label5, label28,
                label29, label3, label6, label35, label7, label2, label38, label40, label41,
                label35, label43, label44);
            SetControlBackgroundColor(MyGUIs.Background.Normal.Color,
                listWidthPercentageTrB, playlistWidthPercentageTrB);
        }

        private void FSettings_Load(object sender, EventArgs e)
        {
            controlSizeCB.Items.AddRange(AbstractYoutuberVideoControl.ControlSizesDescriptions);
            videoResolutionCB.Items.AddRange(Settings.VideoResolutionDescriptions);
            grayscaleVideoThumbnailChB.Items.AddRange(Settings.VideoThumbnailGrayscaleOptionsDescriptions);
            youtuberSortingCB.Items.AddRange(Settings.YoutuberSortingOptionsDescriptions);
            videoSortingCB.Items.AddRange(Settings.VideoSortingOptionsDescriptions);
            videoDoubleClickActionCB.Items.AddRange(Settings.VideoDoubleClickActionDescriptions);
            playlistOpenCB.Items.AddRange(Settings.PlaylistOpenActionDescriptions);
            thumbnailClickCB.Items.AddRange(Settings.ThumbnailClickActionDescriptions);

            Settings sett = this.viewerForm.Database.Settings;

            autoLoginAtStartupChB.Checked = sett.AutomaticallyLogInAtStartup;
            forceUpdateOldVideosChB.Checked = sett.ForceUpdateOldVideoStats;
            forceThumbnailUpdateChB.Checked = sett.ForceUpdateVideoAndYoutuberImages;
            markEmailsAsReadChB.Checked = sett.MarkEmailsAsRead;
            controlSizeCB.SelectedIndex = controlSizeCB.Items.IndexOf(sett.ControlSize.ToString());
            collapseYoutubersOnStartupChB.Checked = sett.YoutuberControlStateOnStartupOrUpdateIsExpanded;
            showYoutuberThumbnailAsCircleChB.Checked = sett.ShowYoutuberThumbnailAsCircle;
            showYoutuberThumbnailChB.Checked = sett.ShowYoutuberThumbnailsInsteadOfIcons;
            showVideoThumbnailChB.Checked = sett.ShowVideoThumbnailsInsteadOfIcons;
            displayChannelNameOnChannelLinkButtonChB.Checked = sett.DisplayChannelNameOnChannelLinkButton;
            grayscaleVideoThumbnailChB.SelectedIndex = grayscaleVideoThumbnailChB.Items.IndexOf(Settings.VideoThumbnailGrayscaleOptionsDescriptions[(int) sett.VideoThumbnailGrayscale]);
            videoSortingCB.SelectedIndex = videoSortingCB.Items.IndexOf(Settings.VideoSortingOptionsDescriptions[(int) sett.VideoSorting]);
            youtuberSortingCB.SelectedIndex = youtuberSortingCB.Items.IndexOf(Settings.YoutuberSortingOptionsDescriptions[(int) sett.YoutuberSorting]);
            scrollAmountNUD.Value = sett.ListScrollAmount;
            listWidthPercentageTrB.Value = sett.ListWidthPercentage;
            playlistWidthPercentageTrB.Value = sett.PlaylistWidthPercentage;
            videoDoubleClickActionCB.SelectedIndex = videoDoubleClickActionCB.Items.IndexOf(Settings.VideoDoubleClickActionDescriptions[(int) sett.VideoDoubleClickAction]);
            playlistOpenCB.SelectedIndex = playlistOpenCB.Items.IndexOf(Settings.PlaylistOpenActionDescriptions[(int) sett.PlaylistOpenAction]);
            videoResolutionCB.SelectedIndex = sett.VideoResolutionIndex;
            secondsToSkipNUD.Value = sett.SecondsToSkipInVideo;
            thumbnailClickCB.SelectedIndex = thumbnailClickCB.Items.IndexOf(Settings.ThumbnailClickActionDescriptions[(int) sett.ThumbnailClickAction]);
            hideWhileWatchingChB.Checked = sett.HideAppWhileWatchingVideo;
            openLinksInInternalBrowserChB.Checked = sett.OpenLinksInInternalBrowser;
            allowInternalBrowserNavigationChB.Checked = sett.AllowInternalBrowserNavigation;
            closeInternalBrowserPageWhenVideoUnfocusedChB.Checked = sett.CloseInternalBrowserPageWhenVideoUnfocused;
            markToDeleteVideosAfterWatchingChB.Checked = sett.MarkToDeleteVideoAfterWatching;
            hideWindowInsteadOfAltTabChB.Checked = sett.HideWindowInsteadOfAltTabbing;

            listWidthPercentageTrB_Scroll(null, null);
            playlistWidthPercentageTrB_Scroll(null, null);
        }

        private void listWidthPercentageTrB_Scroll(object sender, EventArgs e)
        {
            label14.Text = string.Format("List width ({0}%)", listWidthPercentageTrB.Value);
        }

        private void playlistWidthPercentageTrB_Scroll(object sender, EventArgs e)
        {
            label34.Text = string.Format("Playlist width ({0}%)", playlistWidthPercentageTrB.Value);
        }

        private void exitB_Click(object sender, EventArgs e)
        {
            Settings sett = this.viewerForm.Database.Settings;

            int oldListWidthPercentage = sett.ListWidthPercentage;
            int oldPlaylistWidthPercentage = sett.PlaylistWidthPercentage;
            Settings.YoutuberSortingOptions oldYoutuberSorting = sett.YoutuberSorting;
            Settings.VideoSortingOptions oldVideoSorting = sett.VideoSorting;

            sett.AutomaticallyLogInAtStartup = autoLoginAtStartupChB.Checked;
            sett.ForceUpdateOldVideoStats = forceUpdateOldVideosChB.Checked;
            sett.ForceUpdateVideoAndYoutuberImages = forceThumbnailUpdateChB.Checked;
            sett.MarkEmailsAsRead = markEmailsAsReadChB.Checked;
            sett.ControlSize = (AbstractYoutuberVideoControl.ControlSizes) Enum.Parse(typeof(AbstractYoutuberVideoControl.ControlSizes), controlSizeCB.Items[controlSizeCB.SelectedIndex] as string);
            sett.YoutuberControlStateOnStartupOrUpdateIsExpanded = collapseYoutubersOnStartupChB.Checked;
            sett.ShowYoutuberThumbnailAsCircle = showYoutuberThumbnailAsCircleChB.Checked;
            sett.ShowYoutuberThumbnailsInsteadOfIcons = showYoutuberThumbnailChB.Checked;
            sett.ShowVideoThumbnailsInsteadOfIcons = showVideoThumbnailChB.Checked;
            sett.DisplayChannelNameOnChannelLinkButton = displayChannelNameOnChannelLinkButtonChB.Checked;
            sett.VideoThumbnailGrayscale = (Settings.VideoThumbnailGrayscaleOptions) grayscaleVideoThumbnailChB.SelectedIndex;
            sett.VideoSorting = (Settings.VideoSortingOptions) videoSortingCB.SelectedIndex;
            sett.YoutuberSorting = (Settings.YoutuberSortingOptions) youtuberSortingCB.SelectedIndex;
            sett.ListScrollAmount = (int) scrollAmountNUD.Value;
            sett.ListWidthPercentage = listWidthPercentageTrB.Value;
            sett.PlaylistWidthPercentage = playlistWidthPercentageTrB.Value;
            sett.VideoDoubleClickAction = (Settings.VideoDoubleClickActions) videoDoubleClickActionCB.SelectedIndex;
            sett.PlaylistOpenAction = (Settings.PlaylistOpenActions) playlistOpenCB.SelectedIndex;
            sett.VideoResolutionIndex = videoResolutionCB.SelectedIndex;
            sett.SecondsToSkipInVideo = (int) secondsToSkipNUD.Value;
            sett.ThumbnailClickAction = (Settings.ThumbnailClickActions) thumbnailClickCB.SelectedIndex;
            sett.HideAppWhileWatchingVideo = hideWhileWatchingChB.Checked;
            sett.OpenLinksInInternalBrowser = openLinksInInternalBrowserChB.Checked;
            sett.AllowInternalBrowserNavigation = allowInternalBrowserNavigationChB.Checked;
            sett.CloseInternalBrowserPageWhenVideoUnfocused = closeInternalBrowserPageWhenVideoUnfocusedChB.Checked;
            sett.HideWindowInsteadOfAltTabbing = hideWindowInsteadOfAltTabChB.Checked;
            sett.MarkToDeleteVideoAfterWatching = markToDeleteVideosAfterWatchingChB.Checked;

            string saveResult = this.viewerForm.Database.SaveToDatabase(Paths.DatabaseFile);
            if (!saveResult.Equals(""))
                System.Windows.Forms.MessageBox.Show(saveResult, "Database save ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            if (oldListWidthPercentage != sett.ListWidthPercentage || oldPlaylistWidthPercentage != sett.PlaylistWidthPercentage)
                this.viewerForm.ResizeViewerForm();
            if (oldYoutuberSorting != sett.YoutuberSorting)
            {
                this.viewerForm.Database.Youtubers.SortYoutubers(sett.YoutuberSorting);
                this.viewerForm.Database.SaveToDatabase(Paths.DatabaseFile);
            }
            if (oldVideoSorting != sett.VideoSorting)
            {
                this.viewerForm.Database.Videos.SortVideos(sett.VideoSorting);
                foreach (Youtuber youtuber in this.viewerForm.Database.Youtubers)
                    youtuber.Videos.SortVideos(sett.VideoSorting);
                this.viewerForm.Database.SaveToDatabase(Paths.DatabaseFile);
            }
            this.viewerForm.yvManager.ScrollPanel.ScrollAmountInPixels = sett.ListScrollAmount;
            YoutuberControl.ControlSize = sett.ControlSize;
            VideoControl.ControlSize = sett.ControlSize;
            this.viewerForm.webWB.AllowNavigation = sett.AllowInternalBrowserNavigation;
            this.viewerForm.RefreshInformation(false);

            this.Close();
        }
    }
}
