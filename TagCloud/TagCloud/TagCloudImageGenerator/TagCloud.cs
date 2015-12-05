using System.Collections.Generic;

namespace TagCloud.TagCloudImageGenerator
{
    public class TagCloud
    {
        public readonly int Width;
        public readonly int Height;
        public readonly IEnumerable<TagCloudElement> Elements;

        public TagCloud(int width, int height, IEnumerable<TagCloudElement> elements)
        {
            Width = width;
            Height = height;
            Elements = elements;
        }
    }
}
