using System.Drawing;

namespace TagCloud.TagCloudImageGenerator
{
    public class TagCloudElement
    {
        
        public string Text { get; set; }
        public Rectangle Location { get; set; }
        public Color Color { get; set; }
        public Font Font { get; set; }

        public TagCloudElement(string text, Rectangle location, Color color, Font font)
        {
            Text = text;
            Location = location;
            Color = color;
            Font = font;
        }
    }
}
