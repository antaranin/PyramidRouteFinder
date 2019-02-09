using JetBrains.Annotations;

namespace PyramidRouteFinderLib.Model
{
    internal class Option<T>
    {
        [CanBeNull] private T _value;

        private bool _hasValue;
        public static Option<T> None { get; } = new Option<T>();

        public Option([CanBeNull] T value)
        {
            _value = value;
            _hasValue = true;
        }

        private Option()
        {
            _hasValue = false;
        }
    }
}