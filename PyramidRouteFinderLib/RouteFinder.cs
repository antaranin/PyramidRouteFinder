using System;
using System.Linq;
using JetBrains.Annotations;
using PyramidRouteFinderLib.Algo;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib
{
    public class RouteFinder
    {
        [NotNull] private readonly IFileDataExtractor _fileDataExtractor;
        [NotNull] private readonly IDataParser<int> _numericalDataParser;
        [NotNull] private readonly IPyramidRuleApplier<int> _pyramidRuleApplier;
        [NotNull] private readonly ILongestRouteFinder<int> _longestRouteFinder;

        public RouteFinder()
        {
            var l = ServiceLocator.Locator;

            _fileDataExtractor = l.Get<IFileDataExtractor>();
            _numericalDataParser = l.Get<IDataParser<int>>();
            _pyramidRuleApplier = l.Get<IPyramidRuleApplier<int>>();
            _longestRouteFinder = l.Get<ILongestRouteFinder<int>>();
        }

        internal RouteFinder(
            [NotNull] IFileDataExtractor fileDataExtractor,
            [NotNull] IDataParser<int> numericalDataParser,
            [NotNull] IPyramidRuleApplier<int> pyramidRuleApplier,
            [NotNull] ILongestRouteFinder<int> longestRouteFinder
        )
        {
            _fileDataExtractor = fileDataExtractor;
            _numericalDataParser = numericalDataParser;
            _pyramidRuleApplier = pyramidRuleApplier;
            _longestRouteFinder = longestRouteFinder;
        }

        [NotNull]
        public Route<int> FindNumericalRouteFromFile([NotNull] string filePath)
        {
            return null;
        }
    }
}