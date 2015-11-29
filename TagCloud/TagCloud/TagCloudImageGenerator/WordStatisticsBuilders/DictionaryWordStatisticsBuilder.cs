using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TagCloudImageGenerator.WordStatisticsBuilders
{
    public class DictionaryWordStatisticsBuilder : IWordsStatisticsBuilder
    {
        private HashSet<string> _boringWords;

        public IEnumerable<string> BoringWords
        {
            get { return _boringWords; }
            set { _boringWords = new HashSet<string>(value.Select(w => w.ToLower())); }
        }

        public int NeededWords { get; set; } = 100;

        public Statistic BuildStatistic(IEnumerable<string> words)
        {
            var dict = new Dictionary<string, int>();
            foreach (var word in words.Select(w => w.ToLower())
                .Where(w =>_boringWords==null || !_boringWords.Contains(w)))
            {
                if (!dict.ContainsKey(word))
                    dict[word] = 0;
                dict[word]++;
            }
            return new Statistic(dict.Values.Min(), dict.Values.Max(),
                dict.Select(pair => new WordsStatistic(pair.Key, pair.Value))
                    .OrderByDescending(x => x.Count)
                    .Take(NeededWords));
        }
    }
}
