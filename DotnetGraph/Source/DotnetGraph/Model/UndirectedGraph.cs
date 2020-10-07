using DotnetGraph.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DotnetGraph.Model
{
    public class UndirectedGraph<T>
    {
        private ReadOnlyDictionary<T, ReadOnlyCollection<Edge<T>>> EdgesPerNode;
        private readonly Dictionary<T, ReadOnlyCollection<T>> ConnectedNodes;
        private ReadOnlyCollection<T> nodes;

        public ReadOnlyCollection<T> Nodes
        {
            get 
            { 
                if(nodes == null)
                {
                    nodes = Edges
                        .ExtractNodes()
                        .ToList()
                        .AsReadOnly();
                }
                return nodes; 
            }
        }

        public ReadOnlyCollection<Edge<T>> Edges { get; }

        public UndirectedGraph(IEnumerable<Edge<T>> edges)
        {
            edges ??= Array.Empty<Edge<T>>();
            Edges = edges.ToList().AsReadOnly();
        }

        public ReadOnlyCollection<Edge<T>> GetConnections(T node)
        {
            if(EdgesPerNode == null)
            {
                EdgesPerNode = Edges.GetEdgesPerNode();
            }
            return EdgesPerNode[node];
        }

        public ReadOnlyCollection<T> GetConnectedNodes(T node)
        {
            if (!ConnectedNodes.ContainsKey(node))
            {
                ConnectedNodes[node] = EdgesPerNode[node].ExtractNodes().ToList().AsReadOnly();
            }
            return ConnectedNodes[node];
        }
    }
}