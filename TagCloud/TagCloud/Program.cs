using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using CommandLine;
using TagCloud.TagCloudImageGenerator;
using TagCloud.TagCloudImageGenerator.ImageGenerators;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            var imageGeneratorsNames = DiContainer.GetAllServices<IImageGenerator>()
                .Select(g => g.Name);
            //todo выводить сообщение, если переданы лишние параметры
            var arguments = new Arguments(imageGeneratorsNames);
            if (!Parser.Default.ParseArguments(args, arguments))
                return;

            var generator = DiContainer.GetService<ITagCloudImageGenerator>();

            try
            {
                generator.SetFont(arguments.Font)
                    .SetColorList(new List<Color> {Color.Blue, Color.Red, Color.Green})
                    .SetBoringWordsFile(arguments.BoringWordsFile)
                    .SetImageSize(arguments.ImageWidth, arguments.ImageHeight)
                    .SetImageGenerator(arguments.ImageGenerator);
            }
            catch (TagCloudImageGeneratorTuningException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            var image = generator.GenerateImage(arguments.InputPath);
            image.Save(arguments.OutputPath, ImageFormat.Png);
        }
    }
}
