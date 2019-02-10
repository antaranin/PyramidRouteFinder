using System;
using System.Collections.Generic;
using NUnit.Framework;
using PyramidRouteFinderLib;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderTest
{
    [TestFixture]
    public class PyramidConstructorTest
    {
        [Test]
        public void EmptyInput_Construct_NoneReturned()
        {
            //Arrange
            var none = Option<Pyramid<int>>.None;

            var constructor = new PyramidConstructor<int>();
            var input = new List<IList<int>>();

            //Act
            var result = constructor.ConstructPyramidFromDataLines(input);

            //Assert 
            Assert.AreSame(none, result);
        }

        [Test]
        public void InputLinesNotInAscendingByOneOrder_Construct_ExceptionIsThrown()
        {
            //Arrange
            var constructor = new PyramidConstructor<int>();
            var invalidInput = new List<IList<int>>
            {
                new List<int> {0, 1},
                new List<int> {2},
                new List<int> {3, 4, 5}
            };

            //Act
            void ConstructOp() => constructor.ConstructPyramidFromDataLines(invalidInput);

            //Assert 
            Assert.Throws<InvalidOperationException>(ConstructOp);
        }

        [Test]
        public void SingleLineInput_Construct_PyramidWithOnlyTopReturned()
        {
            //Arrange
            var expectedResult = new Option<Pyramid<int>>(new Pyramid<int>(1));

            var constructor = new PyramidConstructor<int>();
            var input = new List<IList<int>> { new List<int>{1} };

            //Act
            var result = constructor.ConstructPyramidFromDataLines(input);

            //Assert 
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void NormalInput_Construct_ExpectedPyramidReturned()
        {
            //Arrange
            var p4_1 = new Pyramid<int>(-11);
            var p4_2 = new Pyramid<int>(200);
            var p4_3 = new Pyramid<int>(300);
            var p4_4 = new Pyramid<int>(0);
            var p3_1 = new Pyramid<int>(p4_1, 4, p4_2);
            var p3_2 = new Pyramid<int>(p4_2, 5, p4_3);
            var p3_3 = new Pyramid<int>(p4_3, 6, p4_4);
            var p2_1 = new Pyramid<int>(p3_1, 2, p3_2);
            var p2_2 = new Pyramid<int>(p3_2, 3, p3_3);
            var p1_1 = new Pyramid<int>(p2_1, 1, p2_2);
            var expectedResult = new Option<Pyramid<int>>(p1_1);

            var constructor = new PyramidConstructor<int>();
            var input = new List<IList<int>>
            {
                new List<int>{1},
                new List<int>{2, 3},
                new List<int>{4, 5, 6},
                new List<int>{-11, 200, 300, 0},
            };

            //Act
            var result = constructor.ConstructPyramidFromDataLines(input);

            //Assert 
            Assert.AreEqual(expectedResult, result);
        }
    }
}