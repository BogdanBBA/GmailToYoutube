namespace GmailToYoutube
{
    partial class FLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLogin));
            this.initializeBgW = new System.ComponentModel.BackgroundWorker();
            this.stepL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.exitB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.loginB = new GmailToYoutube.BBA.VisualComponents.MyButton();
            this.SuspendLayout();
            // 
            // initializeBgW
            // 
            this.initializeBgW.WorkerReportsProgress = true;
            this.initializeBgW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.initializeBgW_DoWork);
            this.initializeBgW.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.initializeBgW_ProgressChanged);
            this.initializeBgW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.initializeBgW_RunWorkerCompleted);
            // 
            // stepL
            // 
            this.stepL.AutoSize = true;
            this.stepL.BackColor = System.Drawing.Color.Transparent;
            this.stepL.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepL.Location = new System.Drawing.Point(26, 95);
            this.stepL.Name = "stepL";
            this.stepL.Size = new System.Drawing.Size(407, 32);
            this.stepL.TabIndex = 6;
            this.stepL.Text = "Checking paths and loading database...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 65);
            this.label1.TabIndex = 5;
            this.label1.Text = "Gmail-to-Youtube";
            // 
            // exitB
            // 
            this.exitB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitB.Location = new System.Drawing.Point(257, 144);
            this.exitB.MakeItBig = true;
            this.exitB.Name = "exitB";
            this.exitB.Size = new System.Drawing.Size(200, 60);
            this.exitB.TabIndex = 3;
            this.exitB.Text = "Exit";
            this.exitB.UseVisualStyleBackColor = true;
            this.exitB.Click += new System.EventHandler(this.exitB_Click);
            // 
            // loginB
            // 
            this.loginB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginB.Location = new System.Drawing.Point(59, 144);
            this.loginB.MakeItBig = true;
            this.loginB.Name = "loginB";
            this.loginB.Size = new System.Drawing.Size(200, 60);
            this.loginB.TabIndex = 2;
            this.loginB.Text = "Login";
            this.loginB.UseVisualStyleBackColor = true;
            this.loginB.Click += new System.EventHandler(this.loginB_Click);
            // 
            // FLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(511, 245);
            this.Controls.Add(this.stepL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exitB);
            this.Controls.Add(this.loginB);
            this.DrawFormAccent = true;
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.FLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BBA.VisualComponents.MyButton loginB;
        private BBA.VisualComponents.MyButton exitB;
        private System.ComponentModel.BackgroundWorker initializeBgW;
        private System.Windows.Forms.Label stepL;
        private System.Windows.Forms.Label label1;
    }
}

