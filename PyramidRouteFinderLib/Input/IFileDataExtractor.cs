using System.Collections.Generic;
using JetBrains.Annotations;

namespace PyramidRouteFinderLib
{
    internal interface IFileDataExtractor
    {
        [NotNull, ItemNotNull]
        IEnumerable<string> ExtractLines([NotNull] string filePath);
    }
}