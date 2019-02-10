using JetBrains.Annotations;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib.Algo
{
    internal class PyramidRuleApplier<T> : IPyramidRuleApplier<T>
    {
        [NotNull] private readonly IPyramidPathRuleChecker<int> _ruleChecker;

        public PyramidRuleApplier([NotNull] IPyramidPathRuleChecker<int> ruleChecker)
        {
            _ruleChecker = ruleChecker;
        }

        public Pyramid<T> TransformPyramid(Pyramid<T> pyramid)
        {
            return null;
        }
    }
}