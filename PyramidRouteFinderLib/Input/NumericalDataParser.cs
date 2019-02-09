using System.Collections.Generic;
using System.Linq;
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
            var formattedData = ExtractDataLines(data);
            return _pyramidConstructor.ConstructPyramidFromDataLines(formattedData);
        }

        private IList<IList<int>> ExtractDataLines(IEnumerable<string> data)
        {
            return data.Select(d => d.Split(" ").Select(int.Parse).ToList() as IList<int>).ToList();
        }
    }
}