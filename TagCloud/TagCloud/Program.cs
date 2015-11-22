using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Ninject;
using Ninject.Extensions.Conventions;
using TagCloud.TagCloudImageGenerator;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {

            var generator = DiContainer.GetService<ITagCloudImageGenerator>();

            generator.SetFont("courier")
                            .SetColorList(new List<Color> { Color.Blue, Color.Red, Color.Green })
                            .SetBoringWordsFile("boring.txt")
                            .SetImageSize(1500, 1500);

            var image = generator.GenerateImage("Text.txt");
            image.Save("output.png", ImageFormat.Png);
        }
    }
}
