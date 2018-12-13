using System;

namespace SearchAThing.Helpers
{
    // thank you to https://stackoverflow.com/a/3199453/1586229
    public abstract class Either<L, R>
    {
        public abstract T Match<T>(Func<L, T> f, Func<R, T> g);
        public abstract void Match(Action<L> f, Action<R> g);

        public abstract Either<T, R> Map<T>(Func<L, T> f);

        // private ctor ensures no external classes can inherit
        // (not sure why that's important, but it's in the SO answer)
        private Either() { }
        public sealed class Left : Either<L, R>
        {
            public readonly L Item;
            public Left(L item) : base() { this.Item = item; }
            public override T Match<T>(Func<L, T> f, Func<R, T> g)
            {
                return f(Item);
            }
            public override void Match(Action<L> f, Action<R> g)
            {
                f(Item);
            }
            public override Either<T, R> Map<T>(Func<L, T> f)
            {
                return f(Item);
            }
        }
        public static implicit operator Either<L, R>(L ell)
        {
            return new Left(ell);
        }

        public sealed class Right : Either<L, R>
        {
            public readonly R Item;
            public Right(R item) { this.Item = item; }
            public override T Match<T>(Func<L, T> f, Func<R, T> g)
            {
                return g(Item);
            }
            public override void Match(Action<L> f, Action<R> g)
            {
                g(Item);
            }
            public override Either<T, R> Map<T>(Func<L, T> f)
            {
                return Item;
            }


        }
        public static implicit operator Either<L, R>(R arr)
        {
            return new Right(arr);
        }
    }

}