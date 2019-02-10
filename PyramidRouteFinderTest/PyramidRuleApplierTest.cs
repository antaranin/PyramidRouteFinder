using Moq;
using NUnit.Framework;
using PyramidRouteFinderLib.Algo;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderTest
{
    [TestFixture]
    public class PyramidRuleApplierTest
    {
        [Test]
        public void PyramidMatchesRules_Transform_IdenticalPyramidReturned()
        {
            //Arrange
            var pyramid = CreateTestPyramid();
            var checker = new Mock<IPyramidPathRuleChecker<int>>();
            checker.Setup(m => m.IsValidPath(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var applier = new PyramidRuleApplier<int>(checker.Object);

            //Act
            var result = applier.TransformPyramid(pyramid);

            //Assert 
            Assert.AreEqual(pyramid, result);
        }

        [Test]
        public void SomePathsDoNotMatchRules_Transform_NotMatchingPathsNotInReturnedPyramid()
        {
            //Arrange
            var pyramid = CreateTestPyramid();
            var p3_2 = new Pyramid<int>(7);
            var p3_3 = new Pyramid<int>(6);
            var p2_1 = new Pyramid<int>(2, p3_2);
            var p2_2 = new Pyramid<int>(3, p3_3);
            var expectedResult = new Pyramid<int>(p2_1, 1, p2_2);

            var checker = new Mock<IPyramidPathRuleChecker<int>>();
            checker.Setup(m => m.IsValidPath(It.IsAny<int>(), It.IsAny<int>()))
                   .Returns((int val1, int val2) => val1 + val2 < 10);

            var applier = new PyramidRuleApplier<int>(checker.Object);

            //Act
            var result = applier.TransformPyramid(pyramid);

            //Assert 
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void UpperPathsDoNotMatchRules_Transform_UnaccesiblePathsNotChecked()
        {
            //Arrange
            var pyramid = CreateTestPyramid();
            var checker = new Mock<IPyramidPathRuleChecker<int>>();
            checker.Setup(m => m.IsValidPath(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            var applier = new PyramidRuleApplier<int>(checker.Object);

            //Act
            var result = applier.TransformPyramid(pyramid);

            //Assert 
            Assert.AreEqual(new Pyramid<int>(1), result);
            checker.Verify(m => m.IsValidPath(1, 2), Times.Once);
            checker.Verify(m => m.IsValidPath(1, 3), Times.Once);
            checker.VerifyNoOtherCalls();
        }

        private static Pyramid<int> CreateTestPyramid()
        {
            var p4_1 = new Pyramid<int>(7);
            var p4_2 = new Pyramid<int>(8);
            var p4_3 = new Pyramid<int>(9);
            var p4_4 = new Pyramid<int>(10);
            var p3_1 = new Pyramid<int>(p4_1, 8, p4_2);
            var p3_2 = new Pyramid<int>(p4_2, 7, p4_3);
            var p3_3 = new Pyramid<int>(p4_3, 6, p4_4);
            var p2_1 = new Pyramid<int>(p3_1, 2, p3_2);
            var p2_2 = new Pyramid<int>(p3_2, 3, p3_3);
            var p1_1 = new Pyramid<int>(p2_1, 1, p2_2);
            return p1_1;
        }
    }
}