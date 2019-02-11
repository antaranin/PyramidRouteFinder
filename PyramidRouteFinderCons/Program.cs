using System;
using System.Linq;
using JetBrains.Annotations;
using PyramidRouteFinderLib;

namespace PyramidRouteFinderCons
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "../data1.txt";
            var finder = new RouteFinder();
            var route = finder.FindNumericalRouteFromFile(path);
            Console.WriteLine(route);
            Console.WriteLine($"Sum: {route.Steps.Sum()}");
        }

    }
}