using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamebase
{
    public static class IEnumerableExtensions
    {
        public static TSource FindMin<TSource, TResult>(
            this IEnumerable<TSource> self, 
            Func<TSource, TResult>    selector)
        {
            return self.First(c => selector(c).Equals(self.Min(selector)));
        }
    
        public static TSource FindMax<TSource, TResult>(
            this IEnumerable<TSource> self,
            Func<TSource, TResult>    selector)
        {
            return self.First(c => selector(c).Equals(self.Max(selector)));
        }
    }
}