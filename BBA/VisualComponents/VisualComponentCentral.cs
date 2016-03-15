using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace GmailToYoutube.BBA.VisualComponents
{
    /// <summary>
    /// A color-brush-pen resource class.
    /// </summary>
    public class ColorResource
    {
        public Color Color { get; private set; }
        public Brush Brush { get; private set; }
        public Pen Pen { get; private set; }

        public ColorResource(Color color)
        {
            this.SetColorAndUpdateResource(color);
        }

        public void SetColorAndUpdateResource(Color color)
        {
            this.Color = color;
            this.Brush = new SolidBrush(color);
            this.Pen = new Pen(color);
        }
    }

    /// <summary>
    /// The constant (or at least static) values used for the GUIs.
    /// </summary>
    public static class MyGUIs
    {
        public static Pair<ColorResource> Background;
        public static Pair<ColorResource> Text;
        public static Pair<ColorResource> Accent;
        public static Pair<ColorResource> Youtuber;
        public static Pair<ColorResource> Video;

        static MyGUIs()
        {
            MyGUIs.Reset();
        }

        /// <summary>Initializes to default values.</summary>
        public static void Reset()
        {
            MyGUIs.Background = new Pair<ColorResource>(new ColorResource(Color.FromArgb(16, 16, 16)), new ColorResource(Color.FromArgb(24, 24, 24)));
            MyGUIs.Text = new Pair<ColorResource>(new ColorResource(Color.WhiteSmoke), new ColorResource(Color.Orange));
            MyGUIs.Accent = new Pair<ColorResource>(new ColorResource(Color.Orange), new ColorResource(Color.OrangeRed));
            MyGUIs.Youtuber = new Pair<ColorResource>(new ColorResource(Color.FromArgb(16, 79, 178)), new ColorResource(Color.FromArgb(12, 60, 122)));
            MyGUIs.Video = new Pair<ColorResource>(new ColorResource(Color.FromArgb(178, 16, 31)), new ColorResource(Color.FromArgb(122, 12, 20)));
        }

        /// <summary>Initializes from file.</summary>
        public static void Initialize()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Paths.ThemeFile);

            MyGUIs.Background = new Pair<ColorResource>(
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Background").Attributes["normal"].Value)),
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Background").Attributes["highlighted"].Value)));
            MyGUIs.Text = new Pair<ColorResource>(
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Text").Attributes["normal"].Value)),
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Text").Attributes["highlighted"].Value)));
            MyGUIs.Accent = new Pair<ColorResource>(
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Accent").Attributes["normal"].Value)),
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Accent").Attributes["highlighted"].Value)));
            MyGUIs.Youtuber = new Pair<ColorResource>(
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Youtuber").Attributes["normal"].Value)),
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Youtuber").Attributes["highlighted"].Value)));
            MyGUIs.Video = new Pair<ColorResource>(
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Video").Attributes["normal"].Value)),
                new ColorResource(ColorTranslator.FromHtml(doc.SelectSingleNode("Theme/Video").Attributes["highlighted"].Value)));
        }

        /// <summary>Initializes from given values.</summary>
        public static void Initialize(Pair<ColorResource> background, Pair<ColorResource> text, Pair<ColorResource> accent, Pair<ColorResource> youtuber, Pair<ColorResource> video)
        {
            MyGUIs.Background = background;
            MyGUIs.Text = text;
            MyGUIs.Accent = accent;
            MyGUIs.Youtuber = youtuber;
            MyGUIs.Video = video;
        }

        /// <summary>Saves current state to file.</summary>
        public static void SaveToFile()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.AppendChild(doc.CreateElement("Theme"));

            XmlNode node = root.AppendChild(doc.CreateElement("Background"));
            node.AddAttribute(doc, MyGUIs.Background);
            node = root.AppendChild(doc.CreateElement("Text"));
            node.AddAttribute(doc, MyGUIs.Text);
            node = root.AppendChild(doc.CreateElement("Accent"));
            node.AddAttribute(doc, MyGUIs.Accent);
            node = root.AppendChild(doc.CreateElement("Youtuber"));
            node.AddAttribute(doc, MyGUIs.Youtuber);
            node = root.AppendChild(doc.CreateElement("Video"));
            node.AddAttribute(doc, MyGUIs.Video);

            doc.Save(Paths.ThemeFile);
        }
    }
}
