using System.Collections.Generic;
using JetBrains.Annotations;

namespace PyramidRouteFinderLib.Model
{
    //Solve also empty Pyramid 
    internal class Pyramid<T>
    {
        public T Value { get; }

        [NotNull]
        public Option<Pyramid<T>> Left { get; }

        [NotNull]
        public Option<Pyramid<T>> Right { get; }

        public Pyramid([NotNull] Option<Pyramid<T>> left, T value, [NotNull] Option<Pyramid<T>> right)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        public Pyramid([NotNull] Pyramid<T> left, T value, [NotNull] Pyramid<T> right)
        {
            Value = value;
            Left = new Option<Pyramid<T>>(left);
            Right = new Option<Pyramid<T>>(right);
        }

        public Pyramid([NotNull] Pyramid<T> left, T value)
        {
            Value = value;
            Left = new Option<Pyramid<T>>(left);
            Right = Option<Pyramid<T>>.None;
        }

        public Pyramid(T value, [NotNull] Pyramid<T> right)
        {
            Value = value;
            Left = Option<Pyramid<T>>.None;
            Right = new Option<Pyramid<T>>(right);
        }

        public Pyramid(T value)
        {
            Value = value;
            Left = Option<Pyramid<T>>.None;
            Right = Option<Pyramid<T>>.None;
        }

        protected bool Equals(Pyramid<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value) && Left.Equals(other.Left) &&
                   Right.Equals(other.Right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Pyramid<T>) obj);
        }

        public override string ToString()
        {
            return $"{nameof(Value)}: {Value}, {nameof(Left)}: {Left}, {nameof(Right)}: {Right}";
        }
    }
}