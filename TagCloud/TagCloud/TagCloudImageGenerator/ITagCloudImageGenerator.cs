using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.TagCloudImageGenerator
{
    public interface ITagCloudImageGenerator
    {
        ITagCloudImageGenerator SetFont(string fontName);
        ITagCloudImageGenerator SetImageSize(int width, int height);
        ITagCloudImageGenerator SetColorList(List<Color> colors);
        ITagCloudImageGenerator SetBoringWordsFile(string path);
        Image GenerateImage(string path);
    }
}
