using JetBrains.Annotations;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib.Algo
{
    internal interface IPyramidRuleApplier<T>
    {
        [NotNull] Pyramid<T> TransformPyramid([NotNull] Pyramid<T> pyramid);
    }
}