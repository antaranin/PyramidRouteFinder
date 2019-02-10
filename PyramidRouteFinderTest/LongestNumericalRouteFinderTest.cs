using System.Collections.Generic;
using NUnit.Framework;
using PyramidRouteFinderLib.Algo;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderTest
{
    [TestFixture]
    public class LongestNumericalRouteFinderTest
    {
        [Test]
        public void OneItemPyramid_FindRoute_FirstValueRouteReturned()
        {
            //Arrange
            var pyramid = new Pyramid<int>(33);

            var finder = new LongestNumericalRouteFinder();

            //Act
            var result = finder.FindLongestRoute(pyramid);

            //Assert 
            Assert.NotNull(result);
            TestHelper.AssertListContentSame(new List<int> {33}, result.Steps);
        }

        [Test]
        public void PyramidWithPath1_FindRoute_CorrectRouteReturned()
        {
            //Arrange
            //      07
            //     30 02
            //   07 12 48
            //  40 55 02 -1
            //19 -2 77 10 99
            //Expected: 07 -> 30 -> 12 -> 55 -> 77
            var expectedRes = new List<int> {07, 30, 12, 55, 77};
            var p5_1 = new Pyramid<int>(19);
            var p5_2 = new Pyramid<int>(-2);
            var p5_3 = new Pyramid<int>(77);
            var p5_4 = new Pyramid<int>(10);
            var p5_5 = new Pyramid<int>(99);
            var p4_1 = new Pyramid<int>(p5_1, 40, p5_2);
            var p4_2 = new Pyramid<int>(p5_2, 55, p5_3);
            var p4_3 = new Pyramid<int>(p5_3, 02, p5_4);
            var p4_4 = new Pyramid<int>(p5_4, -1, p5_5);
            var p3_1 = new Pyramid<int>(p4_1, 07, p4_1);
            var p3_2 = new Pyramid<int>(p4_2, 12, p4_3);
            var p3_3 = new Pyramid<int>(p4_3, 48, p4_4);
            var p2_1 = new Pyramid<int>(p3_1, 30, p3_2);
            var p2_2 = new Pyramid<int>(p3_2, 02, p3_3);
            var top = new Pyramid<int>(p2_1, 07, p2_2);

            var finder = new LongestNumericalRouteFinder();

            //Act
            var result = finder.FindLongestRoute(top);

            //Assert 
            Assert.NotNull(result);
            TestHelper.AssertListContentSame(expectedRes, result.Steps);
        }

        [Test]
        public void PyramidWithPath2_FindRoute_CorrectRouteReturned()
        {
            //Arrange
            //      07
            //     30 02
            //   07 12 48
            //  40 55 17 -1
            //19 -2 -8 10 99
            //Expected: 07 -> 02 -> 48 -> -1 -> 99
            var expectedRes = new List<int> {07, 02, 48, -1, 99};
            var p5_1 = new Pyramid<int>(19);
            var p5_2 = new Pyramid<int>(-2);
            var p5_3 = new Pyramid<int>(-8);
            var p5_4 = new Pyramid<int>(10);
            var p5_5 = new Pyramid<int>(99);
            var p4_1 = new Pyramid<int>(p5_1, 40, p5_2);
            var p4_2 = new Pyramid<int>(p5_2, 55, p5_3);
            var p4_3 = new Pyramid<int>(p5_3, 17, p5_4);
            var p4_4 = new Pyramid<int>(p5_4, -1, p5_5);
            var p3_1 = new Pyramid<int>(p4_1, 07, p4_1);
            var p3_2 = new Pyramid<int>(p4_2, 12, p4_3);
            var p3_3 = new Pyramid<int>(p4_3, 48, p4_4);
            var p2_1 = new Pyramid<int>(p3_1, 30, p3_2);
            var p2_2 = new Pyramid<int>(p3_2, 02, p3_3);
            var top = new Pyramid<int>(p2_1, 07, p2_2);

            var finder = new LongestNumericalRouteFinder();

            //Act
            var result = finder.FindLongestRoute(top);

            //Assert 
            Assert.NotNull(result);
            TestHelper.AssertListContentSame(expectedRes, result.Steps);
        }

        [Test]
        public void PyramidWithHoles_FindRoute_CorrectRouteFound()
        {
            //Arrange
            //         07
            //       30 02
            //      07 12 48
            //     40 55    -1
            //    11 -2 55 55 55
            //  12 55    11 99   
            //19 -2 -8 10    45   
            //Expected: 07 -> 02 -> 48 -> -1 -> 55 -> 99 -> 45
            var expectedRes = new List<int> {07, 02, 48, -1, 55, 99, 45};
            var p7_1 = new Pyramid<int>(19);
            var p7_2 = new Pyramid<int>(-2);
            var p7_3 = new Pyramid<int>(-8);
            var p7_4 = new Pyramid<int>(10);
            var p7_6 = new Pyramid<int>(45);
            var p6_1 = new Pyramid<int>(p7_1, 12, p7_2);
            var p6_2 = new Pyramid<int>(p7_2, 55, p7_3);
            var p6_4 = new Pyramid<int>(p7_4, 11);
            var p6_5 = new Pyramid<int>(99, p7_6);
            var p5_1 = new Pyramid<int>(p6_1, 11, p6_2);
            var p5_2 = new Pyramid<int>(p6_2, -2);
            var p5_3 = new Pyramid<int>(55, p6_4);
            var p5_4 = new Pyramid<int>(p6_4, 55, p6_5);
            var p5_5 = new Pyramid<int>(p6_5, 55);
            var p4_1 = new Pyramid<int>(p5_1, 40, p5_2);
            var p4_2 = new Pyramid<int>(p5_2, 55, p5_3);
            var p4_4 = new Pyramid<int>(p5_4, -1, p5_5);
            var p3_1 = new Pyramid<int>(p4_1, 07, p4_1);
            var p3_2 = new Pyramid<int>(p4_2, 12);
            var p3_3 = new Pyramid<int>(48, p4_4);
            var p2_1 = new Pyramid<int>(p3_1, 30, p3_2);
            var p2_2 = new Pyramid<int>(p3_2, 02, p3_3);
            var top = new Pyramid<int>(p2_1, 07, p2_2);

            var finder = new LongestNumericalRouteFinder();

            //Act
            var result = finder.FindLongestRoute(top);

            //Assert 
            Assert.NotNull(result);
            TestHelper.AssertListContentSame(expectedRes, result.Steps);
        }

        [Test]
        public void PyramidWithHolesMaxRouteNotTillEnd_FindRoute_RouteLeadingToEndReturned()
        {
            //Arrange
            //         07
            //       30 02
            //      07    99
            //     40 55    99
            //    11 -2 55 55 99
            //  12 55 90       99  
            //19 -2 -8 10         
            //Expected: 07 -> 30 -> 07 -> 55 -> 55 -> 90 -> 10
            var expectedRes = new List<int> {07, 30, 07, 55, 55, 90, 10};
            var p7_1 = new Pyramid<int>(19);
            var p7_2 = new Pyramid<int>(-2);
            var p7_3 = new Pyramid<int>(-8);
            var p7_4 = new Pyramid<int>(10);
            var p6_1 = new Pyramid<int>(p7_1, 12, p7_2);
            var p6_2 = new Pyramid<int>(p7_2, 55, p7_3);
            var p6_3 = new Pyramid<int>(p7_3, 90, p7_4);
            var p6_6 = new Pyramid<int>(99);
            var p5_1 = new Pyramid<int>(p6_1, 11, p6_2);
            var p5_2 = new Pyramid<int>(p6_2, -2, p6_3);
            var p5_3 = new Pyramid<int>(p6_3, 55);
            var p5_4 = new Pyramid<int>(55);
            var p5_5 = new Pyramid<int>(99, p6_6);
            var p4_1 = new Pyramid<int>(p5_1, 40, p5_2);
            var p4_2 = new Pyramid<int>(p5_2, 55, p5_3);
            var p4_4 = new Pyramid<int>(p5_4, 99, p5_5);
            var p3_1 = new Pyramid<int>(p4_1, 07, p4_1);
            var p3_2 = new Pyramid<int>(p4_2, 12);
            var p3_3 = new Pyramid<int>(99, p4_4);
            var p2_1 = new Pyramid<int>(p3_1, 30, p3_2);
            var p2_2 = new Pyramid<int>(p3_2, 02, p3_3);
            var top = new Pyramid<int>(p2_1, 07, p2_2);

            var finder = new LongestNumericalRouteFinder();

            //Act
            var result = finder.FindLongestRoute(top);

            //Assert 
            Assert.NotNull(result);
            TestHelper.AssertListContentSame(expectedRes, result.Steps);
        }
    }
}