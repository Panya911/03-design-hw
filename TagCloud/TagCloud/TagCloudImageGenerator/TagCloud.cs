using System.Collections.Generic;

namespace TagCloud.TagCloudImageGenerator
{
    public class TagCloud
    {
        public readonly int Width;
        public readonly int Height;
        public readonly IEnumerable<Word> Words;

        public TagCloud(int width, int height, IEnumerable<Word> words)
        {
            Width = width;
            Height = height;
            Words = words;
        }
    }
}
