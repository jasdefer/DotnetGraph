using DotnetGraph.Model;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Helper
{
    public static class ArcExtensions
    {
        public static IEnumerable<T> ExtractNodes<T>(this IEnumerable<Arc<T>> arcs)
        {
            if (arcs is null)
            {
                throw new ArgumentNullException(nameof(arcs));
            }

            var nodes = new HashSet<T>();
            foreach (var arc in arcs)
            {
                nodes.Add(arc.Origin);
                nodes.Add(arc.Destination);
            }
            return nodes;
        }

        public static double TotalWeight<T>(this IEnumerable<Arc<T>> arcs)
        {
            var weight = 0d;
            foreach (var arc in arcs)
            {
                weight += arc.Weight;
            }
            return weight;
        }
    }
}