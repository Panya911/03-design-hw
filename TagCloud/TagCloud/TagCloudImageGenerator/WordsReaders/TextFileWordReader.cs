using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloud.TagCloudImageGenerator.WordsReaders
{
    public class TextFileWordReader : WordReaderBase
    {
        protected override string GetAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public override string FileExtension => ".txt";
    }
}
