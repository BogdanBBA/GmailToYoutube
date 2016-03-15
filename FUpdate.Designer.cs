namespace GmailToYoutube
{
    partial class FUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FUpdate));
            this.workBgW = new System.ComponentModel.BackgroundWorker();
            this.closeT = new System.Windows.Forms.Timer(this.components);
            this.stepPB = new System.Windows.Forms.ProgressBar();
            this.stepL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.myLoadingIcon1 = new GmailToYoutube.BBA.VisualComponents.MyLoadingIcon();
            this.SuspendLayout();
            // 
            // workBgW
            // 
            this.workBgW.WorkerReportsProgress = true;
            this.workBgW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workBgW_DoWork);
            this.workBgW.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workBgW_ProgressChanged);
            this.workBgW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workBgW_RunWorkerCompleted);
            // 
            // closeT
            // 
            this.closeT.Interval = 1000;
            this.closeT.Tick += new System.EventHandler(this.closeT_Tick);
            // 
            // stepPB
            // 
            this.stepPB.Location = new System.Drawing.Point(59, 138);
            this.stepPB.MarqueeAnimationSpeed = 10;
            this.stepPB.Name = "stepPB";
            this.stepPB.Size = new System.Drawing.Size(398, 32);
            this.stepPB.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.stepPB.TabIndex = 3;
            this.stepPB.Visible = false;
            // 
            // stepL
            // 
            this.stepL.AutoSize = true;
            this.stepL.BackColor = System.Drawing.Color.Transparent;
            this.stepL.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepL.Location = new System.Drawing.Point(63, 92);
            this.stepL.Name = "stepL";
            this.stepL.Size = new System.Drawing.Size(129, 32);
            this.stepL.TabIndex = 1;
            this.stepL.Text = "Initializing...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = "Updating...";
            // 
            // myLoadingIcon1
            // 
            this.myLoadingIcon1.Location = new System.Drawing.Point(23, 92);
            this.myLoadingIcon1.Name = "myLoadingIcon1";
            this.myLoadingIcon1.Size = new System.Drawing.Size(34, 37);
            this.myLoadingIcon1.TabIndex = 8;
            this.myLoadingIcon1.Text = "myLoadingIcon1";
            // 
            // FUpdate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(511, 149);
            this.Controls.Add(this.myLoadingIcon1);
            this.Controls.Add(this.stepPB);
            this.Controls.Add(this.stepL);
            this.Controls.Add(this.label1);
            this.DrawFormAccent = true;
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update data";
            this.Load += new System.EventHandler(this.FUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label stepL;
        private System.ComponentModel.BackgroundWorker workBgW;
        private System.Windows.Forms.Timer closeT;
        private System.Windows.Forms.ProgressBar stepPB;
        private BBA.VisualComponents.MyLoadingIcon myLoadingIcon1;
    }
}