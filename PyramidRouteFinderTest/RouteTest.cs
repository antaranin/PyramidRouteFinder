using System.Collections.Generic;
using NUnit.Framework;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderTest
{
    [TestFixture]
    public class RouteTest
    {
        [Test]
        public void RouteWithNumbers_AddFirstStep_NewRouteWithNumberReturned()
        {
            //Arrange
            var route = new Route<int>(new List<int> {2, 3, 4, 5});
            var expectedNewSteps = new List<int> {666, 2, 3, 4, 5};

            //Act
            var newRoute = route.AddFirstStep(666);

            //Assert 
            TestHelper.AssertListContentSame(expectedNewSteps, newRoute.Steps);
        }

        [Test]
        public void RouteWithNumbers_AddFirstStep_OriginalRouteNotChanged()
        {
            //Arrange
            var route = new Route<int>(new List<int> {2, 3, 4, 5});
            var expectedSteps = new List<int> {2, 3, 4, 5};

            //Act
            route.AddFirstStep(666);

            //Assert 
            TestHelper.AssertListContentSame(expectedSteps, route.Steps);
        }
    }
}