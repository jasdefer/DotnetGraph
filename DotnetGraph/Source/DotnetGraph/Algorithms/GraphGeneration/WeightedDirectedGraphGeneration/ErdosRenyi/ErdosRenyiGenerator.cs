using DotnetGraph.Algorithms.Components.StronglyConnectedComponents.Tarjan;
using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using System;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.ErdosRenyi
{
    public class ErdosRenyiGenerator : IWeightedDirectGraphGenerator
    {
        public Random Random { get; set; } = new Random(1);
        public bool ConnectComponents { get; set; } = true;
        public WeightedDirectedGraphNode[] Generate(int numberOfNodes, double density, IWeightGenerator weightGenerator)
        {
            ValidateInput(numberOfNodes, density, weightGenerator);
            var nodes = CreateNodes(numberOfNodes);
            var arcId = 1;
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                for (int j = i + 1; j < nodes.Length; j++)
                {
                    var createArc = Random.NextDouble() < density;
                    if (createArc)
                    {
                        var weight = weightGenerator.Generate();
                        AddArcInBothDirections(arcId, nodes[i], nodes[j], weight);
                        arcId += 2;
                    }
                }
            }

            //Connect all components of the graph so that every node is connected to every other node
            if (ConnectComponents)
            {
                var componentAlgorithm = new TarjanAlgorithm();
                var componentResult = componentAlgorithm.GetCompontents<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes);
                for (int i = 0; i < componentResult.Components.Count - 1; i++)
                {
                    var nodeId = Random.Next(0, componentResult.Components[i].Count);
                    var node1 = componentResult.Components[i][nodeId];
                    nodeId = Random.Next(0, componentResult.Components[i + 1].Count);
                    var node2 = componentResult.Components[i + 1][nodeId];
                    var weight = weightGenerator.Generate();
                    AddArcInBothDirections(arcId, node1, node2, weight);
                    arcId += 2;
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

        public static WeightedDirectedGraphNode[] CreateNodes(int numberOfNodes)
        {
            var nodes = new WeightedDirectedGraphNode[numberOfNodes];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new WeightedDirectedGraphNode(i + 1);
            }
            return nodes;
        }

        public static void ValidateInput(int numberOfNodes, double density, IWeightGenerator weightGenerator)
        {
            if (numberOfNodes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfNodes));
            }

            if (density < 0 || density > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfNodes));
            }

            if (weightGenerator == null)
            {
                throw new ArgumentNullException(nameof(weightGenerator));
            }
        }
    }
}