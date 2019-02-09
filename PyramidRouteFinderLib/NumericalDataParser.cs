using System.Collections.Generic;
using JetBrains.Annotations;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib
{
    internal class NumericalDataParser : IDataParser<int>
    {
        [NotNull] private readonly IPyramidConstructor<int> _pyramidConstructor;

        public NumericalDataParser([NotNull] IPyramidConstructor<int> pyramidConstructor)
        {
            _pyramidConstructor = pyramidConstructor;
        }

        public Option<Pyramid<int>> ParseIntoPyramid(IEnumerable<string> data)
        {
            return Option<Pyramid<int>>.None;
        }

        private IList<IList<int>> ExtractDataLines(IEnumerable<string> data)
        {
            throw new System.NotImplementedException("Pyramid fail");
        }
    }
}