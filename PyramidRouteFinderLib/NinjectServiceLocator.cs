using Ninject;

namespace PyramidRouteFinderLib
{
    internal class NinjectServiceLocator : IServiceLocator
    {
        private readonly IKernel kernel;

        internal NinjectServiceLocator()
        {
            kernel = new StandardKernel();
            ApplyBindings();
        }

        private void ApplyBindings()
        {
            kernel.Bind<IDataParser<int>>().To<NumericalDataParser>();
            kernel.Bind<IFileDataExtractor>().To<FileDataExtractor>();
            kernel.Bind(typeof(IPyramidConstructor<>)).To(typeof(PyramidConstructor<>));
        }

        public T Get<T>() => kernel.Get<T>();
    }
}