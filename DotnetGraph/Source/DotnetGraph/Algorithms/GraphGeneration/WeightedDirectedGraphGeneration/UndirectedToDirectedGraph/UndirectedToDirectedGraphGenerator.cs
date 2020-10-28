using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.LineGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.UndirectedToDirectedGraph
{
    public class UndirectedToDirectedGraphGenerator : IWeightedDirectedGraphGenerator
    {
        public IWeightedUndirectedGraphGenerator WeightedUndirectedGraphGenerator { get; set; } = new LineGraphGenerator();

        public UndirectedToDirectedGraphGenerator()
        {
            WeightedUndirectedGraphGenerator = new LineGraphGenerator();
        }

        public WeightedDirectedGraphNode[] Generate(int numberOfNodes, double density, IWeightGenerator weightGenerator)
        {
            var undirectedNodes = WeightedUndirectedGraphGenerator.Generate(numberOfNodes, density, weightGenerator);
            var nodes = Convert(undirectedNodes);
            return nodes;
        }

        public static WeightedDirectedGraphNode[] Convert(WeightedUndirectedGraphNode[] undirectedNodes)
        {
            if (undirectedNodes is null)
            {
                throw new ArgumentNullException(nameof(undirectedNodes));
            }

            var nodes = new WeightedDirectedGraphNode[undirectedNodes.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new WeightedDirectedGraphNode(undirectedNodes[i].Id);
            }
            var dict = nodes.ToDictionary(x => x.Id, x => x);
            var edgesIds = new HashSet<int>();
            var arcId = 1;
            for (int i = 0; i < undirectedNodes.Length; i++)
            {
                foreach (var edge in undirectedNodes[i].Edges)
                {
                    if (!edgesIds.Contains(edge.Id))
                    {
                        edgesIds.Add(edge.Id);
                        AddArcInBothDirections(arcId, dict[edge.Node1.Id], dict[edge.Node2.Id], edge.Weight);
                        arcId += 2;
                    }
                }
            }

            return nodes;
        }

        private static void AddArcInBothDirections(int id, WeightedDirectedGraphNode node1, WeightedDirectedGraphNode node2, double weight)
        {
            var arc = new WeightedDirectedGraphArc(id, node2, weight);
            node1.AddArc(arc);
            arc = new WeightedDirectedGraphArc(id + 1, node1, weight);
            node2.AddArc(arc);
        }
    }
}