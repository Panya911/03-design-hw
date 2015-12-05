using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TagCloud.TagCloudImageGenerator;
using TagCloud.TagCloudImageGenerator.CloudDrawer;
using TagCloud.TagCloudImageGenerator.TagCloudGenerators;
using TagCloud.TagCloudImageGenerator.WordsReaders;
using TagCloud.TagCloudImageGenerator.WordStatisticsBuilders;

namespace TagCloudTests
{
    [TestFixture]
    public class TagCloudImageGenerator_should
    {

        private Mock<IWordReader> _readerMock;
        private Mock<IWordsStatisticsBuilder> _wordStatisticBuilderMock;
        private Mock<ITagCloudGenerator> _imageGeneratorMock;
        private Mock<ITagCloudImageDrawer> _tagCloudDrawerMock;

        [SetUp]
        public void SetUp()
        {
            _readerMock = new Mock<IWordReader>();
            _readerMock.Setup(r => r.FileExtension).Returns("txt");
            _wordStatisticBuilderMock = new Mock<IWordsStatisticsBuilder>();
            _imageGeneratorMock = new Mock<ITagCloudGenerator>();
            _imageGeneratorMock.Setup(g => g.Name).Returns("name");
            _tagCloudDrawerMock=new Mock<ITagCloudImageDrawer>();
        }

        [Test]
        public void throwException_if_inputFile_doesNotExist()
        {
            var generator = new TagCloudImageGenerator(new[] { _readerMock.Object },
                _wordStatisticBuilderMock.Object,
                new[] { _imageGeneratorMock.Object },
                _tagCloudDrawerMock.Object);
            Assert.Throws(typeof(TagCloudImageGeneratorTuningException), () => generator.SetFont("Magic Font"));
        }

        [Test]
        public void throwException_if_imageGenerator_doesNotExist()
        {
            var generator = new TagCloudImageGenerator(new[] { _readerMock.Object },
                _wordStatisticBuilderMock.Object,
                new[] { _imageGeneratorMock.Object },
                _tagCloudDrawerMock.Object);
            Assert.Throws(typeof(TagCloudImageGeneratorTuningException), () => generator.SetImageGenerator("incorrect Name"));
        }
    }
}
