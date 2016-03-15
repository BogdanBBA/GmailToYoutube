using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BBA.VisualComponents;
using GmailToYoutube.BBA.VisualComponents;
using Newtonsoft.Json;

namespace GmailToYoutube
{
    public partial class FLogin : MyForm
    {
        private Database database;

        public FLogin()
            : base()
        {
            InitializeComponent();

            SetControlForegroundColor(MyGUIs.Accent.Highlighted.Color, label1);
            SetControlForegroundColor(MyGUIs.Text.Normal.Color, stepL);
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            string checkPathsResult = Paths.CheckPaths(true);
            if (!checkPathsResult.Equals(""))
            {
                MessageBox.Show(checkPathsResult, "Initialization ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            MyGUIs.Initialize();
            SetControlForegroundColor(MyGUIs.Accent.Highlighted.Color, label1);
            SetControlForegroundColor(MyGUIs.Text.Normal.Color, stepL);

            this.database = new Database();
            string databaseLoadResult = this.database.LoadFromDatabase(Paths.DatabaseFile);
            if (!databaseLoadResult.Equals(""))
            {
                MessageBox.Show(databaseLoadResult, "Database load ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            this.stepL.Text = "Inialization complete. Login to continue.";

            if (this.database.Settings.AutomaticallyLogInAtStartup)
                this.loginB_Click(null, null);
        }

        private void loginB_Click(object sender, EventArgs e)
        {
            initializeBgW.RunWorkerAsync();
        }

        private void initializeBgW_DoWork(object sender, DoWorkEventArgs e)
        {
            initializeBgW.ReportProgress(0, "Initializing connection data...");
            ConnectionUtils.InitializeData();

            initializeBgW.ReportProgress(50, "Loading static app resources...");
            string imageLoadResult = StaticImages.LoadStaticImages();
            if (!imageLoadResult.Equals(""))
            {
                MessageBox.Show(imageLoadResult, "Static images load ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            initializeBgW.ReportProgress(100, "Done.");
        }

        private void initializeBgW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.stepPB.Value = e.ProgressPercentage;
            this.stepL.Text = e.UserState as string;
        }

        private void initializeBgW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FViewer viewerForm = new FViewer(this.database);
            viewerForm.Show();
            viewerForm.Focus();
            this.Hide();
        }

        private void exitB_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
