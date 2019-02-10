using System;
using JetBrains.Annotations;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib.Algo
{
    internal class PyramidRuleApplier<T> : IPyramidRuleApplier<T>
    {
        [NotNull] private readonly IPyramidPathRuleChecker<T> _ruleChecker;

        public PyramidRuleApplier([NotNull] IPyramidPathRuleChecker<T> ruleChecker)
        {
            _ruleChecker = ruleChecker;
        }

        public Pyramid<T> TransformPyramid(Pyramid<T> pyramid)
        {
            Option<Pyramid<T>> FlatMapOp(Pyramid<T> p) => _ruleChecker.IsValidPath(pyramid.Value, p.Value)
                ? new Option<Pyramid<T>>(TransformPyramid(p))
                : Option<Pyramid<T>>.None;

            var newLeft = pyramid.Left.FlatMap(FlatMapOp);
            var newRight = pyramid.Right.FlatMap(FlatMapOp);
            return new Pyramid<T>(newLeft, pyramid.Value, newRight);
        }
    }
}