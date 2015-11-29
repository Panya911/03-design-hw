using System.Collections.Generic;

namespace TagCloud.TagCloudImageGenerator.WordStatisticsBuilders
{
    public interface IWordsStatisticsBuilder
    {
        Statistic BuildStatistic(IEnumerable<string> words);
        IEnumerable<string> BoringWords { get; set; }
        int NeededWords { get; set; }
    }
}
