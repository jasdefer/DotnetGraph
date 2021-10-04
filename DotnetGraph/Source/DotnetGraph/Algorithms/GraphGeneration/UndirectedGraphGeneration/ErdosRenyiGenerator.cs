using DotnetGraph.Algorithms.Components.ConnectedComponents;
using DotnetGraph.Algorithms.Components.ConnectedComponents.SimpleConnectedComponent;
using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.UndirectedGraph;
using System;

namespace DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration
{
    public class ErdosRenyiGenerator : IUndirectedGraphGenerator
    {
        public Random Random { get; set; } = new Random(1);
        public bool ConnectComponents { get; set; } = true;
        public IConnectedComponentsAlgorithm ComponentsAlgorithm { get; set; } = new SimpleConnectedComponentAlgorithm();

        public UndirectedGraphNode[] Generate(int numberOfNodes, double density)
        {
            ValidateInput(numberOfNodes, density);
            var nodes = GenerateNodes(numberOfNodes);
            var numberOfEdges = CreateUniformLikelyEdges(nodes, density);
            if (ConnectComponents)
            {
                // The density will increase to connect all components
                ConnectAllComponents(nodes, numberOfEdges);
            }
            return nodes;
        }

        public void ConnectAllComponents(UndirectedGraphNode[] nodes, int numberOfEdges)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var componentResult = ComponentsAlgorithm.GetComponents<UndirectedGraphNode, UndirectedGraphEdge>(nodes);
            for (int i = 0; i < componentResult.Components.Count - 1; i++)
            {
                var node1 = componentResult.Components[i].Nodes[0];
                var node2 = componentResult.Components[i + 1].Nodes[0];
                var edge = new UndirectedGraphEdge(++numberOfEdges, node1, node2);
                node1.Add(edge);
                node2.Add(edge);
            }
        }

        public int CreateUniformLikelyEdges(UndirectedGraphNode[] nodes, double density)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var edgeId = 0;
            var numberOfExpectedArcs = GraphPropertyHelper.NumberOfPossibleEdges(nodes.Length) * density;
            while (edgeId < numberOfExpectedArcs)
            {
                var index1 = Random.Next(0, nodes.Length);
                var index2 = Random.Next(0, nodes.Length);
                if (index1 == index2)
                {
                    if (index2 == nodes.Length - 1)
                    {
                        index2 = Random.Next(0, nodes.Length - 1);
                    }
                    else
                    {
                        index2 = Random.Next(index2, nodes.Length);
                    }
                }
                var edge = new UndirectedGraphEdge(++edgeId, nodes[index1], nodes[index2]);
                nodes[index1].Add(edge);
                nodes[index2].Add(edge);
            }
            return edgeId;
        }

        public static UndirectedGraphNode[] GenerateNodes(int numberOfNodes)
        {
            var nodes = new UndirectedGraphNode[numberOfNodes];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new UndirectedGraphNode(i + 1);
            }
            return nodes;
        }

        public static void ValidateInput(int numberOfNodes, double density)
        {
            if (numberOfNodes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfNodes));
            }

            if (density < 0 || density > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfNodes));
            }
        }
    }
}