using System;
using System.Drawing;
using System.Windows.Forms;

namespace GmailToYoutube.BBA.VisualComponents
{
    /// <summary>
    /// Custom-configured BBA button.
    /// </summary>
    public class MyButton : Button
    {
        protected static readonly Pair<Font> textFont = new Pair<Font>(new Font("Segoe UI", 10, FontStyle.Bold), new Font("Segoe UI", 20, FontStyle.Bold));
        public static readonly Pair<int> BarHeight = new Pair<int>(2, 4);

        protected bool mouseIsOver = false;
        protected bool mouseIsClicked = false;

        private bool makeItBig = false;
        public bool MakeItBig
        {
            get { return this.makeItBig; }
            set { this.makeItBig = value; this.Invalidate(); }
        }

        public MyButton()
            : base()
        {
            this.Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.mouseIsOver = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.mouseIsOver = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.mouseIsClicked = true;
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            this.mouseIsClicked = false;
            base.OnMouseUp(mevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.GetValue(this.mouseIsOver).Color);

            Size size = e.Graphics.MeasureString(this.Text, textFont.GetValue(this.makeItBig)).ToSize();
            const int imageLabelPadding = 2;

            if (this.Image != null)
                size.Width += this.Image.Width + imageLabelPadding;
            int lastLeft = this.Width / 2 - size.Width / 2;
            if (this.Image != null)
            {
                e.Graphics.DrawImage(this.Image, lastLeft, this.Height / 2 - this.Image.Height / 2);
                lastLeft += this.Image.Width + imageLabelPadding;
            }

            e.Graphics.DrawString(this.Text, textFont.GetValue(this.makeItBig), MyGUIs.Text.GetValue(this.mouseIsClicked).Brush,
                new Point(lastLeft, this.Height / 2 - size.Height / 2));

            e.Graphics.FillRectangle(MyGUIs.Accent.GetValue(this.mouseIsOver).Brush, 1, this.Height - BarHeight.GetValue(this.makeItBig), this.Width - 2, BarHeight.GetValue(this.makeItBig));
        }
    }

    /// <summary>
    /// A button meant to only draw a question mark inside a circle.
    /// </summary>
    public class MyHelpButton : MyButton
    {
        protected static readonly Brush outerCircleBrush = new SolidBrush(ColorTranslator.FromHtml("#EEEEEE"));
        protected static readonly Brush innerCircleBrush = new SolidBrush(ColorTranslator.FromHtml("#045ED4"));

        protected static Font questionMarkFont = new Font("Segoe UI", 6, FontStyle.Bold);

        protected Rectangle drawingBounds;

        public MyHelpButton()
            : base()
        {
            this.drawingBounds = new Rectangle(Point.Empty, this.Size);
        }

        protected override void OnResize(EventArgs e)
        {
            Size maxSize = this.Width < this.Height ? new Size(this.Width, this.Width) : new Size(this.Height, this.Height);
            this.drawingBounds = new Rectangle(new Point((this.Width - maxSize.Width) / 2, (this.Height - maxSize.Height) / 2), maxSize);

            Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            int fontSize = 6;
            Size textSize;
            do
            {
                questionMarkFont = new Font("Segoe UI", fontSize++, FontStyle.Bold);
                textSize = g.MeasureString("?", questionMarkFont).ToSize();
            }
            while (textSize.Width < 0.75 * this.drawingBounds.Width && textSize.Height < 0.75 * this.drawingBounds.Height);
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);
            e.Graphics.FillEllipse(outerCircleBrush,
                this.drawingBounds.Left + this.drawingBounds.Width / 12f, this.drawingBounds.Top + this.drawingBounds.Width / 12f,
                this.drawingBounds.Width - this.drawingBounds.Width / 6f, this.drawingBounds.Height - this.drawingBounds.Width / 6f);
            e.Graphics.FillEllipse(innerCircleBrush,
                this.drawingBounds.Left + this.drawingBounds.Width / 6f, this.drawingBounds.Top + this.drawingBounds.Width / 6f,
                this.drawingBounds.Width - this.drawingBounds.Width / 3f, this.drawingBounds.Height - this.drawingBounds.Width / 3f);
            Size size = e.Graphics.MeasureString("?", questionMarkFont).ToSize();
            e.Graphics.DrawString("?", questionMarkFont, MyGUIs.Text.GetValue(this.mouseIsOver).Brush, new Point(this.Width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));
        }
    }
}
