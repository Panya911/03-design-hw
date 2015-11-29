using System;
using System.Collections.Generic;

namespace TagCloud.TagCloudImageGenerator.WordsReaders
{
    public abstract class WordReaderBase : IWordReader
    {
        public IEnumerable<string> ReadAllWords(string path)
        {
            var text = GetAllText(path);
            return text.Split(new[] { ' ', '\n', '\t', '\r', '.', ',', ':', '`', '(', ')', '?', '!', '\"', '”' },
                StringSplitOptions.RemoveEmptyEntries);
        }

        protected abstract string GetAllText(string path);

        public abstract string FileExtension { get; }
    }
}
