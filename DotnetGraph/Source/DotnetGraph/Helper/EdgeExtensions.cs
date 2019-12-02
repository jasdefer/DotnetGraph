using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DotnetGraph.Helper
{
    public static class EdgeExtensions
    {
        public static int CountNodes<T>(this IEnumerable<Edge<T>> edges)
        {
            if (edges is null)
            {
                return 0; 
            }
            var nodes = new HashSet<T>();
            foreach (var edge in edges)
            {
                nodes.Add(edge.Node1);
                nodes.Add(edge.Node2);
            }
            return nodes.Count;
        }

        public static T[] ExtractNodes<T>(this IEnumerable<Edge<T>> edges)
        {
            if (edges is null)
            {
                return Array.Empty<T>();
            }
            var nodes = new HashSet<T>();
            foreach (var edge in edges)
            {
                nodes.Add(edge.Node1);
                nodes.Add(edge.Node2);
            }
            return nodes.ToArray();
        }

        public static double TotalWeight<T>(this IEnumerable<Edge<T>> edges)
        {
            if(edges is null)
            {
                return 0;
            }
            var weight = 0d;
            foreach (var edge in edges)
            {
                weight += edge.Weight;
            }
            return weight;
        }
        public static string Print<T>(this IEnumerable<Edge<T>> edges)
        {
            return edges.Print(CultureInfo.InvariantCulture);
        }

        public static string Print<T>(this IEnumerable<Edge<T>> edges, IFormatProvider provider)
        {
            if(edges is null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            var format = "{0}\t{1}\t{2}";
            sb.AppendLine(string.Format(provider: provider, format: format, 
                "Node 1", 
                "Node 2", 
                "Weight"));
            foreach (var edge in edges)
            {
                var line = string.Format(provider: provider, format: format, 
                    edge.Node1.ToString(), 
                    edge.Node2.ToString(), 
                    edge.Weight);
                sb.AppendLine(line);
            }
            return sb.ToString();
        }
    }
}