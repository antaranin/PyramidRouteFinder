namespace PyramidRouteFinderLib.Algo
{
    internal class PyramidNumericalPathRuleChecker : IPyramidPathRuleChecker<int>
    {
        public bool IsValidPath(int currentElement, int nextElement)
        {
            return (currentElement + nextElement) % 2 != 0;
        }
    }
}