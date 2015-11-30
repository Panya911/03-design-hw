using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TagCloud.TagCloudImageGenerator.ImageGenerators
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
        private static readonly Random Rnd = new Random();

        private List<RectangleF> _badPlaces;
        public TagCloud GenerateCloud(Statistic wordsStatistics)
        {
            _minWeight = wordsStatistics.MinWeight;
            _maxWeight = wordsStatistics.MaxWeight;
            var words = PlaceWords((wordsStatistics.WordsStatistics));

            return new TagCloud(ImageWidth, ImageHeight, words);
        }

        private IEnumerable<Word> PlaceWords(IEnumerable<WordsStatistic> statistics)
        {
            _badPlaces = new List<RectangleF>();
            foreach (var wordsStatistic in statistics)
            {
                Word word;
                if (!TryPlaceWord(wordsStatistic, out word))
                    yield break;
                yield return word;
            }
        }

        private bool TryPlaceWord(WordsStatistic wordsStatistic, out Word word)
        {
            var font = GetFont(wordsStatistic.Count);
            var boundsRect = graphics.MeasureString(wordsStatistic.Word, font); //it's Bad :( where should i take graphics?
            Point location;
            if (!TryGetLocationForRect(boundsRect, out location))
            {
                word = null;
                return false;
            }
            _badPlaces.Add(new RectangleF(location, boundsRect));
            word = new Word(boundsRect, wordsStatistic.Word, location, GetRandomColor(), font);
            return true;
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
            return Colors[Rnd.Next(Colors.Count)];
        }
    }
}
