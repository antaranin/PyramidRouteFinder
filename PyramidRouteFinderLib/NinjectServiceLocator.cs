using Ninject;
using PyramidRouteFinderLib.Algo;

namespace PyramidRouteFinderLib
{
    internal class NinjectServiceLocator : IServiceLocator
    {
        private readonly IKernel _kernel;

        internal NinjectServiceLocator()
        {
            _kernel = new StandardKernel();
            ApplyBindings();
        }

        private void ApplyBindings()
        {
            _kernel.Bind<IDataParser<int>>().To<NumericalDataParser>();
            _kernel.Bind<IFileDataExtractor>().To<FileDataExtractor>();
            _kernel.Bind(typeof(IPyramidRuleApplier<>)).To(typeof(PyramidRuleApplier<>));
            _kernel.Bind(typeof(IPyramidConstructor<>)).To(typeof(PyramidConstructor<>));
            _kernel.Bind<ILongestRouteFinder<int>>().To<LongestNumericalRouteFinder>();
            _kernel.Bind<IPyramidPathRuleChecker<int>>().To<PyramidNumericalPathRuleChecker>();
        }

        public T Get<T>() => _kernel.Get<T>();
    }
}