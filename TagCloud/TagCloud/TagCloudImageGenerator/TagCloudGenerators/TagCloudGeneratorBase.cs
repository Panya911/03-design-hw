using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.TagCloudImageGenerator.TagCloudGenerators
{
    public abstract class TagCloudGeneratorBase : ITagCloudGenerator
    {
        public int ImageWidth { get; set; } = 1000;
        public int ImageHeight { get; set; } = 1000;
        public FontFamily Font { get; set; } = new FontFamily("arial");
        public List<Color> Colors { get; set; } = new List<Color> { Color.DarkRed };
        public int MaxFontSize { get; set; } = 200;



        protected Point Center => new Point(ImageWidth / 2, ImageHeight / 2);

        private int _minWeight;
        private int _maxWeight;
        private Func<string, Font, Size> _measureString;
        private static readonly Random Rnd = new Random();

        private List<Rectangle> _badPlaces;
        public TagCloud GenerateCloud(Statistic wordsStatistics, Func<string, Font, Size> measureString)
        {
            _minWeight = wordsStatistics.MinWeight;
            _maxWeight = wordsStatistics.MaxWeight;
            _measureString = measureString;
            var words = PlaceWords((wordsStatistics.WordsStatistics));

            return new TagCloud(ImageWidth, ImageHeight, words);
        }

        private IEnumerable<TagCloudElement> PlaceWords(IEnumerable<WordsStatistic> statistics)
        {
            _badPlaces = new List<Rectangle>();
            foreach (var wordsStatistic in statistics)
            {
                TagCloudElement tagCloudElement;
                if (!TryPlaceWord(wordsStatistic, out tagCloudElement))
                    yield break;
                yield return tagCloudElement;
            }
        }

        private bool TryPlaceWord(WordsStatistic wordsStatistic, out TagCloudElement tagCloudElement)
        {
            var font = GetFont(wordsStatistic.Count);
            var boundsRect = _measureString(wordsStatistic.Word, font);
            Point location;
            if (!TryGetLocationForRect(boundsRect, out location))
            {
                tagCloudElement = null;
                return false;
            }
            var position = new Rectangle(location, boundsRect);
            _badPlaces.Add(position);
            tagCloudElement = new TagCloudElement(wordsStatistic.Word, position, GetRandomColor(), font);
            return true;
        }

        public abstract string Name { get; }

        protected abstract bool TryGetLocationForRect(Size rect, out Point location);

        protected bool CanLocate(Rectangle rect)
        {
            return _badPlaces.All(r => !r.IntersectsWith(rect)) && IsInsideImage(rect);
        }

        protected bool IsAllOutBounds(Rectangle rect)
        {
            return !(new Rectangle(0, 0, ImageWidth, ImageHeight).IntersectsWith(rect));
        }

        private bool IsInsideImage(Rectangle rect)
        {
            return rect.Left > 0 && rect.Right <= ImageWidth
            && rect.Top > 0 && rect.Bottom <= ImageHeight;
        }

        private Font GetFont(int weight)
        {
            if (_maxWeight - _minWeight == 0)
                return new Font(Font, MaxFontSize);
            var fontSize = (MaxFontSize * (weight - _minWeight)) / (_maxWeight - _minWeight);
            if (fontSize < 1)
                fontSize = 1;
            return new Font(Font, fontSize);
        }

        private Color GetRandomColor()
        {
            return Colors[Rnd.Next(Colors.Count)];
        }
    }
}
