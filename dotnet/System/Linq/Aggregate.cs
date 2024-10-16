// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

#pragma warning disable CS8600

namespace System.Linq
{
    public static partial class Enumerable
    {
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (func == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.func);
            }

            using (IEnumerator<TSource> e = source.GetEnumerator())
            {
                if (!e.MoveNext())
                {
                    ThrowHelper.ThrowNoElementsException();
                }

                TSource result = e.Current;
                while (e.MoveNext())
                {
                    result = func(result, e.Current);
                }

                return result;
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (func == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.func);
            }

            TAccumulate result = seed;
            foreach (TSource element in source)
            {
                result = func(result, element);
            }

            return result;
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (func == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.func);
            }

            if (resultSelector == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.resultSelector);
            }

            TAccumulate result = seed;
            foreach (TSource element in source)
            {
                result = func(result, element);
            }

            return resultSelector(result);
        }
    }
}
