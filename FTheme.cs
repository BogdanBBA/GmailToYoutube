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
using GmailToYoutube.BBA;
using GmailToYoutube.BBA.VisualComponents;

namespace GmailToYoutube
{
    public partial class FTheme : MyForm
    {
        private FViewer mainForm;

        public FTheme(FViewer mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            SetControlForegroundColor(MyGUIs.Accent.Highlighted.Color,
                label1);
            SetControlForegroundColor(MyGUIs.Accent.Normal.Color,
                label8);
            SetControlForegroundColor(MyGUIs.Text.Normal.Color,
                label24, label3, label5, label7, label10);
            SetControlForegroundColor(MyGUIs.Text.Highlighted.Color,
                label23, label2, label4, label6, label9);
        }

        private void FTheme_Load(object sender, EventArgs e)
        {
            FillColorPanelsWithCurrentValues();
        }

        private void FillColorPanelsWithCurrentValues()
        {
            backgroundNP.BackColor = MyGUIs.Background.Normal.Color;
            backgroundHP.BackColor = MyGUIs.Background.Highlighted.Color;
            textNP.BackColor = MyGUIs.Text.Normal.Color;
            textHP.BackColor = MyGUIs.Text.Highlighted.Color;
            accentNP.BackColor = MyGUIs.Accent.Normal.Color;
            accentHP.BackColor = MyGUIs.Accent.Highlighted.Color;
            youtuberNP.BackColor = MyGUIs.Youtuber.Normal.Color;
            youtuberHP.BackColor = MyGUIs.Youtuber.Highlighted.Color;
            videoNP.BackColor = MyGUIs.Video.Normal.Color;
            videoHP.BackColor = MyGUIs.Video.Highlighted.Color;
        }

        private void ColorPanel_Click(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            colorCD.Color = panel.BackColor;
            if (colorCD.ShowDialog() == DialogResult.OK)
                panel.BackColor = colorCD.Color;
        }

        private void resetB_Click(object sender, EventArgs e)
        {
            MyGUIs.Reset();
            FillColorPanelsWithCurrentValues();
        }

        private void InvalidateControlAndChildren(Control control)
        {
            if (!(control is Label))
                control.Invalidate();
            foreach (Control child in control.Controls)
                if (child.Visible)
                    InvalidateControlAndChildren(child);
        }

        private void exitB_Click(object sender, EventArgs e)
        {
            Pair<ColorResource> background = new Pair<ColorResource>(new ColorResource(backgroundNP.BackColor), new ColorResource(backgroundHP.BackColor));
            Pair<ColorResource> text = new Pair<ColorResource>(new ColorResource(textNP.BackColor), new ColorResource(textHP.BackColor));
            Pair<ColorResource> accent = new Pair<ColorResource>(new ColorResource(accentNP.BackColor), new ColorResource(accentHP.BackColor));
            Pair<ColorResource> youtuber = new Pair<ColorResource>(new ColorResource(youtuberNP.BackColor), new ColorResource(youtuberHP.BackColor));
            Pair<ColorResource> video = new Pair<ColorResource>(new ColorResource(videoNP.BackColor), new ColorResource(videoHP.BackColor));

            MyGUIs.Initialize(background, text, accent, youtuber, video);
            MyGUIs.SaveToFile();

            this.mainForm.RefreshThemeColorsOfOtherwiseNonRefreshableControls();

            this.mainForm.Invalidate();
            foreach (Control control in this.mainForm.Controls)
                if (control.Visible)
                    InvalidateControlAndChildren(control);
            this.mainForm.RefreshInformation(false);

            this.Close();
        }
    }
}
