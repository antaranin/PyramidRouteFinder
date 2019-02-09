using System;
using System.Linq;
using JetBrains.Annotations;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib
{
    public class RouteFinder
    {
        [NotNull]
        public Route<int> FindNumericalRouteFromFile([NotNull] string filePath)
        {
            var fileExtractor = ServiceLocator.Locator.Get<IFileDataExtractor>();
            var lines = fileExtractor.ExtractLines(filePath).Aggregate((line, acc) => $"{line}\n{acc}");
            Console.WriteLine(lines);
            throw new NotImplementedException("Fail from route finder");
        }
    }
}