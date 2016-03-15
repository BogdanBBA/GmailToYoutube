namespace GmailToYoutube
{
    partial class FViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FViewer));
            this.rightP = new GmailToYoutube.BBA.VisualComponents.MyPanel();
            this.webWB = new System.Windows.Forms.WebBrowser();
            this.subsubTitleL = new System.Windows.Forms.Label();
            this.subTitleL = new System.Windows.Forms.Label();
            this.screenshotPB = new System.Windows.Forms.PictureBox();
            this.videoTitleL = new System.Windows.Forms.Label();
            this.leftP = new GmailToYoutube.BBA.VisualComponents.MyPanel();
            this.moreMenuMCMS = new GmailToYoutube.BBA.VisualComponents.MyContextMenuStrip();
            this.aboutTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openWorkspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLocalImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoMCMS = new GmailToYoutube.BBA.VisualComponents.MyContextMenuStrip();
            this.markAsREADToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markToDELETEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.youtuberMCMS = new GmailToYoutube.BBA.VisualComponents.MyContextMenuStrip();
            this.markAllAsREADToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAllToDELETEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAllToPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allMCMS = new GmailToYoutube.BBA.VisualComponents.MyContextMenuStrip();
            this.markAllInDatabaseAsREADToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAllInDatabaseToDELETEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllYoutubersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllYoutubersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskbarIconNI = new System.Windows.Forms.NotifyIcon(this.components);
            this.taskbarIconMCMS = new GmailToYoutube.BBA.VisualComponents.MyContextMenuStrip();
            this.showHideAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuP = new GmailToYoutube.BBA.VisualComponents.MyPanel();
            this.playlistB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.settingsB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.hideB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.allB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.exitB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.moreB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.updateB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.leftHeaderP = new GmailToYoutube.BBA.VisualComponents.MyPanel();
            this.totalsSV = new GmailToYoutube.BBA.VisualComponents.StatsView();
            this.rightHeaderP = new GmailToYoutube.BBA.VisualComponents.MyPanel();
            this.linkChannelB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.linkVideoB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.linkGmailB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.linkYoutubeB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.playlistP = new GmailToYoutube.BBA.VisualComponents.MyPanel();
            this.playlistScrollP = new GmailToYoutube.BBA.VisualComponents.MyPanel();
            this.playlistClearB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.playlistOpenB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.playlistL = new System.Windows.Forms.Label();
            this.rightP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screenshotPB)).BeginInit();
            this.moreMenuMCMS.SuspendLayout();
            this.videoMCMS.SuspendLayout();
            this.youtuberMCMS.SuspendLayout();
            this.allMCMS.SuspendLayout();
            this.taskbarIconMCMS.SuspendLayout();
            this.mainMenuP.SuspendLayout();
            this.leftHeaderP.SuspendLayout();
            this.rightHeaderP.SuspendLayout();
            this.playlistP.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightP
            // 
            this.rightP.Controls.Add(this.webWB);
            this.rightP.Controls.Add(this.subsubTitleL);
            this.rightP.Controls.Add(this.subTitleL);
            this.rightP.Controls.Add(this.screenshotPB);
            this.rightP.Controls.Add(this.videoTitleL);
            this.rightP.DrawPanelAccent = false;
            this.rightP.Location = new System.Drawing.Point(355, 288);
            this.rightP.Name = "rightP";
            this.rightP.Size = new System.Drawing.Size(684, 279);
            this.rightP.TabIndex = 6;
            // 
            // webWB
            // 
            this.webWB.AllowNavigation = false;
            this.webWB.AllowWebBrowserDrop = false;
            this.webWB.IsWebBrowserContextMenuEnabled = false;
            this.webWB.Location = new System.Drawing.Point(462, 116);
            this.webWB.MinimumSize = new System.Drawing.Size(20, 20);
            this.webWB.Name = "webWB";
            this.webWB.Size = new System.Drawing.Size(199, 137);
            this.webWB.TabIndex = 7;
            this.webWB.WebBrowserShortcutsEnabled = false;
            this.webWB.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webWB_DocumentCompleted);
            // 
            // subsubTitleL
            // 
            this.subsubTitleL.AutoSize = true;
            this.subsubTitleL.BackColor = System.Drawing.Color.Transparent;
            this.subsubTitleL.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subsubTitleL.Location = new System.Drawing.Point(240, 155);
            this.subsubTitleL.Name = "subsubTitleL";
            this.subsubTitleL.Size = new System.Drawing.Size(206, 21);
            this.subsubTitleL.TabIndex = 6;
            this.subsubTitleL.Text = "subsubTitleL word word word";
            this.subsubTitleL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // subTitleL
            // 
            this.subTitleL.AutoSize = true;
            this.subTitleL.BackColor = System.Drawing.Color.Transparent;
            this.subTitleL.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subTitleL.Location = new System.Drawing.Point(236, 135);
            this.subTitleL.Name = "subTitleL";
            this.subTitleL.Size = new System.Drawing.Size(186, 20);
            this.subTitleL.TabIndex = 5;
            this.subTitleL.Text = "subTitleL word word word";
            this.subTitleL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // screenshotPB
            // 
            this.screenshotPB.Location = new System.Drawing.Point(462, 32);
            this.screenshotPB.Name = "screenshotPB";
            this.screenshotPB.Size = new System.Drawing.Size(100, 50);
            this.screenshotPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.screenshotPB.TabIndex = 1;
            this.screenshotPB.TabStop = false;
            this.screenshotPB.Click += new System.EventHandler(this.screenshotPB_Click);
            // 
            // videoTitleL
            // 
            this.videoTitleL.BackColor = System.Drawing.Color.Transparent;
            this.videoTitleL.Font = new System.Drawing.Font("Segoe UI Semibold", 32F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videoTitleL.Location = new System.Drawing.Point(114, 15);
            this.videoTitleL.Name = "videoTitleL";
            this.videoTitleL.Size = new System.Drawing.Size(244, 120);
            this.videoTitleL.TabIndex = 0;
            this.videoTitleL.Text = "videoTitleL word word word";
            this.videoTitleL.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // leftP
            // 
            this.leftP.DrawPanelAccent = false;
            this.leftP.Location = new System.Drawing.Point(52, 366);
            this.leftP.Name = "leftP";
            this.leftP.Size = new System.Drawing.Size(297, 77);
            this.leftP.TabIndex = 5;
            // 
            // moreMenuMCMS
            // 
            this.moreMenuMCMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.moreMenuMCMS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.moreMenuMCMS.DropShadowEnabled = false;
            this.moreMenuMCMS.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.moreMenuMCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutTSMI,
            this.openWorkspaceToolStripMenuItem,
            this.editThemeToolStripMenuItem,
            this.deleteLocalImagesToolStripMenuItem});
            this.moreMenuMCMS.Name = "moreMenuMSMS";
            this.moreMenuMCMS.ShowImageMargin = false;
            this.moreMenuMCMS.Size = new System.Drawing.Size(263, 140);
            // 
            // aboutTSMI
            // 
            this.aboutTSMI.Name = "aboutTSMI";
            this.aboutTSMI.ShowShortcutKeys = false;
            this.aboutTSMI.Size = new System.Drawing.Size(262, 34);
            this.aboutTSMI.Text = "About GtY";
            this.aboutTSMI.Click += new System.EventHandler(this.aboutTSMI_Click);
            // 
            // openWorkspaceToolStripMenuItem
            // 
            this.openWorkspaceToolStripMenuItem.Name = "openWorkspaceToolStripMenuItem";
            this.openWorkspaceToolStripMenuItem.Size = new System.Drawing.Size(262, 34);
            this.openWorkspaceToolStripMenuItem.Text = "Open workspace";
            this.openWorkspaceToolStripMenuItem.Click += new System.EventHandler(this.openWorkspaceToolStripMenuItem_Click);
            // 
            // editThemeToolStripMenuItem
            // 
            this.editThemeToolStripMenuItem.Name = "editThemeToolStripMenuItem";
            this.editThemeToolStripMenuItem.Size = new System.Drawing.Size(262, 34);
            this.editThemeToolStripMenuItem.Text = "Edit theme";
            this.editThemeToolStripMenuItem.Click += new System.EventHandler(this.editThemeToolStripMenuItem_Click);
            // 
            // deleteLocalImagesToolStripMenuItem
            // 
            this.deleteLocalImagesToolStripMenuItem.Name = "deleteLocalImagesToolStripMenuItem";
            this.deleteLocalImagesToolStripMenuItem.Size = new System.Drawing.Size(262, 34);
            this.deleteLocalImagesToolStripMenuItem.Text = "Delete local images";
            this.deleteLocalImagesToolStripMenuItem.Click += new System.EventHandler(this.deleteLocalImagesToolStripMenuItem_Click);
            // 
            // videoMCMS
            // 
            this.videoMCMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.videoMCMS.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.videoMCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAsREADToolStripMenuItem,
            this.markToDELETEToolStripMenuItem,
            this.addToPlaylistToolStripMenuItem});
            this.videoMCMS.Name = "moreMenuMSMS";
            this.videoMCMS.Size = new System.Drawing.Size(251, 106);
            // 
            // markAsREADToolStripMenuItem
            // 
            this.markAsREADToolStripMenuItem.Name = "markAsREADToolStripMenuItem";
            this.markAsREADToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.markAsREADToolStripMenuItem.Text = "Mark as READ";
            this.markAsREADToolStripMenuItem.Click += new System.EventHandler(this.markAsREADToolStripMenuItem_Click);
            // 
            // markToDELETEToolStripMenuItem
            // 
            this.markToDELETEToolStripMenuItem.Name = "markToDELETEToolStripMenuItem";
            this.markToDELETEToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.markToDELETEToolStripMenuItem.Text = "Mark to DELETE";
            this.markToDELETEToolStripMenuItem.Click += new System.EventHandler(this.markToDELETEToolStripMenuItem_Click);
            // 
            // addToPlaylistToolStripMenuItem
            // 
            this.addToPlaylistToolStripMenuItem.Name = "addToPlaylistToolStripMenuItem";
            this.addToPlaylistToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.addToPlaylistToolStripMenuItem.Text = "Add to playlist";
            this.addToPlaylistToolStripMenuItem.Click += new System.EventHandler(this.addToPlaylistToolStripMenuItem_Click);
            // 
            // youtuberMCMS
            // 
            this.youtuberMCMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.youtuberMCMS.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.youtuberMCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAllAsREADToolStripMenuItem,
            this.markAllToDELETEToolStripMenuItem,
            this.addAllToPlaylistToolStripMenuItem});
            this.youtuberMCMS.Name = "youtuberMCMS";
            this.youtuberMCMS.Size = new System.Drawing.Size(281, 106);
            // 
            // markAllAsREADToolStripMenuItem
            // 
            this.markAllAsREADToolStripMenuItem.Name = "markAllAsREADToolStripMenuItem";
            this.markAllAsREADToolStripMenuItem.Size = new System.Drawing.Size(280, 34);
            this.markAllAsREADToolStripMenuItem.Text = "Mark all as READ";
            this.markAllAsREADToolStripMenuItem.Click += new System.EventHandler(this.markAllAsREADToolStripMenuItem_Click);
            // 
            // markAllToDELETEToolStripMenuItem
            // 
            this.markAllToDELETEToolStripMenuItem.Name = "markAllToDELETEToolStripMenuItem";
            this.markAllToDELETEToolStripMenuItem.Size = new System.Drawing.Size(280, 34);
            this.markAllToDELETEToolStripMenuItem.Text = "Mark all to DELETE";
            this.markAllToDELETEToolStripMenuItem.Click += new System.EventHandler(this.markAllToDELETEToolStripMenuItem_Click);
            // 
            // addAllToPlaylistToolStripMenuItem
            // 
            this.addAllToPlaylistToolStripMenuItem.Name = "addAllToPlaylistToolStripMenuItem";
            this.addAllToPlaylistToolStripMenuItem.Size = new System.Drawing.Size(280, 34);
            this.addAllToPlaylistToolStripMenuItem.Text = "Add all to playlist";
            this.addAllToPlaylistToolStripMenuItem.Click += new System.EventHandler(this.addAllToPlaylistToolStripMenuItem_Click);
            // 
            // allMCMS
            // 
            this.allMCMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.allMCMS.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.allMCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAllInDatabaseAsREADToolStripMenuItem,
            this.markAllInDatabaseToDELETEToolStripMenuItem,
            this.clearDatabaseToolStripMenuItem,
            this.collapseAllYoutubersToolStripMenuItem,
            this.expandAllYoutubersToolStripMenuItem});
            this.allMCMS.Name = "allMCMS";
            this.allMCMS.Size = new System.Drawing.Size(314, 174);
            // 
            // markAllInDatabaseAsREADToolStripMenuItem
            // 
            this.markAllInDatabaseAsREADToolStripMenuItem.Name = "markAllInDatabaseAsREADToolStripMenuItem";
            this.markAllInDatabaseAsREADToolStripMenuItem.Size = new System.Drawing.Size(313, 34);
            this.markAllInDatabaseAsREADToolStripMenuItem.Text = "Mark all as READ";
            this.markAllInDatabaseAsREADToolStripMenuItem.Click += new System.EventHandler(this.markAllInDatabaseAsREADToolStripMenuItem_Click);
            // 
            // markAllInDatabaseToDELETEToolStripMenuItem
            // 
            this.markAllInDatabaseToDELETEToolStripMenuItem.Name = "markAllInDatabaseToDELETEToolStripMenuItem";
            this.markAllInDatabaseToDELETEToolStripMenuItem.Size = new System.Drawing.Size(313, 34);
            this.markAllInDatabaseToDELETEToolStripMenuItem.Text = "Mark all to DELETE";
            this.markAllInDatabaseToDELETEToolStripMenuItem.Click += new System.EventHandler(this.markAllInDatabaseToDELETEToolStripMenuItem_Click);
            // 
            // clearDatabaseToolStripMenuItem
            // 
            this.clearDatabaseToolStripMenuItem.Name = "clearDatabaseToolStripMenuItem";
            this.clearDatabaseToolStripMenuItem.Size = new System.Drawing.Size(313, 34);
            this.clearDatabaseToolStripMenuItem.Text = "Clear local database";
            this.clearDatabaseToolStripMenuItem.Click += new System.EventHandler(this.clearDatabaseToolStripMenuItem_Click);
            // 
            // collapseAllYoutubersToolStripMenuItem
            // 
            this.collapseAllYoutubersToolStripMenuItem.Name = "collapseAllYoutubersToolStripMenuItem";
            this.collapseAllYoutubersToolStripMenuItem.Size = new System.Drawing.Size(313, 34);
            this.collapseAllYoutubersToolStripMenuItem.Text = "Collapse all Youtubers";
            this.collapseAllYoutubersToolStripMenuItem.Click += new System.EventHandler(this.collapseAllYoutubersToolStripMenuItem_Click);
            // 
            // expandAllYoutubersToolStripMenuItem
            // 
            this.expandAllYoutubersToolStripMenuItem.Name = "expandAllYoutubersToolStripMenuItem";
            this.expandAllYoutubersToolStripMenuItem.Size = new System.Drawing.Size(313, 34);
            this.expandAllYoutubersToolStripMenuItem.Text = "Expand all Youtubers";
            this.expandAllYoutubersToolStripMenuItem.Click += new System.EventHandler(this.expandAllYoutubersToolStripMenuItem_Click);
            // 
            // taskbarIconNI
            // 
            this.taskbarIconNI.ContextMenuStrip = this.taskbarIconMCMS;
            this.taskbarIconNI.Icon = ((System.Drawing.Icon)(resources.GetObject("taskbarIconNI.Icon")));
            this.taskbarIconNI.Text = "Gmail-to-Youtube";
            this.taskbarIconNI.Visible = true;
            this.taskbarIconNI.DoubleClick += new System.EventHandler(this.hideB_Click);
            // 
            // taskbarIconMCMS
            // 
            this.taskbarIconMCMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.taskbarIconMCMS.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.taskbarIconMCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideAppToolStripMenuItem,
            this.eXITToolStripMenuItem});
            this.taskbarIconMCMS.Name = "taskbarIconMCMS";
            this.taskbarIconMCMS.Size = new System.Drawing.Size(256, 72);
            // 
            // showHideAppToolStripMenuItem
            // 
            this.showHideAppToolStripMenuItem.Name = "showHideAppToolStripMenuItem";
            this.showHideAppToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.showHideAppToolStripMenuItem.Text = "Show / hide app";
            this.showHideAppToolStripMenuItem.Click += new System.EventHandler(this.hideB_Click);
            // 
            // eXITToolStripMenuItem
            // 
            this.eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
            this.eXITToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.eXITToolStripMenuItem.Text = "EXIT";
            this.eXITToolStripMenuItem.Click += new System.EventHandler(this.exitB_Click);
            // 
            // mainMenuP
            // 
            this.mainMenuP.Controls.Add(this.playlistB);
            this.mainMenuP.Controls.Add(this.settingsB);
            this.mainMenuP.Controls.Add(this.hideB);
            this.mainMenuP.Controls.Add(this.allB);
            this.mainMenuP.Controls.Add(this.exitB);
            this.mainMenuP.Controls.Add(this.moreB);
            this.mainMenuP.Controls.Add(this.updateB);
            this.mainMenuP.DrawPanelAccent = false;
            this.mainMenuP.Location = new System.Drawing.Point(33, 0);
            this.mainMenuP.Name = "mainMenuP";
            this.mainMenuP.Size = new System.Drawing.Size(1255, 129);
            this.mainMenuP.TabIndex = 11;
            // 
            // playlistB
            // 
            this.playlistB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playlistB.Location = new System.Drawing.Point(635, 12);
            this.playlistB.MakeItBig = true;
            this.playlistB.Name = "playlistB";
            this.playlistB.Size = new System.Drawing.Size(200, 60);
            this.playlistB.TabIndex = 16;
            this.playlistB.Text = "PLAYLIST";
            this.playlistB.UseVisualStyleBackColor = true;
            this.playlistB.Click += new System.EventHandler(this.playlistB_Click);
            // 
            // settingsB
            // 
            this.settingsB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settingsB.Location = new System.Drawing.Point(387, 69);
            this.settingsB.MakeItBig = true;
            this.settingsB.Name = "settingsB";
            this.settingsB.Size = new System.Drawing.Size(200, 60);
            this.settingsB.TabIndex = 15;
            this.settingsB.Text = "SETTINGS";
            this.settingsB.UseVisualStyleBackColor = true;
            this.settingsB.Click += new System.EventHandler(this.settingsB_Click);
            // 
            // hideB
            // 
            this.hideB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hideB.Location = new System.Drawing.Point(181, 69);
            this.hideB.MakeItBig = true;
            this.hideB.Name = "hideB";
            this.hideB.Size = new System.Drawing.Size(200, 60);
            this.hideB.TabIndex = 14;
            this.hideB.Text = "HIDE APP";
            this.hideB.UseVisualStyleBackColor = true;
            this.hideB.Click += new System.EventHandler(this.hideB_Click);
            // 
            // allB
            // 
            this.allB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.allB.Location = new System.Drawing.Point(318, 0);
            this.allB.MakeItBig = true;
            this.allB.Name = "allB";
            this.allB.Size = new System.Drawing.Size(200, 60);
            this.allB.TabIndex = 13;
            this.allB.Text = "[ ALL ]";
            this.allB.UseVisualStyleBackColor = true;
            this.allB.Click += new System.EventHandler(this.allB_Click);
            // 
            // exitB
            // 
            this.exitB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitB.Location = new System.Drawing.Point(992, 66);
            this.exitB.MakeItBig = true;
            this.exitB.Name = "exitB";
            this.exitB.Size = new System.Drawing.Size(200, 60);
            this.exitB.TabIndex = 12;
            this.exitB.Text = "EXIT";
            this.exitB.UseVisualStyleBackColor = true;
            this.exitB.Click += new System.EventHandler(this.exitB_Click);
            // 
            // moreB
            // 
            this.moreB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.moreB.Location = new System.Drawing.Point(593, 69);
            this.moreB.MakeItBig = true;
            this.moreB.Name = "moreB";
            this.moreB.Size = new System.Drawing.Size(200, 60);
            this.moreB.TabIndex = 11;
            this.moreB.Text = "[ MORE ]";
            this.moreB.UseVisualStyleBackColor = true;
            this.moreB.Click += new System.EventHandler(this.moreB_Click);
            // 
            // updateB
            // 
            this.updateB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateB.Location = new System.Drawing.Point(19, 21);
            this.updateB.MakeItBig = true;
            this.updateB.Name = "updateB";
            this.updateB.Size = new System.Drawing.Size(200, 60);
            this.updateB.TabIndex = 10;
            this.updateB.Text = "UPDATE";
            this.updateB.UseVisualStyleBackColor = true;
            this.updateB.Click += new System.EventHandler(this.updateB_Click);
            // 
            // leftHeaderP
            // 
            this.leftHeaderP.Controls.Add(this.totalsSV);
            this.leftHeaderP.DrawPanelAccent = false;
            this.leftHeaderP.Location = new System.Drawing.Point(75, 191);
            this.leftHeaderP.Name = "leftHeaderP";
            this.leftHeaderP.Size = new System.Drawing.Size(297, 77);
            this.leftHeaderP.TabIndex = 12;
            // 
            // totalsSV
            // 
            this.totalsSV.Location = new System.Drawing.Point(23, 4);
            this.totalsSV.Name = "totalsSV";
            this.totalsSV.Size = new System.Drawing.Size(230, 57);
            this.totalsSV.TabIndex = 0;
            this.totalsSV.Text = "statsView1";
            // 
            // rightHeaderP
            // 
            this.rightHeaderP.Controls.Add(this.linkChannelB);
            this.rightHeaderP.Controls.Add(this.linkVideoB);
            this.rightHeaderP.Controls.Add(this.linkGmailB);
            this.rightHeaderP.Controls.Add(this.linkYoutubeB);
            this.rightHeaderP.DrawPanelAccent = false;
            this.rightHeaderP.Location = new System.Drawing.Point(449, 135);
            this.rightHeaderP.Name = "rightHeaderP";
            this.rightHeaderP.Size = new System.Drawing.Size(927, 100);
            this.rightHeaderP.TabIndex = 13;
            // 
            // linkChannelB
            // 
            this.linkChannelB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkChannelB.Location = new System.Drawing.Point(524, 37);
            this.linkChannelB.MakeItBig = true;
            this.linkChannelB.Name = "linkChannelB";
            this.linkChannelB.Size = new System.Drawing.Size(200, 60);
            this.linkChannelB.TabIndex = 7;
            this.linkChannelB.Text = "Channel";
            this.linkChannelB.UseVisualStyleBackColor = true;
            this.linkChannelB.Click += new System.EventHandler(this.linkChannelB_Click);
            // 
            // linkVideoB
            // 
            this.linkVideoB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkVideoB.Location = new System.Drawing.Point(425, 11);
            this.linkVideoB.MakeItBig = true;
            this.linkVideoB.Name = "linkVideoB";
            this.linkVideoB.Size = new System.Drawing.Size(200, 60);
            this.linkVideoB.TabIndex = 6;
            this.linkVideoB.Text = "Video";
            this.linkVideoB.UseVisualStyleBackColor = true;
            this.linkVideoB.Click += new System.EventHandler(this.linkVideoB_Click);
            // 
            // linkGmailB
            // 
            this.linkGmailB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkGmailB.Location = new System.Drawing.Point(13, 11);
            this.linkGmailB.MakeItBig = true;
            this.linkGmailB.Name = "linkGmailB";
            this.linkGmailB.Size = new System.Drawing.Size(200, 60);
            this.linkGmailB.TabIndex = 5;
            this.linkGmailB.Text = "Gmail";
            this.linkGmailB.UseVisualStyleBackColor = true;
            this.linkGmailB.Click += new System.EventHandler(this.linkGmailB_Click);
            // 
            // linkYoutubeB
            // 
            this.linkYoutubeB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkYoutubeB.Location = new System.Drawing.Point(219, 11);
            this.linkYoutubeB.MakeItBig = true;
            this.linkYoutubeB.Name = "linkYoutubeB";
            this.linkYoutubeB.Size = new System.Drawing.Size(200, 60);
            this.linkYoutubeB.TabIndex = 4;
            this.linkYoutubeB.Text = "Youtube";
            this.linkYoutubeB.UseVisualStyleBackColor = true;
            this.linkYoutubeB.Click += new System.EventHandler(this.linkYoutubeB_Click);
            // 
            // playlistP
            // 
            this.playlistP.Controls.Add(this.playlistScrollP);
            this.playlistP.Controls.Add(this.playlistClearB);
            this.playlistP.Controls.Add(this.playlistOpenB);
            this.playlistP.Controls.Add(this.playlistL);
            this.playlistP.DrawPanelAccent = false;
            this.playlistP.Location = new System.Drawing.Point(1086, 270);
            this.playlistP.Name = "playlistP";
            this.playlistP.Size = new System.Drawing.Size(304, 274);
            this.playlistP.TabIndex = 14;
            // 
            // playlistScrollP
            // 
            this.playlistScrollP.DrawPanelAccent = false;
            this.playlistScrollP.Location = new System.Drawing.Point(28, 62);
            this.playlistScrollP.Name = "playlistScrollP";
            this.playlistScrollP.Size = new System.Drawing.Size(234, 77);
            this.playlistScrollP.TabIndex = 10;
            // 
            // playlistClearB
            // 
            this.playlistClearB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playlistClearB.Location = new System.Drawing.Point(48, 130);
            this.playlistClearB.MakeItBig = false;
            this.playlistClearB.Name = "playlistClearB";
            this.playlistClearB.Size = new System.Drawing.Size(200, 30);
            this.playlistClearB.TabIndex = 9;
            this.playlistClearB.Text = "CLEAR";
            this.playlistClearB.UseVisualStyleBackColor = true;
            this.playlistClearB.Click += new System.EventHandler(this.playlistClearB_Click);
            // 
            // playlistOpenB
            // 
            this.playlistOpenB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playlistOpenB.Location = new System.Drawing.Point(48, 211);
            this.playlistOpenB.MakeItBig = true;
            this.playlistOpenB.Name = "playlistOpenB";
            this.playlistOpenB.Size = new System.Drawing.Size(200, 60);
            this.playlistOpenB.TabIndex = 8;
            this.playlistOpenB.Text = "OPEN";
            this.playlistOpenB.UseVisualStyleBackColor = true;
            this.playlistOpenB.Click += new System.EventHandler(this.playlistOpenB_Click);
            // 
            // playlistL
            // 
            this.playlistL.AutoSize = true;
            this.playlistL.BackColor = System.Drawing.Color.Transparent;
            this.playlistL.Font = new System.Drawing.Font("Segoe UI Semibold", 32F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playlistL.Location = new System.Drawing.Point(0, 0);
            this.playlistL.Name = "playlistL";
            this.playlistL.Size = new System.Drawing.Size(162, 59);
            this.playlistL.TabIndex = 1;
            this.playlistL.Text = "Playlist";
            this.playlistL.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // FViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(1440, 579);
            this.ControlBox = false;
            this.Controls.Add(this.playlistP);
            this.Controls.Add(this.rightHeaderP);
            this.Controls.Add(this.leftHeaderP);
            this.Controls.Add(this.mainMenuP);
            this.Controls.Add(this.rightP);
            this.Controls.Add(this.leftP);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gmail-To-Youtube";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FViewer_Load);
            this.rightP.ResumeLayout(false);
            this.rightP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screenshotPB)).EndInit();
            this.moreMenuMCMS.ResumeLayout(false);
            this.videoMCMS.ResumeLayout(false);
            this.youtuberMCMS.ResumeLayout(false);
            this.allMCMS.ResumeLayout(false);
            this.taskbarIconMCMS.ResumeLayout(false);
            this.mainMenuP.ResumeLayout(false);
            this.leftHeaderP.ResumeLayout(false);
            this.rightHeaderP.ResumeLayout(false);
            this.playlistP.ResumeLayout(false);
            this.playlistP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private BBA.VisualComponents.MyPanel leftP;
        private BBA.VisualComponents.MyPanel rightP;
        private BBA.VisualComponents.MyContextMenuStrip moreMenuMCMS;
        private System.Windows.Forms.ToolStripMenuItem aboutTSMI;
        private BBA.VisualComponents.MyContextMenuStrip videoMCMS;
        private System.Windows.Forms.ToolStripMenuItem markAsREADToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markToDELETEToolStripMenuItem;
        private BBA.VisualComponents.MyContextMenuStrip youtuberMCMS;
        private System.Windows.Forms.ToolStripMenuItem markAllAsREADToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAllToDELETEToolStripMenuItem;
        private BBA.VisualComponents.MyContextMenuStrip allMCMS;
        private System.Windows.Forms.ToolStripMenuItem markAllInDatabaseAsREADToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAllInDatabaseToDELETEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllYoutubersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllYoutubersToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon taskbarIconNI;
        private BBA.VisualComponents.MyContextMenuStrip taskbarIconMCMS;
        private System.Windows.Forms.ToolStripMenuItem showHideAppToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.Label subsubTitleL;
        private System.Windows.Forms.Label subTitleL;
        private System.Windows.Forms.ToolStripMenuItem deleteLocalImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWorkspaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editThemeToolStripMenuItem;
        internal System.Windows.Forms.PictureBox screenshotPB;
        internal System.Windows.Forms.Label videoTitleL;
        private BBA.VisualComponents.MyPanel mainMenuP;
        private BBA.VisualComponents.MyButton settingsB;
        private BBA.VisualComponents.MyButton hideB;
        private BBA.VisualComponents.MyButton allB;
        private BBA.VisualComponents.MyButton exitB;
        private BBA.VisualComponents.MyButton moreB;
        private BBA.VisualComponents.MyButton updateB;
        private BBA.VisualComponents.MyPanel leftHeaderP;
        private BBA.VisualComponents.MyPanel rightHeaderP;
        private BBA.VisualComponents.MyButton linkChannelB;
        private BBA.VisualComponents.MyButton linkVideoB;
        private BBA.VisualComponents.MyButton linkGmailB;
        private BBA.VisualComponents.MyButton linkYoutubeB;
        private BBA.VisualComponents.StatsView totalsSV;
        private BBA.VisualComponents.MyPanel playlistP;
        private BBA.VisualComponents.MyButton playlistOpenB;
        internal System.Windows.Forms.Label playlistL;
        private BBA.VisualComponents.MyButton playlistB;
        private BBA.VisualComponents.MyButton playlistClearB;
        private BBA.VisualComponents.MyPanel playlistScrollP;
        private System.Windows.Forms.ToolStripMenuItem addToPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAllToPlaylistToolStripMenuItem;
        internal System.Windows.Forms.WebBrowser webWB;
    }
}