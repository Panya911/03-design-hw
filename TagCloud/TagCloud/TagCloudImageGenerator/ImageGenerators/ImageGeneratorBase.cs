using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TagCloud.TagCloudImageGenerator.ImageGenerators
{
    public abstract class ImageGeneratorBase : IImageGenerator
    {
        public int ImageWidth { get; set; } = 1000;
        public int ImageHeight { get; set; } = 1000;
        public FontFamily Font { get; set; } = new FontFamily("arial");
        public List<Color> Colors { get; set; } = new List<Color> { Color.DarkRed };
        public int MaxFontSize { get; set; } = 200;


        protected Point Center => new Point(ImageWidth / 2, ImageHeight / 2);

        private int _minWeight;
        private int _maxWeight;
        private static Random _rnd=new Random();

        private List<RectangleF> _badPlaces;
        public Image GenerateImage(Statistic wordsStatistics)
        {
            _minWeight = wordsStatistics.MinWeight;
            _maxWeight = wordsStatistics.MaxWeight;
            var image = new Bitmap(ImageWidth, ImageHeight);
            var graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            _badPlaces = new List<RectangleF>();
            foreach (var wordsStatistic in wordsStatistics.WordsStatistics)
            {
                var font = GetFont(wordsStatistic.Count);
                var boundsRect = graphics.MeasureString(wordsStatistic.Word, font);
                Point location;
                if (!TryGetLocationForRect(boundsRect, out location))
                    break;
                _badPlaces.Add(new RectangleF(location, boundsRect));
                //graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(location.X, location.Y,
                // (int)boundsRect.Width, (int)boundsRect.Height));
                graphics.DrawString(wordsStatistic.Word, font, new SolidBrush(GetRandomColor()), location);
            }
            return image;
        }

        public abstract string Name { get; }

        protected abstract bool TryGetLocationForRect(SizeF rect, out Point location);

        protected bool CanLocate(RectangleF rect)
        {
            return _badPlaces.All(r => !r.IntersectsWith(rect)) && !IsAllOutBounds(rect);
        }

        protected bool IsAllOutBounds(RectangleF rect)
        {
            return !(new RectangleF(0, 0, ImageWidth, ImageHeight).IntersectsWith(rect));
        }

        private Font GetFont(int weight)
        {
            var fontSize = (MaxFontSize * (weight - _minWeight)) / (_maxWeight - _minWeight);
            if (fontSize < 1)
                fontSize = 1;
            return new Font(Font, fontSize);
        }

        private Color GetRandomColor()
        {
            return Colors[_rnd.Next(Colors.Count)];
        }
    }
}
