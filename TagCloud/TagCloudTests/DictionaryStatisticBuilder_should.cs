using System.Linq;
using NUnit.Framework;
using TagCloud.TagCloudImageGenerator.WordStatisticsBuilders;

namespace TagCloudTests
{
    [TestFixture]
    public class DictionaryStatisticBuilder_should
    {
        [Test]
        public void includeAllDifferentWords()
        {
            var statisticsBuilder = new DictionaryWordStatisticsBuilder();
            var result = statisticsBuilder.BuildStatistic(new[] { "a", "b", "c" });
            Assert.AreEqual(result.WordsStatistics.Count(), 3);
        }

        [Test]
        public void calculateWordCount_correctly()
        {
            var statisticsBuilder = new DictionaryWordStatisticsBuilder();
            var result = statisticsBuilder.BuildStatistic(new[] { "a", "a", "a" });
            Assert.AreEqual(result.WordsStatistics.First().Count, 3);
        }

        [Test]
        public void exceptBoringWords()
        {
            var statisticsBuilder = new DictionaryWordStatisticsBuilder { BoringWords = new[] { "a" } };
            var result = statisticsBuilder.BuildStatistic(new[] { "a", "a", "a", "b", "c" });
            Assert.That(result.WordsStatistics.All(s => s.Word != "a"));
        }
    }
}
