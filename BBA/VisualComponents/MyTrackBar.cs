using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GmailToYoutube.BBA.VisualComponents
{
    public class MyTrackBar : TrackBar
    {
        public MyTrackBar()
            : base()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);
        }
    }
}
