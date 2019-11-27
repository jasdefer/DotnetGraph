using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Helper
{
    public static class Extensions
    {
        public static IEnumerable<T> ExtractNodes<T>(this IEnumerable<Arc<T>> arcs)
        {
            var nodes = new HashSet<T>();
            foreach (var arc in arcs)
            {
                nodes.Add(arc.Origin);
                nodes.Add(arc.Destination);
            }
            return nodes;
        }

        public static Dictionary<int, T> ToDictionary<T>(this IEnumerable<T> items)
        {
            var dict = new Dictionary<int, T>();
            var counter = 0;
            foreach (var item in items)
            {
                dict.Add(counter++, item);
            }
            return dict;
        }
    }
}