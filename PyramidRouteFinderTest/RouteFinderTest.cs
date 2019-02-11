using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PyramidRouteFinderLib;
using PyramidRouteFinderLib.Algo;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderTest
{
    [TestFixture]
    public class RouteFinderTest
    {
        [Test]
        public void NullPathPassed_FindNumericalRoute_ExceptionIsThrown()
        {
            //Arrange
            var extractor = new Mock<IFileDataExtractor>();
            var dataParser = new Mock<IDataParser<int>>();
            var ruleApplier = new Mock<IPyramidRuleApplier<int>>();
            var longestRouteFinder = new Mock<ILongestRouteFinder<int>>();

            var routeFinder = new RouteFinder(
                extractor.Object,
                dataParser.Object,
                ruleApplier.Object,
                longestRouteFinder.Object
            );

            //Act
            void FindOp() => routeFinder.FindNumericalRouteFromFile(null);

            //Assert 
            Assert.Throws<ArgumentNullException>(FindOp);
        }

        [Test]
        public void PathPassed_FindNumericalRoute_FileDataExtractorCalledWithPath()
        {
            //Arrange
            const string path = "Some path";
            var extractor = new Mock<IFileDataExtractor>();
            extractor.Setup(m => m.ExtractLines(path)).Returns(new string[0]);
            var dataParser = new Mock<IDataParser<int>>();
            dataParser.Setup(m => m.ParseIntoPyramid(It.IsAny<IEnumerable<string>>()))
                      .Returns(Option<Pyramid<int>>.None);
            var ruleApplier = new Mock<IPyramidRuleApplier<int>>();
            var longestRouteFinder = new Mock<ILongestRouteFinder<int>>();

            var routeFinder = new RouteFinder(
                extractor.Object,
                dataParser.Object,
                ruleApplier.Object,
                longestRouteFinder.Object
            );

            //Act
            routeFinder.FindNumericalRouteFromFile(path);

            //Assert 
            extractor.Verify(m => m.ExtractLines(path), Times.Once);
            extractor.VerifyNoOtherCalls();
        }

        [Test]
        public void DataExtractedFromFile_FindNumericalRoute_DataParserCalledWithData()
        {
            //Arrange
            const string path = "Some path";
            var stringData = new[] {"blah", "blah blah"};

            var extractor = new Mock<IFileDataExtractor>();
            extractor.Setup(m => m.ExtractLines(path)).Returns(stringData);

            var dataParser = new Mock<IDataParser<int>>();
            dataParser.Setup(m => m.ParseIntoPyramid(stringData)).Returns(Option<Pyramid<int>>.None);

            var ruleApplier = new Mock<IPyramidRuleApplier<int>>();
            var longestRouteFinder = new Mock<ILongestRouteFinder<int>>();

            var routeFinder = new RouteFinder(
                extractor.Object,
                dataParser.Object,
                ruleApplier.Object,
                longestRouteFinder.Object
            );

            //Act
            routeFinder.FindNumericalRouteFromFile(path);

            //Assert 
            dataParser.Verify(m => m.ParseIntoPyramid(stringData), Times.Once);
            dataParser.VerifyNoOtherCalls();
        }

        [Test]
        public void DataParsedIntoPyramid_FindNumericalRoute_PyramidRuleAppliedCalledWithThePyramid()
        {
            //Arrange
            const string path = "Some path";
            var stringData = new[] {"blah", "blah blah"};

            var pyramid = new Pyramid<int>(754);

            var extractor = new Mock<IFileDataExtractor>();
            extractor.Setup(m => m.ExtractLines(path)).Returns(stringData);

            var dataParser = new Mock<IDataParser<int>>();
            dataParser.Setup(m => m.ParseIntoPyramid(stringData)).Returns(new Option<Pyramid<int>>(pyramid));

            var ruleApplier = new Mock<IPyramidRuleApplier<int>>();
            ruleApplier.Setup(m => m.TransformPyramid(pyramid)).Returns(new Pyramid<int>(1));
            var longestRouteFinder = new Mock<ILongestRouteFinder<int>>();

            var routeFinder = new RouteFinder(
                extractor.Object,
                dataParser.Object,
                ruleApplier.Object,
                longestRouteFinder.Object
            );

            //Act
            routeFinder.FindNumericalRouteFromFile(path);

            //Assert 
            ruleApplier.Verify(m => m.TransformPyramid(pyramid), Times.Once);
            ruleApplier.VerifyNoOtherCalls();
        }

        [Test]
        public void NoPyramidReturnedFromDataParser_FindNumericalRoute_EmptyRouteReturned()
        {
            //Arrange
            const string path = "Some path";
            var stringData = new[] {"blah", "blah blah"};

            var none = Option<Pyramid<int>>.None;

            var extractor = new Mock<IFileDataExtractor>();
            extractor.Setup(m => m.ExtractLines(path)).Returns(stringData);

            var dataParser = new Mock<IDataParser<int>>();
            dataParser.Setup(m => m.ParseIntoPyramid(stringData)).Returns(none);

            var ruleApplier = new Mock<IPyramidRuleApplier<int>>();
            var longestRouteFinder = new Mock<ILongestRouteFinder<int>>();

            var routeFinder = new RouteFinder(
                extractor.Object,
                dataParser.Object,
                ruleApplier.Object,
                longestRouteFinder.Object
            );

            //Act
            var result = routeFinder.FindNumericalRouteFromFile(path);

            //Assert 
            Assert.NotNull(result);
            TestHelper.AssertListContentSame(new List<int>(), result.Steps);
            ruleApplier.VerifyNoOtherCalls();
            longestRouteFinder.VerifyNoOtherCalls();
        }

        [Test]
        public void PyramidTransformedToMatchTheRules_FindNumericalRoute_PyramidPassedToLongestRouteFinder()
        {
            //Arrange
            const string path = "Some path";
            var stringData = new[] {"blah", "blah blah"};

            var pyramid = new Pyramid<int>(754);
            var transformedPyramid = new Pyramid<int>(42);

            var extractor = new Mock<IFileDataExtractor>();
            extractor.Setup(m => m.ExtractLines(path)).Returns(stringData);

            var dataParser = new Mock<IDataParser<int>>();
            dataParser.Setup(m => m.ParseIntoPyramid(stringData)).Returns(new Option<Pyramid<int>>(pyramid));

            var ruleApplier = new Mock<IPyramidRuleApplier<int>>();
            ruleApplier.Setup(m => m.TransformPyramid(pyramid)).Returns(transformedPyramid);

            var longestRouteFinder = new Mock<ILongestRouteFinder<int>>();

            var routeFinder = new RouteFinder(
                extractor.Object,
                dataParser.Object,
                ruleApplier.Object,
                longestRouteFinder.Object
            );

            //Act
            routeFinder.FindNumericalRouteFromFile(path);

            //Assert 
            longestRouteFinder.Verify(m => m.FindLongestRoute(transformedPyramid), Times.Once);
            longestRouteFinder.VerifyNoOtherCalls();
        }

        [Test]
        public void LongestRouteFound_FindNumericalRoute_RouteReturned()
        {
            //Arrange
            const string path = "Some path";
            var stringData = new[] {"blah", "blah blah"};

            var pyramid = new Pyramid<int>(754);
            var transformedPyramid = new Pyramid<int>(42);
            var route = new Route<int>(new List<int> {100, 200, 54});

            var extractor = new Mock<IFileDataExtractor>();
            extractor.Setup(m => m.ExtractLines(path)).Returns(stringData);

            var dataParser = new Mock<IDataParser<int>>();
            dataParser.Setup(m => m.ParseIntoPyramid(stringData)).Returns(new Option<Pyramid<int>>(pyramid));

            var ruleApplier = new Mock<IPyramidRuleApplier<int>>();
            ruleApplier.Setup(m => m.TransformPyramid(pyramid)).Returns(transformedPyramid);

            var longestRouteFinder = new Mock<ILongestRouteFinder<int>>();
            longestRouteFinder.Setup(m => m.FindLongestRoute(transformedPyramid)).Returns(route);

            var routeFinder = new RouteFinder(
                extractor.Object,
                dataParser.Object,
                ruleApplier.Object,
                longestRouteFinder.Object
            );

            //Act
            var result = routeFinder.FindNumericalRouteFromFile(path);

            //Assert 
            Assert.AreSame(route, result);
        }
    }
}