using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagCloud.TagCloudImageGenerator.ImageGenerators;
using TagCloud.TagCloudImageGenerator.WordsReaders;
using TagCloud.TagCloudImageGenerator.WordStatisticsBuilders;

namespace TagCloud.TagCloudImageGenerator
{
    public class TagCloudImageGenerator : ITagCloudImageGenerator
    {
        private readonly IWordsStatisticsBuilder _statisticsBuilder;

        private readonly Dictionary<string, IWordReader> _readers;
        private readonly Dictionary<string, IImageGenerator> _imageGenerators;

        private IImageGenerator _currentImageGenerator;

        public TagCloudImageGenerator(IEnumerable<IWordReader> readers, IWordsStatisticsBuilder statisticsBuilder,
            IEnumerable<IImageGenerator> imageGenerators)
        {
            _readers = readers.ToDictionary(x => x.FileExtension, x => x);
            _imageGenerators = imageGenerators.ToDictionary(x => x.Name, x => x);
            _statisticsBuilder = statisticsBuilder;
            _currentImageGenerator = _imageGenerators.First().Value;
        }

        public ITagCloudImageGenerator SetFont(string fontName)
        {
            var newFont = new FontFamily(fontName);
            _currentImageGenerator.Font = newFont;
            return this;
        }

        public ITagCloudImageGenerator SetImageSize(int width, int height)
        {
            _currentImageGenerator.ImageWidth = width;
            _currentImageGenerator.ImageHeight = height;
            return this;
        }

        public ITagCloudImageGenerator SetColorList(List<Color> colors)
        {
            _currentImageGenerator.Colors = colors;
            return this;
        }


        public ITagCloudImageGenerator SetBoringWordsFile(string path)
        {
            var extension = GetExtension(path);
            var inputReader = _readers[extension];
            _statisticsBuilder.BoringWords = inputReader.ReadAllWords(path);
            return this;
        }

        public ITagCloudImageGenerator SetImageGenerator(string algorithm)
        {
            _currentImageGenerator = _imageGenerators[algorithm];
            return this;
        }

        private string GetExtension(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!Path.HasExtension(path))
                throw new ArgumentException("path should has extension");
            return Path.GetExtension(path);
        }

        public Image GenerateImage(string path)
        {
            var extension = GetExtension(path);
            var inputReader = _readers[extension];
            var words = inputReader.ReadAllWords(path);

            var statistics = _statisticsBuilder.BuildStatistic(words);

            return _currentImageGenerator.GenerateImage(statistics);
        }
    }
}
