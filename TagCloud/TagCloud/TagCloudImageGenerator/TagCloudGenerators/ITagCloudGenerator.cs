using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.TagCloudImageGenerator.ImageGenerators
{
    public interface IImageGenerator
    {
        int ImageWidth { get; set; }
        int ImageHeight { get; set; }
        FontFamily Font { get; set; }
        List<Color> Colors { get; set; }
        Image GenerateImage(Statistic statistic);
        string Name { get; }
        int MaxFontSize { get; set; }

    }
}
