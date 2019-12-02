using DotnetGraph.Algorithms.ComponentAlgorithm;
using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Algorithms.Contracts.GraphGeneration;
using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration
{
    public class ErdosRenyi : IUndirectedGraphGeneration
    {
        private double p;

        public double P
        {
            get { return p; }
            set 
            { 
                if(value < 0|| value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                p = value; 
            }
        }

        private int nodeCount;

        public int NodeCount
        {
            get { return nodeCount; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                nodeCount = value; 
            }
        }
        public Random Random { get; set; } = new Random(1);
        public IWeightGenerator WeightGenerator { get; set; } = new UniformWeightGenerator();
        public IComponentAlgorithm ComponentAlgorithm { get; set; } = new BreadthFirstComponent();

        public Edge<T>[] GenerateGraph<T>(IEnumerable<T> nodes)
        {
            var edges = CreateEdges(nodes);
            ConnectComponents(edges);
            return edges.ToArray();
        }

        private List<Edge<T>> CreateEdges<T>(IEnumerable<T> nodes)
        {
            var nodeArray = nodes.ToArray();
            var edges = new List<Edge<T>>();
            var connectionCount = new int[nodeArray.Length];
            for (int i = 0; i < nodeArray.Length; i++)
            {
                for (int j = 0; j < nodeArray.Length; j++)
                {
                    if (i != j && Random.NextDouble() < P)
                    {
                        var weight = WeightGenerator.Create();
                        var edge = new Edge<T>(nodeArray[i], nodeArray[j], weight);
                        edges.Add(edge);
                        connectionCount[i]++;
                        connectionCount[j]++;
                    }
                }
            }

            for (int i = 0; i < connectionCount.Length; i++)
            {
                if (connectionCount[i] == 0)
                {
                    var weight = WeightGenerator.Create();
                    var candidates = Enumerable.Range(0, nodeArray.Length).ToList();
                    candidates.Remove(i);
                    var node2 = candidates[Random.Next(0, candidates.Count)];
                    var edge = new Edge<T>(nodeArray[i], nodeArray[node2], weight);
                    edges.Add(edge);
                }
            }

            return edges;
        }

        private void ConnectComponents<T>(List<Edge<T>> edges)
        {
            var components = ComponentAlgorithm.GetComponents<T>(edges);
            for (int i = 1; i < components.Length; i++)
            {
                var node1 = components[i - 1][0];
                var node2 = components[i][0];
                var weight = WeightGenerator.Create();
                var edge = new Edge<T>(node1, node2, weight);
                edges.Add(edge);
            }
        }
    }
}