using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloud.TagCloudImageGenerator.WordsReaders
{
    public class TextFileWordReader : IWordReader
    {
        
        public IEnumerable<string> ReadAllWords(string path)
        {
            var text= File.ReadAllText(path);
            text = text.Replace(" ", Environment.NewLine);
            return text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string FileExtension => ".txt";
    }
}
