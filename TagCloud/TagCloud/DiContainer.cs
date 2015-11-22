using Ninject;
using Ninject.Extensions.Conventions;
using TagCloud.TagCloudImageGenerator;
using TagCloud.TagCloudImageGenerator.ImageGenerators;
using TagCloud.TagCloudImageGenerator.WordsReaders;
using TagCloud.TagCloudImageGenerator.WordStatisticsBuilders;

namespace TagCloud
{
    public static class DiContainer
    {
        private static readonly StandardKernel Container = new StandardKernel();

        static DiContainer()
        {
            BindDependencies();  
        }

        public static T GetService<T>()
        {
            return Container.Get<T>();
        }

        private static void BindDependencies()
        {

            Container.Bind(c => c.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<IWordReader>()
                .BindAllInterfaces());

            Container.Bind<IWordsStatisticsBuilder>().To<DictionaryWordStatisticsBuilder>();

            Container.Bind(c => c.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<IImageGenerator>()
                .BindAllInterfaces());

            Container.Bind<ITagCloudImageGenerator>().To<TagCloudImageGenerator.TagCloudImageGenerator>();

        }
    }
}
