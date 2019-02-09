using System.Collections.Generic;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib
{
    internal class PyramidConstructor<T> : IPyramidConstructor<T>
    {
        public Option<Pyramid<T>> ConstructPyramidFromDataLines(IList<IList<T>> dataLines)
        {
            return Option<Pyramid<T>>.None;
        }
    }
}