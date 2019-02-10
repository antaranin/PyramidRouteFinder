using JetBrains.Annotations;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib.Algo
{
    internal interface ILongestRouteFinder<T>
    {
        [NotNull] Route<T> FindLongestRoute([NotNull] Pyramid<T> pyramid);
    }
}