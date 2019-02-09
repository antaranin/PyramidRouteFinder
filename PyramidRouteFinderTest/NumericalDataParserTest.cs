using System.Collections.Generic;
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
            constructor.Verify(m => m.ConstructPyramidFromDataLines(expectedOutput), Times.Once);
            constructor.VerifyNoOtherCalls();
        }

        [Test]
        public void Input_Parse_ReturnResultFromPyramidConstructor()
        {
            //Arrange
            var input = new List<string> {"33 22 -2"};
            var parsedInput = new List<IList<int>> {new List<int> {33, 22, -2}};
            var result = new Pyramid<int>(75, new Pyramid<int>(100));
            var constructor = new Mock<IPyramidConstructor<int>>();
            constructor.Setup(m => m.ConstructPyramidFromDataLines(parsedInput))
                       .Returns(new Option<Pyramid<int>>(result));

            var parser = new NumericalDataParser(constructor.Object);

            //Act
            var actualResult = parser.ParseIntoPyramid(input);

            //Assert 
            Assert.AreEqual(result, actualResult);
        }
    }
}