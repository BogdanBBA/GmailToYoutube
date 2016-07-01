using BBA.VisualComponents;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using GmailToYoutube.BBA.VisualComponents;

namespace GmailToYoutube
{
    public partial class FUpdate : MyForm
    {
        private FViewer viewerForm;

        // construct
        public FUpdate(FViewer viewerForm)
        {
            InitializeComponent();
            this.viewerForm = viewerForm;

            SetControlForegroundColor(MyGUIs.Accent.Highlighted.Color,
                label1);
            SetControlForegroundColor(MyGUIs.Text.Normal.Color,
                stepL);
        }

        // on load
        private void FUpdate_Load(object sender, EventArgs e)
        {
            this.workBgW.RunWorkerAsync(this.viewerForm.Database);
        }

        // work
        private void workBgW_DoWork(object sender, DoWorkEventArgs e)
        {
            // variable declarations and initialization

            workBgW.ReportProgress(0, "Initializing update...");

            const int pageEmailResults = 100;
            int count = 0;
            Database database = e.Argument as Database;
            WebClient webClient = new WebClient();

            // trash queried emails

            ListOfIDObjects<VideoSummary> videosToDelete = database.GetToDeleteVideos();
            foreach (VideoSummary video in videosToDelete)
            {
                workBgW.ReportProgress(0, "Trashing queried emails... (" + (++count) + " of " + videosToDelete.Count + ")");
                database.RemoveVideoFromVideoListAndYoutuberVideoList(video);
                UsersResource.MessagesResource.TrashRequest trashRequest = ConnectionUtils.GmailService.Users.Messages.Trash("me", video.EmailID);
                trashRequest.Execute();
            }

            // read emails

            count = 0;
            workBgW.ReportProgress(0, "Reading Gmail emails...");

            List<EmailSummary> youtubeUploadEmails = new List<EmailSummary>();
            UsersResource.MessagesResource.ListRequest messageRequest = ConnectionUtils.GmailService.Users.Messages.List("me");
            messageRequest.MaxResults = pageEmailResults;
            messageRequest.Q = "from:noreply@youtube.com label:inbox";
            List<Message> messages = new List<Message>();

            do
            {
                ListMessagesResponse messageRequestResponse = messageRequest.Execute();
                if (messageRequestResponse.Messages != null)
                    messages.AddRange(messageRequestResponse.Messages);
                messageRequest.PageToken = messageRequestResponse.NextPageToken;
            }
            while (messageRequest.PageToken != null);

            database.EmailsFromYoutube = messages.Count;

            foreach (Message iMessage in messages)
            {
                workBgW.ReportProgress(0, "Parsing Gmail emails (" + (++count) + " of " + messages.Count + ")");
                Message message = null;
                if (database.Settings.ForceUpdateOldVideoStats || database.GetVideoByEmailID(iMessage.Id) == null)
                {
                    message = ConnectionUtils.GmailService.Users.Messages.Get("me", iMessage.Id).Execute();
                    if (Utils.MessageIsYoutubeUpload(message))
                        youtubeUploadEmails.Add(new EmailSummary(message));
                }
                if (database.Settings.MarkEmailsAsRead)
                {
                    if (message == null)
                        message = ConnectionUtils.GmailService.Users.Messages.Get("me", iMessage.Id).Execute();
                    ConnectionUtils.GmailService.Users.Messages.Modify(
                        new ModifyMessageRequest() { RemoveLabelIds = new string[] { "UNREAD" } }, "me", message.Id).Execute();
                }
            }

            // update videos

            count = 0;

            foreach (EmailSummary email in youtubeUploadEmails)
            {
                workBgW.ReportProgress(0, "Updating videos (from email " + (++count) + " of " + youtubeUploadEmails.Count + ")");
                VideosResource.ListRequest request = ConnectionUtils.YouTubeService.Videos.List("snippet,contentDetails,statistics");
                request.Id = Utils.ExtractVideoIDFromMessageBody(email.Body);
                VideoListResponse response = request.Execute();
                if (response.Items.Count == 0)
                {
                    VideoSummary video = database.Videos.GetItemByID(request.Id);
                    if (video != null)
                        database.RemoveVideoFromVideoListAndYoutuberVideoList(video);
                    continue;
                }
                Video googleVideo = response.Items[0];
                VideoSummary videoSummary = new VideoSummary(email.ID, googleVideo.Id, DateTime.Now, googleVideo);
                database.AddVideo(videoSummary);
            }

            // update video thumbnails

            List<VideoSummary> videosToUpdateThumbnailFor = database.Settings.ForceUpdateVideoAndYoutuberImages
                ? database.Videos
                : database.Videos.FindAll(video => !File.Exists(video.ThumbnailPath));
            count = 0;

            foreach (VideoSummary video in videosToUpdateThumbnailFor)
                try
                {
                    workBgW.ReportProgress(0, "Updating video thumbnails (" + (++count) + " of " + videosToUpdateThumbnailFor.Count + ")");
                    webClient.DownloadFile(video.ThumbnailURL, video.ThumbnailPath);
                }
                catch (Exception) { }

            // update youtubers

            count = 0;

            for (int iY = 0; iY < database.Youtubers.Count; iY++)
            {
                workBgW.ReportProgress(0, "Updating youtubers (" + (++count) + " of " + database.Youtubers.Count + ")");
                Youtuber youtuber = database.Youtubers[iY];
                ChannelsResource.ListRequest request = ConnectionUtils.YouTubeService.Channels.List("snippet,contentDetails,statistics");
                request.Id = youtuber.ID;
                ChannelListResponse response = request.Execute();
                if (response.Items.Count == 0)
                {
                    database.Youtubers.RemoveAt(iY--);
                    continue;
                }
                Channel channel = response.Items[0];
                Utils.UpdateYoutuberInfo(ref youtuber, channel, database.Videos);
            }

            for (int iY = 0; iY < database.Youtubers.Count; iY++)
                if (database.Youtubers[iY].Videos.Count == 0)
                    database.Youtubers.RemoveAt(iY--);

            // update youtuber thumbnails

            List<Youtuber> youtubersToUpdateThumbnailFor = database.Settings.ForceUpdateVideoAndYoutuberImages
                ? database.Youtubers
                : database.Youtubers.FindAll(youtuber => !File.Exists(youtuber.ThumbnailPath));
            count = 0;

            foreach (Youtuber youtuber in youtubersToUpdateThumbnailFor)
                try
                {
                    workBgW.ReportProgress(0, "Updating youtuber thumbnails (" + (++count) + " of " + youtubersToUpdateThumbnailFor.Count + ")");
                    webClient.DownloadFile(youtuber.ThumbnailURL, youtuber.ThumbnailPath);
                }
                catch (Exception) { }

            // organize database

            workBgW.ReportProgress(0, "Organizing database...");

            database.Videos.SortVideos(database.Settings.VideoSorting);
            foreach (Youtuber youtuber in database.Youtubers)
                youtuber.Videos.SortVideos(database.Settings.VideoSorting);
            database.Youtubers.SortYoutubers(database.Settings.YoutuberSorting);

            for (int index = 0; index < database.PlaylistVideos.Count; index++)
                if (database.Videos.GetItem(database.PlaylistVideos[index]) == null)
                    database.PlaylistVideos.RemoveAt(index--);
            // should I check for viewerForm.PlayingVideo? it shouldn't be accessed anymore

            string saveResult = this.viewerForm.Database.SaveToDatabase(Paths.DatabaseFile);
            if (!saveResult.Equals(""))
                System.Windows.Forms.MessageBox.Show(saveResult, "Database save ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            // done         

            workBgW.ReportProgress(100, "Update finished. Refreshing controls...");
        }

        // report progress
        private void workBgW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            stepL.Text = e.UserState as string;
        }

        // completed
        private void workBgW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.closeT.Enabled = true;
        }

        // close
        private void closeT_Tick(object sender, EventArgs e)
        {
            this.closeT.Enabled = false;
            this.viewerForm.RefreshInformation(true);
            this.Close();
        }

        private void hideB_Click(object sender, EventArgs e)
        {
            this.viewerForm.hideB_Click(this, null);
        }
    }
}
