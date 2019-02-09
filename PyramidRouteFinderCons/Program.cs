using System;
using System.Linq;
using JetBrains.Annotations;
using PyramidRouteFinderLib;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderCons
{
    class Program
    {
        static void Main(string[] args)
        {
//          Console.WriteLine("Insert data file path");
//          var path = Console.ReadLine();
            const string path = "../data0.txt";
            var finder = new RouteFinder();
            var route = finder.FindNumericalRouteFromFile(path);
            Console.WriteLine(FormatRoute(route));
        }

        [NotNull]
        private static string FormatRoute<T>(Route<T> route)
        {
            return route.Steps.Select(step => $" => {step}")
                        .Aggregate("start", (el, acc) => el + acc);
        }
    }
}