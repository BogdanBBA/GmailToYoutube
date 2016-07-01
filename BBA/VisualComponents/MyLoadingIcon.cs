using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GmailToYoutube.BBA.VisualComponents
{
    public class MyLoadingIcon : Control
    {
        protected static readonly Pair<int> PieSpanRange = new Pair<int>(180, 360);
        protected const int AngleChange = 20;
        protected const int PieSpanChange = 8;

        protected Timer timer;
        protected int angle;
        protected int pieSpan;
        protected bool pieSpanIncreasing;

        public MyLoadingIcon()
            : base()
        {
            this.DoubleBuffered = true;
            this.timer = new Timer();
            this.timer.Interval = 50;
            this.timer.Tick += Timer_Tick;
            this.angle = 0;
            this.pieSpan = MyLoadingIcon.PieSpanRange.Normal;
            this.pieSpanIncreasing = true;
            this.TurnAnimation(true);
        }

        public void TurnAnimation(bool on)
        {
            this.timer.Enabled = on;
        }

        protected void Timer_Tick(object sender, EventArgs e)
        {
            this.pieSpan += MyLoadingIcon.PieSpanChange * (this.pieSpanIncreasing ? 1 : -1);
            if (this.pieSpan >= MyLoadingIcon.PieSpanRange.Highlighted)
                this.pieSpanIncreasing = false;
            else if (this.pieSpan <= MyLoadingIcon.PieSpanRange.Normal)
                this.pieSpanIncreasing = true;
            this.angle += MyLoadingIcon.AngleChange;
            if (this.angle >= 360)
                this.angle = 0;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            int size = this.Width < this.Height ? this.Width : this.Height;
            Rectangle rect = new Rectangle(this.Width / 2 - size / 2, this.Height / 2 - size / 2, size, size);
            e.Graphics.FillPie(MyGUIs.Accent.Highlighted.Brush, rect, this.angle, this.pieSpan);

            size /= 2;
            rect = new Rectangle(this.Width / 2 - size / 2, this.Height / 2 - size / 2, size, size);
            e.Graphics.FillEllipse(MyGUIs.Background.Normal.Brush, rect);
        }
    }
}
