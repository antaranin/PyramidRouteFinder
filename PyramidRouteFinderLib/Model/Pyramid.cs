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

        public Pyramid([NotNull] Pyramid<T> left, T value, [NotNull] Pyramid<T> right)
        {
            Value = value;
            Left = new Option<Pyramid<T>>(left);
            Right = new Option<Pyramid<T>>(right);
        }

        public Pyramid([NotNull]Pyramid<T> left, T value)
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
    }
}