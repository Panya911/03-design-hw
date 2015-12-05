using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.TagCloudImageGenerator.TagCloudGenerators;

namespace TagCloud.TagCloudImageGenerator.CloudDrawer
{
    public class TagCloudImageDrawer:ITagCloudImageDrawer
    {
        
        public Image DrawTagCloudImage(Statistic statistic, ITagCloudGenerator tagCloudGenerator)
        {
            var image = new Bitmap(tagCloudGenerator.ImageWidth, tagCloudGenerator.ImageHeight);
            var graphics = Graphics.FromImage(image);
            var cloud = tagCloudGenerator.GenerateCloud(statistic, (s, f) => graphics.MeasureString(s, f).ToSize());
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            foreach (var wordRectangle in cloud.Elements)
            {
                //graphics.DrawRectangle(new Pen(Color.Black), wordRectangle.Border);
                graphics.DrawString(wordRectangle.Text, wordRectangle.Font, new SolidBrush(wordRectangle.Color), wordRectangle.Location.Location);
            }
            return image;
        }
    }
}
