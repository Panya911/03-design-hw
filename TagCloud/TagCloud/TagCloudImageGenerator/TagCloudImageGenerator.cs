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
            if (readers == null)
                throw new ArgumentNullException(nameof(readers));
            if (statisticsBuilder == null)
                throw new ArgumentNullException(nameof(statisticsBuilder));
            if (imageGenerators == null)
                throw new ArgumentNullException(nameof(imageGenerators));
            _readers = readers.ToDictionary(x => x.FileExtension.ToLower(), x => x);
            _imageGenerators = imageGenerators.ToDictionary(x => x.Name.ToLower(), x => x);
            _statisticsBuilder = statisticsBuilder;
            _currentImageGenerator = _imageGenerators.First().Value;
        }

        public ITagCloudImageGenerator SetFont(string fontName)
        {

            FontFamily newFont;
            try
            {
                newFont = new FontFamily(fontName);
            }
            catch (ArgumentException)
            {
                throw new TagCloudImageGeneratorTuningException(fontName + " font doesn't exist or not installed on your computer");
            }
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
            if (!File.Exists(path))
                throw new TagCloudImageGeneratorTuningException(path + " file doesn't exist");
            string extension;
            try
            {
                extension = GetExtension(path);
            }
            catch (ArgumentException e)
            {
                throw new TagCloudImageGeneratorTuningException(e.Message);
            }
            var inputReader = _readers[extension];
            _statisticsBuilder.BoringWords = inputReader.ReadAllWords(path);
            return this;
        }

        public ITagCloudImageGenerator SetNeededWordsCount(int count)
        {
            _statisticsBuilder.NeededWords = count;
            return this;
        }

        public ITagCloudImageGenerator SetMaxFontSize(int size)
        {
            _currentImageGenerator.MaxFontSize = size;
            return this;
        }

        public ITagCloudImageGenerator SetImageGenerator(string generatorName)
        {
            generatorName = generatorName.ToLower();
            if (!_imageGenerators.ContainsKey(generatorName))
                throw new TagCloudImageGeneratorTuningException(generatorName + " generator doesn't exist");
            _currentImageGenerator = _imageGenerators[generatorName.ToLower()];
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

    public class TagCloudImageGeneratorTuningException : Exception
    {
        public TagCloudImageGeneratorTuningException(string msg)
            : base(msg)
        {
        }
    }
}
