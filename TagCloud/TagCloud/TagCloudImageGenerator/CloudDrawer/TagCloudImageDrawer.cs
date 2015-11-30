using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.TagCloudImageGenerator.ImageGenerators;

namespace TagCloud.TagCloudImageGenerator.CloudDrawer
{
    public class TagCloudImageDrawer:ITagCloudImageDrawer
    {
        
        public Image DrawTagCloudImage(TagCloud cloud)
        {
            var image = new Bitmap(cloud.Width, cloud.Height);
            var graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            foreach (var wordRectangle in cloud.Words)
            {
                //graphics.DrawRectangle(new Pen(Color.Black), wordRectangle.Border);
                graphics.DrawString(wordRectangle.Text, wordRectangle.Font, new SolidBrush(wordRectangle.Color), wordRectangle.Location);
            }
            return image;
        }
    }
}
