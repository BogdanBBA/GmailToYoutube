using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GmailToYoutube.BBA.VisualComponents;

namespace BBA.VisualComponents
{
    public class WindowHelper
    {
        public const int SW_HIDE = 0;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }

    public class KeyboardHelper
    {
        private const byte VK_MENU = 0x12;
        private const byte VK_TAB = 0x09;
        private const int KEYEVENTF_EXTENDEDKEY = 0x01;
        private const int KEYEVENTF_KEYUP = 0x02;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public static void AltTab(int howManyTimes)
        {
            for (int count = 0; count < howManyTimes; count++)
            {
                keybd_event(VK_MENU, 0xb8, 0, 0); // Alt Press 
                keybd_event(VK_TAB, 0x8f, 0, 0); // Tab Press 
                keybd_event(VK_TAB, 0x8f, KEYEVENTF_KEYUP, 0); // Tab Release 
                keybd_event(VK_MENU, 0xb8, KEYEVENTF_KEYUP, 0); // Alt Release 
            }
        }
    }

    public class MyForm : Form
    {
        public MyForm()
            : base()
        {
        }

        private bool drawFormAccent;
        public bool DrawFormAccent
        {
            get { return this.drawFormAccent; }
            set { this.drawFormAccent = value; this.Invalidate(); }
        }

        protected void SetControlForegroundColor(Color color, params Control[] controls)
        {
            foreach (Control control in controls)
                control.ForeColor = color;
        }

        protected void SetControlBackgroundColor(Color color, params Control[] controls)
        {
            foreach (Control control in controls)
                control.BackColor = color;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(MyGUIs.Background.Normal.Color);
            if (this.drawFormAccent)
                e.Graphics.DrawRectangle(MyGUIs.Accent.Normal.Pen, 3, 3, this.Width - 6, this.Height - 6);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
