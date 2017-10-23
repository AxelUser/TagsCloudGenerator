using System;
using System.Drawing;
using CommandLine;
using SimpleInjector;
using TagsCloudGenerator.Algorithms;
using TagsCloudGenerator.Layouters;
using TagsCloudGenerator.Readers;
using TagsCloudGenerator.Visualization;
using TagsCloudGenerator.WordFormatters;

namespace TagsCloudGenerator.ConsoleApp
{
    class Program
    {
        private static global::TagsCloudGenerator.TagsCloudGenerator CreateGenerator()
        {
            var container = new Container();
            container.RegisterSingleton<ILayouterAlgorithm>(new SimpleRadialAlgorithm());
            container.RegisterSingleton<ICloudLayouter>(() => new CircularCloudLayouter(Point.Empty,
                container.GetInstance<ILayouterAlgorithm>()));
            container.RegisterSingleton<IWordsReader, SimpleTxtReader>();
            container.RegisterSingleton<ILayoutPainter, SimpleLayoutPainter>();
            container.RegisterCollection<IWordFormatter>(new SimpleWordFormatter());
            container.RegisterSingleton<WordsNormalizer>();
            container.RegisterSingleton<WordsLayouter>();
            container.RegisterSingleton<TagsCloudGenerator>();

            container.Verify();

            return container.GetInstance<TagsCloudGenerator>();
        }

        static void Main(string[] args)
        {
            var generator = CreateGenerator();
            Parser.Default.ParseArguments<TagsCloudOptions>(args)
                .WithParsed(opt =>
                {
                    Console.WriteLine("Started");
                    if (string.IsNullOrWhiteSpace(opt.OutputFile))
                    {
                        opt.OutputFile = $"{Guid.NewGuid():N}.png";
                    }
                    generator.SaveCloud(opt.InputFile, opt.Width, opt.Height, opt.OutputFile, opt.Format);
                    Console.WriteLine($"Saved at {opt.OutputFile}");
                });
            ;
        }
    }
}
