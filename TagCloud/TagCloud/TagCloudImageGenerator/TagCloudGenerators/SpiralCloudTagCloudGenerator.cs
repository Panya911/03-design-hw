using System;
using System.Drawing;

namespace TagCloud.TagCloudImageGenerator.TagCloudGenerators
{
    public class SpiralCloudTagCloudGenerator : TagCloudGeneratorBase
    {
        public override string Name => "Spiral";

        protected override bool TryGetLocationForRect(Size rect, out Point location)
        {
            const int spiralStep = 5;
            var angle = 0.0;
            while (true)
            {
                var radius = spiralStep / (2 * Math.PI) * angle;
                var dx = radius * Math.Cos(angle);
                var dy = radius * Math.Sin(angle);
                var newLocation = new Point(Center.X + (int)(dx - rect.Width / 2), Center.Y + (int)(dy - rect.Height / 2));
                var newRect = new Rectangle(newLocation, rect);
                if (CanLocate(newRect))
                {
                    location = newLocation;
                    return true;
                }
                if (IsAllOutBounds(newRect))
                {
                    location = Point.Empty;
                    return false;
                }
                angle += 0.01;
            }
        }
    }
}
