using System;
using System.Collections.Generic;
using System.Linq;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib
{
    internal class PyramidConstructor<T> : IPyramidConstructor<T>
    {
        public Option<Pyramid<T>> ConstructPyramidFromDataLines(IList<IList<T>> dataLines)
        {
            var previousRow = Option<IList<Pyramid<T>>>.None;
            for (var i = dataLines.Count - 1; i >= 0; i--)
            {
                var dataRow = dataLines[i];
                if (dataRow.Count != i + 1)
                    throw new InvalidOperationException($"Malformed pyramid");
                previousRow = new Option<IList<Pyramid<T>>>(ConstructPyramidRow(dataRow, previousRow));
            }

            return previousRow.Map(val => val.Single());
        }

        private IList<Pyramid<T>> ConstructPyramidRow(IList<T> pyramidDataRow, Option<IList<Pyramid<T>>> previousRow)
        {
            return previousRow.Fold(
                row => ConstructPyramidRowWithPreviousRow(pyramidDataRow, row),
                () => ConstructBottomPyramidRow(pyramidDataRow)
            );
        }

        private static IList<Pyramid<T>> ConstructPyramidRowWithPreviousRow(
            IList<T> pyramidDataRow,
            IList<Pyramid<T>> previousRow)
        {
            return pyramidDataRow.Select((el, i) => new Pyramid<T>(previousRow[i], el, previousRow[i + 1])).ToList();
        }

        private static IList<Pyramid<T>> ConstructBottomPyramidRow(IList<T> pyramidDataRow)
        {
            return pyramidDataRow.Select(element => new Pyramid<T>(element)).ToList();
        }
    }
}