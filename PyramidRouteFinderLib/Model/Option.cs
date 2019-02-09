using System.Collections.Generic;
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

        protected bool Equals(Option<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value) && _hasValue == other._hasValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Option<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ _hasValue.GetHashCode();
            }
        }

        public override string ToString()
        {
            return _hasValue ? $"Some({_value})" : "None";
        }
    }
}