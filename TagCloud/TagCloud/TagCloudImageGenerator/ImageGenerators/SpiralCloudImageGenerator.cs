using System;
using System.Drawing;

namespace TagCloud.TagCloudImageGenerator.ImageGenerators
{
    public class SpiralCloudImageGenerator : ImageGeneratorBase
    {
        public override string Name => "Spiral";

        protected override bool TryGetLocationForRect(SizeF rect, out Point location)
        {
            var step = 5;
            var phi = 0.0;
            while (true)
            {
                var rho = step / (2 * Math.PI) * phi;
                var dx = rho * Math.Cos(phi);
                var dy = rho * Math.Sin(phi);
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
                phi += 0.01;
            }
        }

        
    }
}
