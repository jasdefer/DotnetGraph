using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Model;
using DotnetGraph.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.MinimumSpanningTree
{
    public class Kruskal : IMinimumSpanningTreeAlgorithm
    {
        public Edge<T>[] GetMinimumSpanningTree<T>(IEnumerable<Edge<T>> edges)
        {
            if(edges is null)
            {
                return Array.Empty<Edge<T>>();
            }
            var orderedEdges = edges.OrderBy(x => x.Weight).ToList();
            var nodes = orderedEdges.ExtractNodes().ToDictionary();
            var parents = Enumerable.Range(0, nodes.Count).ToArray();
            var tree = new List<Edge<T>>();
            foreach (var edge in orderedEdges)
            {
                var startNodeRoot = FindRoot(nodes[edge.Node1], parents);
                var endNodeRoot = FindRoot(nodes[edge.Node2], parents);

                if (startNodeRoot != endNodeRoot)
                {
                    // Add edge to the spanning tree
                    tree.Add(edge);

                    // Mark one root as parent of the other
                    parents[endNodeRoot] = startNodeRoot;
                }
            }
            return tree.ToArray();
        }

        private static int FindRoot(int node, int[] parent)
        {
            var root = node;
            while (root != parent[root])
            {
                root = parent[root];
            }

            while (node != root)
            {
                var oldParent = parent[node];
                parent[node] = root;
                node = oldParent;
            }

            return root;
        }
    }
}