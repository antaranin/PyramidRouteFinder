using NUnit.Framework;
using PyramidRouteFinderLib.Algo;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderTest
{
    [TestFixture]
    public class PyramidPathRuleCheckerTest
    {
        [Test, Sequential]
        public void TwoEvenOrOddValues_Validate_FalseReturned(
            [Values(1, 2, 3, 300)] int value,
            [Values(1, 4, 553, 899990)] int nextValue
        )
        {
            //Arrange
            var checker = new PyramidNumericalPathRuleChecker();

            //Act
            var result = checker.IsValidPath(value, nextValue);

            //Assert 
            Assert.IsFalse(result);
        }

        [Test, Sequential]
        public void TwoDifferentValues_Validate_TrueReturned(
            [Values(1, 2, 3, 300)] int value,
            [Values(2, 55, -4, 8001)] int nextValue
        )
        {
            //Arrange
            var checker = new PyramidNumericalPathRuleChecker();

            //Act
            var result = checker.IsValidPath(value, nextValue);

            //Assert 
            Assert.IsTrue(result);
        }
    }
}