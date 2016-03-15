using BBA.VisualComponents;
using System;
using GmailToYoutube.BBA.VisualComponents;

namespace GmailToYoutube
{
    public partial class FAbout : MyForm
    {
        public FAbout()
        {
            InitializeComponent();
            SetControlForegroundColor(MyGUIs.Accent.Highlighted.Color,
                label1);
            SetControlForegroundColor(MyGUIs.Accent.Normal.Color,
                label8);
            SetControlForegroundColor(MyGUIs.Text.Highlighted.Color,
                label14, label4, label9, label7, label13);
            SetControlForegroundColor(MyGUIs.Text.Normal.Color,
                label2, label3, label5, label6, label5, label12);
        }

        private void FAbout_Load(object sender, EventArgs e)
        {
            appIconPB.Load(Paths.AppIconPngFile);
        }

        private void exitB_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
