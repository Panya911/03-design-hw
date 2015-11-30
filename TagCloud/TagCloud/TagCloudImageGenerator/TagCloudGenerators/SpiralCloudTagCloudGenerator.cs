using System;
using System.Drawing;

namespace TagCloud.TagCloudImageGenerator.ImageGenerators
{
    public class SpiralCloudTagCloudGenerator : TagCloudGeneratorBase
    {
        public override string Name => "Spiral";

        protected override bool TryGetLocationForRect(SizeF rect, out Point location)
        {
            var spiralStep = 5;
            var angle = 0.0;
            while (true)
            {
                var radius = spiralStep / (2 * Math.PI) * angle;  
                var dx = radius * Math.Cos(angle);
                var dy = radius * Math.Sin(angle);
                var newLocation = new Point(Center.X + (int)(dx-rect.Width/2), Center.Y + (int)(dy-rect.Height/2));
                var newRect = new RectangleF(newLocation, rect);
                if (CanLocate(newRect))
                {
                    location = newLocation;
                    return true;
                }
                if (IsAllOutBounds(newRect))
                {
                    location=Point.Empty;
                    return false;
                }
                angle += 0.01;
            }
        }

        
    }
}
