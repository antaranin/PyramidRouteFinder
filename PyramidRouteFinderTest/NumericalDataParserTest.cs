using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PyramidRouteFinderLib;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderTest
{
    [TestFixture]
    public class NumericalDataParserTest
    {
        [Test]
        public void EmptyInput_Parse_PyramidConstructorCalledWithEmptyInput()
        {
            //Arrange
            var constructor = new Mock<IPyramidConstructor<int>>();
            var parser = new NumericalDataParser(constructor.Object);

            //Act
            parser.ParseIntoPyramid(new List<string>());

            //Assert 
            constructor.Verify(m => m.ConstructPyramidFromDataLines(new List<IList<int>>()), Times.Once);
            constructor.VerifyNoOtherCalls();
        }

        [Test]
        public void NormalInput_Parse_PyramidConstructorCalledWithParsedInput()
        {
            //Arrange
            var constructor = new Mock<IPyramidConstructor<int>>();
            var parser = new NumericalDataParser(constructor.Object);

            var input = new List<string>
            {
                "3 5 10",
                "-1",
                "99 100 101 102 103",
                "2 0"
            };

            var expectedOutput = new List<IList<int>>
            {
                new List<int> {3, 5, 10},
                new List<int> {-1},
                new List<int> {99, 100, 101, 102, 103},
                new List<int> {2, 0},
            };

            //Act
            parser.ParseIntoPyramid(input);

            //Assert 
            constructor.Verify(m =>
                    m.ConstructPyramidFromDataLines(
                        It.Is<IList<IList<int>>>(l => VerifyListContentsSame(l, expectedOutput))),
                Times.Once
            );
            constructor.VerifyNoOtherCalls();
        }

        [Test]
        public void Input_Parse_ReturnResultFromPyramidConstructor()
        {
            //Arrange
            var input = new List<string> {"33 22 -2"};
            var expectedResult = new Option<Pyramid<int>>(new Pyramid<int>(75, new Pyramid<int>(100)));
            var constructor = new Mock<IPyramidConstructor<int>>();
            constructor.Setup(m => m.ConstructPyramidFromDataLines(It.IsAny<IList<IList<int>>>()))
                       .Returns(expectedResult);

            var parser = new NumericalDataParser(constructor.Object);

            //Act
            var actualResult = parser.ParseIntoPyramid(input);

            //Assert 
            Assert.AreEqual(expectedResult, actualResult);
        }

        private static bool VerifyListContentsSame<T>(ICollection<IList<T>> list1, ICollection<IList<T>> list2)
        {
            bool SubListContentSame(IList<T> l1, IList<T> l2) =>
                l1.Count == l2.Count && l1.Zip(l2, (el1, el2) => Equals(el1, el2)).All(el => el);

            return list1.Count == list2.Count && list1.Zip(list2, SubListContentSame).All(el => el);
        }
    }
}