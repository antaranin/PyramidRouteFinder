using System.Collections;
using System.Collections.Generic;

namespace PyramidRouteFinderLib.Model
{
    public class Route<T>
    {
        public IList<T> Steps { get; }
        
        internal Route(IList<T> steps)
        {
            Steps = steps;
        }
    }
}