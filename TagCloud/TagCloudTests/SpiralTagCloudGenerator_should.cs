using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TagCloud.TagCloudImageGenerator;
using TagCloud.TagCloudImageGenerator.TagCloudGenerators;

namespace TagCloudTests
{
    [TestFixture]
    public class SpiralTagCloudGenerator_should
    {
        private readonly Random rnd = new Random();
        private readonly SpiralCloudTagCloudGenerator _generator;
        private readonly Func<string, Font, Size> _measureString;
        public SpiralTagCloudGenerator_should()
        {
            _generator = new SpiralCloudTagCloudGenerator
            {
                ImageHeight = 1000,
                ImageWidth = 1000,
                Font = new FontFamily("courier")
            };
            _measureString = (w, f) => new Size(w.Length, f.Height);

        }
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void placeAllWords_insideImage() //работает долго. Терпи. генерирует 5 случайных облаков
        {
            for (var i = 0; i < 5; i++)
            {
                var wordsCount = rnd.Next(500);
                var statistic = GenerateRandomStatistic(wordsCount);
                var cloud = _generator.GenerateCloud(statistic, _measureString);

                Assert.That(cloud.Elements.All(e => IsInsideImage(e.Location)));
            }
        }

        [Test]
        public void notPlaceWord_ifItVeryLarge()
        {
            var wordStatistics = new[] { new WordsStatistic(GetRandomWord(1000), 1) };
            var statistic = new Statistic(1, 1, wordStatistics);
            var cloud = _generator.GenerateCloud(statistic, _measureString);
            Assert.AreEqual(0, cloud.Elements.Count());
        }

        [Test]
        public void work_ifVeryManyWords() //работает долго. Терпи. генерирует 5 случайных облаков
        {
            for (var i = 0; i < 5; i++)
            {
                var statistic = GenerateRandomStatistic(1000000);
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var cloud = _generator.GenerateCloud(statistic, _measureString);
                cloud.Elements.ToList();//для активации ленивого перечисления
                stopWatch.Stop();
                Assert.That(stopWatch.Elapsed.Seconds < 20);
            }
        }

        private bool IsInsideImage(Rectangle rect)
        {
            return rect.Left > 0 && rect.Right <= _generator.ImageWidth
            && rect.Top > 0 && rect.Bottom <= _generator.ImageHeight;
        }

        private Statistic GenerateRandomStatistic(int wordsCount)
        {
            var wordStatistics = GetWordStatistics(wordsCount).ToList();
            var wordsStatistics = wordStatistics.ToArray().OrderByDescending(ws => ws.Count);
            var minWeight = wordsStatistics.Min(ws => ws.Count);
            var maxWeight = wordsStatistics.Max(ws => ws.Count);
            return new Statistic(minWeight, maxWeight, wordsStatistics);
        }

        private IEnumerable<WordsStatistic> GetWordStatistics(int wordCount)
        {
            for (var i = 0; i < wordCount; i++)
                yield return new WordsStatistic(GetRandomWord(rnd.Next(5, 11)), rnd.Next(10, 400));
        }

        private char GetRandomAsciiSymbol()
        {
            return (char)rnd.Next(65, 91);
        }

        private string GetRandomWord(int wordLength)
        {
            var wordBuilder = new StringBuilder(wordLength);
            for (var i = 0; i < wordLength; i++)
                wordBuilder.Append(GetRandomAsciiSymbol());
            return wordBuilder.ToString();
        }
    }
}
