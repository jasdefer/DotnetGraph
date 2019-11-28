using DotnetGraph.Model;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Helper
{
    public static class CollectionExtensions
    {
        public static Dictionary<T, int> ToDictionary<T>(this IEnumerable<T> items)
        {
            if(items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var dict = new Dictionary<T, int>();
            var counter = 0;
            foreach (var item in items)
            {
                dict.Add(item, counter++);
            }
            return dict;
        }

        
    }
}