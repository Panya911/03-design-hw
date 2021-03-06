﻿using System.Collections.Generic;
using Ninject;
using Ninject.Extensions.Conventions;
using TagCloud.TagCloudImageGenerator;
using TagCloud.TagCloudImageGenerator.CloudDrawer;
using TagCloud.TagCloudImageGenerator.TagCloudGenerators;
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

        public static IEnumerable<T> GetAllServices<T>()
        {
            return Container.GetAll<T>();
        }

        private static void BindDependencies()
        {

            Container.Bind(c => c.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<IWordReader>()
                .BindAllInterfaces());

            Container.Bind<IWordsStatisticsBuilder>().To<DictionaryWordStatisticsBuilder>();
            Container.Bind<ITagCloudImageDrawer>().To<TagCloudImageDrawer>();

            Container.Bind(c => c.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<ITagCloudGenerator>()
                .BindAllInterfaces());

            Container.Bind<ITagCloudImageGenerator>().To<TagCloudImageGenerator.TagCloudImageGenerator>();

        }
    }
}
