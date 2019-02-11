using System;
using System.Collections.Generic;
using System.Linq;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib.Algo
{
    internal class LongestNumericalRouteFinder : ILongestRouteFinder<int>
    {
        private IDictionary<Pyramid<int>, NumericRoute> _memorizer;

        public Route<int> FindLongestRoute(Pyramid<int> pyramid)
        {
            _memorizer = new Dictionary<Pyramid<int>, NumericRoute>();
            var res =  FindLongestRouteWithMem(pyramid);
            return res;
        }

        private NumericRoute FindLongestRouteWithMem(Pyramid<int> pyramid)
        {
            if (_memorizer.ContainsKey(pyramid))
                return _memorizer[pyramid];

            var leftRoute = pyramid.Left.Map(FindLongestRouteWithMem);
            var rightRoute = pyramid.Right.Map(FindLongestRouteWithMem);
            var route = ChooseLongerRoute(leftRoute, rightRoute)
                .Fold(r => r.AddFirstStep(pyramid.Value),
                    () => new NumericRoute(new List<int> {pyramid.Value})
                );
            _memorizer.Add(pyramid, route);
            return route;
        }

        private Option<NumericRoute> ChooseLongerRoute(Option<NumericRoute> left, Option<NumericRoute> right)
        {
            return left.Zip(
                right,
                (l, r) =>
                {
                    if (l.Steps.Count == r.Steps.Count)
                        return l.Sum > r.Sum ? l : r;
                    return l.Steps.Count > r.Steps.Count ? l : r;
                }
            );
        }
    }
}