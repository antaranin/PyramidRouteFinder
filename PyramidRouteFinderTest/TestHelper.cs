using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PyramidRouteFinderTest
{
    public static class TestHelper
    {
        public static void AssertListContentSame<T>(IList<T> l1, IList<T> l2)
        {
            if (l1.Count != l2.Count || !l1.Zip(l2, (el1, el2) => Equals(el1, el2)).All(el => el))
                throw new AssertionException(
                    $"L1: \n{ListVerboseToString(l1)} \ndiffers from L2: \n{ListVerboseToString(l2)}");
        }

        private static string ListVerboseToString<T>(IList<T> list)
        {
            return list.Aggregate("", (acc, el) => $"{acc}, {el}").Remove(0, 2);
        }
    }
}