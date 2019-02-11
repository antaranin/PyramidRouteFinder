using System;
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

        public TR Fold<TR>([NotNull] Func<T, TR> hasValueOp, [NotNull] Func<TR> noValueOp)
        {
            return _hasValue ? hasValueOp(_value) : noValueOp();
        }

        public Option<TR> Map<TR>(Func<T, TR> hasValueOp)
        {
            return _hasValue ? new Option<TR>(hasValueOp(_value)) : Option<TR>.None;
        }

        public Option<TR> FlatMap<TR>(Func<T, Option<TR>> hasValueOp)
        {
            return _hasValue ? hasValueOp(_value) : Option<TR>.None;
        }

        /// <summary>
        /// Zips two options.
        /// If both have value, an operation is applied to zip them together.
        /// If only one has value then that value is used.
        /// If none have value then None is returned.
        /// </summary>
        /// <param name="other">The other option used for zipping</param>
        /// <param name="caseBothOp">
        /// Operation used to zip two option values into one.
        /// Only applied if both options hold a value
        /// </param>
        /// <returns>Option<T> if at least one of the options is not None, None otherwise</returns>
        public Option<T> Zip(Option<T> other, Func<T, T, T> caseBothOp)
        {
            return Fold(thisVal =>
                    new Option<T>(other.Fold(otherVal => caseBothOp(thisVal, otherVal), () => thisVal)),
                () => other.Fold(otherVal => new Option<T>(otherVal), () => None)
            );
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