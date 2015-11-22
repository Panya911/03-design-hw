using System.Collections.Generic;

namespace TagCloud.TagCloudImageGenerator.WordsReaders
{
    public interface IWordReader
    {
        IEnumerable<string> ReadAllWords(string path);
        string FileExtension { get; }
    }
}
