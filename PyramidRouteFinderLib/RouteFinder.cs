using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using PyramidRouteFinderLib.Algo;
using PyramidRouteFinderLib.Model;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PyramidRouteFinderTest")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

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
            if (filePath == null)
                throw new ArgumentNullException($"{nameof(filePath)} cannot be null");

            var stringData = _fileDataExtractor.ExtractLines(filePath);
            var resultingPyramid = _numericalDataParser.ParseIntoPyramid(stringData);

            return resultingPyramid.Map(pyramid => _pyramidRuleApplier.TransformPyramid(pyramid))
                                   .Map(transformedPyramid => _longestRouteFinder.FindLongestRoute(transformedPyramid))
                                   .Fold(r => r, () => new Route<int>(new List<int>()));
        }
    }
}