using System;
using JetBrains.Annotations;

namespace PyramidRouteFinderLib
{
    internal static class ServiceLocator
    {
        [NotNull] private static Lazy<IServiceLocator> locator = new Lazy<IServiceLocator>(new NinjectServiceLocator());

        [NotNull]
        public static IServiceLocator Locator => locator.Value;
    }
}