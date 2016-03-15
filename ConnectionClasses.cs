using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace GmailToYoutube
{
    public static class ConnectionUtils
    {
        public static readonly string[] ApplicationScopes = { GmailService.Scope.MailGoogleCom, YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeForceSsl, YouTubeService.Scope.YoutubeReadonly };
        public const string ApplicationName = "Gmail-To-Youtube Organizer";

        public static UserCredential UserCredential { get; internal set; }
        public static GmailService GmailService { get; internal set; }
        public static YouTubeService YouTubeService { get; internal set; }

        /// <summary>Initializes the user credential and API service.</summary>
        public static void InitializeData()
        {
            using (var stream = new FileStream(Paths.ApiFile, FileMode.Open, FileAccess.Read))
            {
                string credentialSaveFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".credentials/" + ApplicationName);

                ConnectionUtils.UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    ApplicationScopes,
                    "currently-logged-in-user-on-this-computer",
                    CancellationToken.None,
                    new FileDataStore(credentialSaveFilePath, true)).Result;

                Console.WriteLine("Credential file saved to: " + credentialSaveFilePath);
            }

            ConnectionUtils.GmailService = new GmailService(new BaseClientService.Initializer()
               {
                   HttpClientInitializer = UserCredential,
                   ApplicationName = ApplicationName
               });
            ConnectionUtils.YouTubeService = new YouTubeService(new BaseClientService.Initializer()
               {
                   HttpClientInitializer = UserCredential,
                   ApplicationName = ApplicationName
               });
        }

        public static void TestConnection()
        {
            UsersResource.LabelsResource.ListRequest labelRequest = GmailService.Users.Labels.List("me");
            IList<Label> labels = labelRequest.Execute().Labels;
            Console.WriteLine(" * Labels:");
            if (labels == null || labels.Count == 0)
                Console.WriteLine(" * No labels found.");
            else
                foreach (var labelItem in labels)
                    Console.WriteLine(" - {0}", labelItem.Name);
            Console.WriteLine(" * Done.\n");

            UsersResource.MessagesResource.ListRequest messageRequest = GmailService.Users.Messages.List("me");
            messageRequest.MaxResults = 500;
            ListMessagesResponse messagesResponse = messageRequest.Execute();
            Console.WriteLine(" * Messages:");
            if (messagesResponse.Messages == null || messagesResponse.Messages.Count == 0)
                Console.WriteLine(" * No messages found.");
            else
                foreach (var messageItem in messagesResponse.Messages)
                {
                    Message message = GmailService.Users.Messages.Get("me", messageItem.Id).Execute();
                    foreach (MessagePartHeader header in message.Payload.Headers)
                        if (header.Name.Equals("Subject"))
                        {
                            Console.WriteLine(" - {0}: {1}\\n\n", message.Id, header.Value);
                            if (message.Payload.Parts == null || message.Payload.Parts.Count == 0)
                                Console.WriteLine(Encoding.UTF8.GetString(Convert.FromBase64String(message.Payload.Body.Data.Replace('-', '+').Replace('_', '/'))));
                            else
                                Console.WriteLine(Encoding.UTF8.GetString(Convert.FromBase64String(message.Payload.Parts[0].Body.Data.Replace('-', '+').Replace('_', '/'))));
                            Console.WriteLine();
                        }
                }
            Console.WriteLine(" * Done.\n");
        }
    }
}
