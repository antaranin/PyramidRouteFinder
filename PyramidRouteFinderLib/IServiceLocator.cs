using JetBrains.Annotations;

namespace PyramidRouteFinderLib
{
    internal interface IServiceLocator
    {
        [NotNull] T Get<T>();
    }
}