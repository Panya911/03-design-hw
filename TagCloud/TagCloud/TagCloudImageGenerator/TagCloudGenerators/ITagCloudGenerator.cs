using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.TagCloudImageGenerator.ImageGenerators
{
    public interface ITagCloudGenerator
    {
        int ImageWidth { get; set; }
        int ImageHeight { get; set; }
        FontFamily Font { get; set; }
        List<Color> Colors { get; set; }
        TagCloud GenerateCloud(Statistic statistic);
        string Name { get; }
        int MaxFontSize { get; set; }

    }
}
