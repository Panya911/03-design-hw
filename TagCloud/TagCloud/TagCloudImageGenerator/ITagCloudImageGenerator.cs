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
        ITagCloudImageGenerator SetNeededWordsCount(int count);
        ITagCloudImageGenerator SetMaxFontSize(int size);
        ITagCloudImageGenerator SetImageGenerator(string generatorName);
        Image GenerateImage(string path);
    }
}
