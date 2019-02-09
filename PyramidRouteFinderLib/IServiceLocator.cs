namespace PyramidRouteFinderLib
{
    internal interface IServiceLocator
    {
        T Get<T>();
    }
}