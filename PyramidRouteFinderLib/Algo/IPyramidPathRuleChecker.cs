using JetBrains.Annotations;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib.Algo
{
    internal interface IPyramidPathRuleChecker<T>
    {
        bool IsValidPath(T currentElement, T nextElement);
    }
}