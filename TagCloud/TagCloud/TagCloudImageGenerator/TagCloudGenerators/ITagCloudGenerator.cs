using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.TagCloudImageGenerator.TagCloudGenerators
{
    public interface ITagCloudGenerator
    {
        int ImageWidth { get; set; }
        int ImageHeight { get; set; }
        FontFamily Font { get; set; }
        List<Color> Colors { get; set; }
        TagCloud GenerateCloud(Statistic statistic, Func<string, Font, Size> measureString);
        string Name { get; }
        int MaxFontSize { get; set; }
    }
}
