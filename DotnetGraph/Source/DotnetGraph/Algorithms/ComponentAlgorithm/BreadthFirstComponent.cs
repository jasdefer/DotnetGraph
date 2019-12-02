using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.ComponentAlgorithm
{
    public class BreadthFirstComponent : IComponentAlgorithm
    {
        public T[][] GetComponents<T>(IEnumerable<Edge<T>> edges)
        {
            var connections = CreateGraph(edges);
            if (connections.Count < 1)
            {
                return Array.Empty<T[]>();
            }
            var components = GetComponents(connections);
            return components.ToArray();
        }

        private List<T[]> GetComponents<T>(Dictionary<T, Edge<T>[]> connections)
        {
            var components = new List<T[]>();
            var nodes = connections.Keys.ToList();
            while (nodes.Count > 0)
            {
                var component = GetComponent(nodes[0], connections);
                foreach (var node in component)
                {
                    nodes.Remove(node);
                }
                components.Add(component);
            }
            return components;
        }

        private T[] GetComponent<T>(T origin, Dictionary<T, Edge<T>[]> edges)
        {
            var queue = new Queue<T>();
            queue.Enqueue(origin);
            var components = new HashSet<T>();
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                components.Add(node);
                foreach (var edge in edges[node])
                {
                    if (!components.Contains(edge.Node1))
                    {
                        queue.Enqueue(edge.Node1);
                        components.Add(edge.Node1);
                    }
                    if (!components.Contains(edge.Node2))
                    {
                        queue.Enqueue(edge.Node2);
                        components.Add(edge.Node2);
                    }
                }
            }
            return components.ToArray();
        }


        private Dictionary<T, Edge<T>[]> CreateGraph<T>(IEnumerable<Edge<T>> edges)
        {
            var dict = new Dictionary<T, Edge<T>[]>();
            if(edges is null)
            {
                return dict;
            }

            var connections = new Dictionary<T, List<Edge<T>>>();
            foreach (var edge in edges)
            {
                AddIfNew(edge.Node1, connections);
                AddIfNew(edge.Node2, connections);
                connections[edge.Node1].Add(edge);
                connections[edge.Node2].Add(edge);
            }
            foreach (var item in connections)
            {
                dict.Add(item.Key, item.Value.ToArray());
            }
            return dict;
        }

        private void AddIfNew<T>(T node, Dictionary<T, List<Edge<T>>> dictionary)
        {
            if (!dictionary.ContainsKey(node))
            {
                dictionary.Add(node, new List<Edge<T>>());
            }
        }
    }
}