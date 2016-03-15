using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BBA.VisualComponents;

namespace GmailToYoutube.BBA.VisualComponents
{
    public class StatsView : Control
    {
        protected static Font TitleFont = new Font("Segoe UI", 26, FontStyle.Bold, GraphicsUnit.Pixel);
        protected static Font SubtitleFont = new Font("Segoe UI", 24, FontStyle.Bold, GraphicsUnit.Pixel);

        protected static Image GmailImage { get; set; }
        protected static Image YoutuberImage { get; set; }
        protected static Image VideoImage { get; set; }
        protected static Image VideoNewImage { get; set; }
        protected static Image VideoMarkedToDeleteImage { get; set; }

        public static void SetImages(Image gmailImage, Image youtuberImage, Image videoImage, Image videoNewImage, Image videoMarkedToDeleteImage)
        {
            StatsView.GmailImage = gmailImage;
            StatsView.YoutuberImage = youtuberImage;
            StatsView.VideoImage = videoImage;
            StatsView.VideoNewImage = videoNewImage;
            StatsView.VideoMarkedToDeleteImage = videoMarkedToDeleteImage;
        }

        protected int emailCount;
        protected int youtuberCount;
        protected int videoCount;
        protected int videoNewCount;
        protected int videoMarkedToDeleteCount;
        protected TimeSpan totalDuration;

        public void RefreshInformation(int emailCount, int youtuberCount, int videoCount, int videoNewCount, int videoMarkedToDeleteCount, TimeSpan totalDuration)
        {
            this.emailCount = emailCount;
            this.youtuberCount = youtuberCount;
            this.videoCount = videoCount;
            this.videoNewCount = videoNewCount;
            this.videoMarkedToDeleteCount = videoMarkedToDeleteCount;
            this.totalDuration = totalDuration;
            this.Invalidate();
        }

        private void AppendStuff(PaintEventArgs e, Image image, int count, ref int lastLeft)
        {
            if (image != null)
            {
                e.Graphics.DrawImage(image, lastLeft, this.Height / 2 - image.Height / 2);
                lastLeft += image.Width;
            }

            string text = count.ToString();
            Size size = e.Graphics.MeasureString(text, StatsView.TitleFont).ToSize();
            Pen pen = new Pen(MyGUIs.Background.Normal.Color, 2.5f);
            GraphicsPath path = new GraphicsPath();

            if (image != null)
                path.AddString(text, StatsView.TitleFont.FontFamily, (int) StatsView.TitleFont.Style, StatsView.TitleFont.Size,
                    new Point(lastLeft - size.Width / 2, this.Height / 2 + image.Height / 2 - size.Height + 2), StringFormat.GenericDefault);

            e.Graphics.DrawPath(pen, path);
            e.Graphics.FillPath(MyGUIs.Text.Normal.Brush, path);

            lastLeft += size.Width / 2 + 4;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            string text = Utils.FormatDuration(this.totalDuration);
            Size size = e.Graphics.MeasureString(text, StatsView.SubtitleFont).ToSize();
            e.Graphics.DrawString(text, StatsView.SubtitleFont, MyGUIs.Text.Normal.Brush, new Point(this.Width - size.Width, this.Height - size.Height - MyButton.BarHeight.Highlighted + 2));

            int lastLeft = 0;

            this.AppendStuff(e, StatsView.GmailImage, this.emailCount, ref lastLeft);
            this.AppendStuff(e, StatsView.YoutuberImage, this.youtuberCount, ref lastLeft);
            this.AppendStuff(e, StatsView.VideoImage, this.videoCount, ref lastLeft);
            this.AppendStuff(e, StatsView.VideoNewImage, this.videoNewCount, ref lastLeft);
            this.AppendStuff(e, StatsView.VideoMarkedToDeleteImage, this.videoMarkedToDeleteCount, ref lastLeft);

            e.Graphics.FillRectangle(MyGUIs.Accent.Normal.Brush, 1, this.Height - MyButton.BarHeight.Highlighted, this.Width - 2, MyButton.BarHeight.Highlighted);
        }
    }
}
