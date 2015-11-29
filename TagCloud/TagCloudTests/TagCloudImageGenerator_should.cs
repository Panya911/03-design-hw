using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TagCloud.TagCloudImageGenerator;
using TagCloud.TagCloudImageGenerator.ImageGenerators;
using TagCloud.TagCloudImageGenerator.WordsReaders;
using TagCloud.TagCloudImageGenerator.WordStatisticsBuilders;

namespace TagCloudTests
{
    [TestFixture]
    public class TagCloudImageGenerator_should
    {

        private Mock<IWordReader> _readerMock;
        private Mock<IWordsStatisticsBuilder> _wordStatisticBuilderMock;
        private Mock<IImageGenerator> _imageGeneratorMock;

        [SetUp]
        public void SetUp()
        {
            _readerMock = new Mock<IWordReader>();
            _readerMock.Setup(r => r.FileExtension).Returns("txt");
            _wordStatisticBuilderMock = new Mock<IWordsStatisticsBuilder>();
            _imageGeneratorMock = new Mock<IImageGenerator>();
            _imageGeneratorMock.Setup(g => g.Name).Returns("name");
        }

        [Test]
        public void throwException_if_inputFile_doesNotExist()
        {
            var generator = new TagCloudImageGenerator(new[] { _readerMock.Object },
                _wordStatisticBuilderMock.Object,
                new[] { _imageGeneratorMock.Object });
            Assert.Throws(typeof(TagCloudImageGeneratorTuningException), () => generator.SetFont("Magic Font"));
        }

        [Test]
        public void throwException_if_imageGenerator_doesNotExist()
        {
            var generator = new TagCloudImageGenerator(new[] { _readerMock.Object },
                _wordStatisticBuilderMock.Object,
                new[] { _imageGeneratorMock.Object });
            Assert.Throws(typeof(TagCloudImageGeneratorTuningException), () => generator.SetImageGenerator("incorrect Name"));
        }
    }
}
