using System.Drawing;

namespace TagCloud.TagCloudImageGenerator
{
    public class Word
    {
        public Rectangle Border { get; set; }
        public string Text { get; set; }
        public Point Location { get; set; }
        public Color Color { get; set; }
        public Font Font { get; set; }

        public Word(Rectangle border, string text, Point location, Color color, Font font)
        {
            Border = border;
            Text = text;
            Location = location;
            Color = color;
            Font = font;
        }
    }
}
