using System.Collections.Generic;

namespace TagCloud.TagCloudImageGenerator
{
    public class Statistic
    {
        public readonly int MinWeight;
        public readonly int MaxWeight;
        public readonly IEnumerable<WordsStatistic> WordsStatistics;

        public Statistic(int minWeight, int maxWeight, IEnumerable<WordsStatistic> wordsStatistics)
        {
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            WordsStatistics = wordsStatistics;
        }
    }

    public class WordsStatistic
    {
        public string Word { get; }
        public int Count { get; }

        public WordsStatistic(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}
