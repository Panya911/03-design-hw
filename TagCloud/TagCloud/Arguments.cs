using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace TagCloud
{
    class Arguments
    {
        private readonly HashSet<string> _imageGenerators;

        public Arguments(IEnumerable<string> imageGenerators)
        {
            _imageGenerators = new HashSet<string>(imageGenerators);
        }

        [Option('g', "generator", DefaultValue = "spiral", HelpText = "Cloud image generator. (see bellow)")]
        public string ImageGenerator { get; set; }
        [Option('f', "font", DefaultValue = "courier", HelpText = "Font name")]
        public string Font { get; set; }
        [Option('b', "boringWords", DefaultValue = "boring.txt",
            HelpText = "Path to file with words that result won't be consider")]
        public string BoringWordsFile { get; set; }
        [Option('w', "width", DefaultValue = 1500,
            HelpText = "Result image width")]
        public int ImageWidth { get; set; }
        [Option('h', "height", DefaultValue = 1500,
            HelpText = "Result image height")]
        public int ImageHeight { get; set; }
        [Option('i', "input", DefaultValue = "Text.txt",
            HelpText = "Path to file with text")]
        public string InputPath { get; set; }

        [Option('e',"extension",DefaultValue = "png",HelpText = "output file extension")]//выводить только доступные форматы
        public string OutputExtension { get; set; }
        
        [Option('o', "output", DefaultValue = "output.png",
            HelpText = "Path to file where image will be saved")]
        public string OutputPath { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText { AddDashesToOption = true };
            help.AddOptions(this);
            help.AddPostOptionsLine("Image generator. One of: "+GetGeneratorsNames());
            return help;
        }

        private string GetGeneratorsNames()
        {
            var builder = new StringBuilder();
            builder.Append(_imageGenerators.Skip(1)
                .Aggregate(_imageGenerators.FirstOrDefault(), (seed, name) => seed + ',' + name));
            return builder.ToString();
        }
    }
}