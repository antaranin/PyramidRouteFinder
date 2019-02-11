using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace PyramidRouteFinderLib.Model
{
    public class Route<T>
    {
        public IList<T> Steps { get; }
        
        internal Route(IList<T> steps)
        {
            Steps = steps;
        }

        [NotNull, Pure]
        internal Route<T> AddFirstStep(T step)
        {
            var newSteps = Steps.Prepend(step).ToList();
            return new Route<T>(newSteps);
        }
    }

    internal class NumericRoute : Route<int>
    {
        private readonly Lazy<int> _sum;
        internal int Sum => _sum.Value;
        internal NumericRoute(IList<int> steps) : base(steps)
        {
            _sum  = new Lazy<int>(() => Steps.Sum());
        }
        
        [NotNull, Pure]
        internal new NumericRoute AddFirstStep(int step)
        {
            var newSteps = Steps.Prepend(step).ToList();
            return new NumericRoute(newSteps);
        }
        
    }
}