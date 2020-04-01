using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbits
{
    public class Memoizer<TIn, TOut>
    {
        public Memoizer(Func<TIn, TOut> func)
        {
            _func = func;
        }

        private readonly Func<TIn, TOut> _func;
        private readonly Dictionary<TIn, TOut> _cache = new Dictionary<TIn, TOut>();

        public TOut Get(TIn input)
        {
            if (_cache.ContainsKey(input))
            {
                return _cache[input];
            }

            var result = _func(input);
            _cache.Add(input, result);
            return result;
        }
    }

    public static class Memoizer
    {
        public static Func<TIn, TOut> Memoize<TIn, TOut>(Func<TIn, TOut> func) =>
            new Memoizer<TIn, TOut>(func).Get;
    }
}
