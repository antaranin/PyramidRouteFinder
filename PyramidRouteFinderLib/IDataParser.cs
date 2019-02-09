using System.Collections.Generic;
using JetBrains.Annotations;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib
{
    internal interface IDataParser<T>
    {
        [NotNull]
        Option<Pyramid<T>> ParseIntoPyramid([NotNull, ItemNotNull] IEnumerable<string> data);
    }
}