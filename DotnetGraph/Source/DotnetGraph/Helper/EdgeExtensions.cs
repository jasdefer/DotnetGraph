using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}