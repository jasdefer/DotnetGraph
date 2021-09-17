using DotnetGraph.Algorithms.Components.ConnectedComponents;
using DotnetGraph.Algorithms.Components.ConnectedComponents.SimpleConnectedComponent;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;
using System;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.ErdosRenyi
{
    public class ErdosRenyiGenerator : IWeightedUndirectedGraphGenerator
    {
        public Random Random { get; set; } = new Random(1);
        public bool ConnectComponents { get; set; } = true;
        public IConnectedComponentsAlgorithm ComponentsAlgorithm { get; set; } = new SimpleConnectedComponentAlgorithm();

        public WeightedUndirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator weightGenerator)
        {
            ValidateInput(numberOfNodes, density, weightGenerator);
            var nodes = GenerateNodes(numberOfNodes);
            var numberOfEdges = CreateUniformLikelyEdges(nodes, density, weightGenerator);
            if (ConnectComponents)
            {
                ConnectAllComponents(nodes, weightGenerator, numberOfEdges);
            }
            return nodes;
        }

        public void ConnectAllComponents(WeightedUndirectedGraphNode[] nodes, INumberGenerator weightGenerator, int numberOfEdges)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            if (weightGenerator is null)
            {
                throw new ArgumentNullException(nameof(weightGenerator));
            }

            var componentResult = ComponentsAlgorithm.GetComponents<WeightedUndirectedGraphNode, WeightedUndirectedGraphEdge>(nodes);
            for (int i = 0; i < componentResult.Components.Count - 1; i++)
            {
                var weight = weightGenerator.Generate();
                var node1 = componentResult.Components[i].Nodes[0];
                var node2 = componentResult.Components[i + 1].Nodes[0];
                var edge = new WeightedUndirectedGraphEdge(++numberOfEdges, weight, node1, node2);
                node1.Add(edge);
                node2.Add(edge);
            }
        }

        public int CreateUniformLikelyEdges(WeightedUndirectedGraphNode[] nodes, double density, INumberGenerator weightGenerator)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            if (weightGenerator is null)
            {
                throw new ArgumentNullException(nameof(weightGenerator));
            }

            var edgeId = 0;
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = i + 1; j < nodes.Length; j++)
                {
                    if (Random.NextDouble() < density)
                    {
                        var weight = weightGenerator.Generate();
                        var edge = new WeightedUndirectedGraphEdge(++edgeId, weight, nodes[i], nodes[j]);
                        nodes[i].Add(edge);
                        nodes[j].Add(edge);
                    }
                }
            }
            return edgeId;
        }

        public static WeightedUndirectedGraphNode[] GenerateNodes(int numberOfNodes)
        {
            var nodes = new WeightedUndirectedGraphNode[numberOfNodes];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new WeightedUndirectedGraphNode(i + 1);
            }
            return nodes;
        }

        public static void ValidateInput(int numberOfNodes, double density, INumberGenerator weightGenerator)
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