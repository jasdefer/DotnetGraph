using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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

        public static Dictionary<T, Arc<T>> GetOutgoingArcsPerNode<T>(IList<Arc<T>> arcs)
        {
            var dictionary = arcs.ExtractNodes().ToDictionary(x => x,x => new List<Arc<T>>());
            foreach (var arc in arcs)
            {
                dictionary[arc.Origin].Add(arc);
            }

        }

        public static double TotalWeight<T>(this IEnumerable<Arc<T>> arcs)
        {
            if (arcs is null)
            {
                return 0;
            }
            var weight = 0d;
            foreach (var arc in arcs)
            {
                weight += arc.Weight;
            }
            return weight;
        }

        public static string Print<T>(this IEnumerable<Arc<T>> arcs)
        {
            return arcs.Print(CultureInfo.InvariantCulture);
        }

        public static string Print<T>(this IEnumerable<Arc<T>> arcs, IFormatProvider provider)
        {
            if (arcs is null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            var format = "{0}\t{1}\t{2}";
            sb.AppendLine(string.Format(provider: provider, format: format,
                "Origin",
                "Destination",
                "Weight"));
            foreach (var arc in arcs)
            {
                var line = string.Format(provider: provider, format: format,
                    arc.Origin.ToString(),
                    arc.Destination.ToString(),
                    arc.Weight);
                sb.AppendLine(line);
            }
            return sb.ToString();
        }
    }
}