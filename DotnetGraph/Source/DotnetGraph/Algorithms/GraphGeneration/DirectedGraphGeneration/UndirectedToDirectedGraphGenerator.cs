using DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.DirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.DirectedGraphGeneration
{
    /// <summary>
    /// Creates two antiparallel arcs for each edge
    /// </summary>
    public class UndirectedToDirectedGraphGenerator : IDirectedGraphGenerator
    {
        public IUndirectedGraphGenerator UndirectedGraphGenerator { get; set; } = new ErdosRenyiGenerator();

        public DirectedGraphNode[] Generate(int numberOfNodes, double density)
        {
            var undirectedGraphNodes = UndirectedGraphGenerator.Generate(numberOfNodes, density);
            var nodes = new DirectedGraphNode[undirectedGraphNodes.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new DirectedGraphNode(undirectedGraphNodes[i].Id);
            }
            var dict = nodes.ToDictionary(x => x.Id, x => x);
            var edgeId = 0;
            for (int i = 0; i < nodes.Length; i++)
            {
                foreach (var edge in undirectedGraphNodes[i].Edges)
                {
                    DirectedGraphArc arc;
                    if (edge.Node1.Id == edge.Node2.Id)
                    {
                        arc = new DirectedGraphArc(edgeId++, nodes[i]);
                    }
                    else if (edge.Node1.Id == nodes[i].Id)
                    {
                        arc = new DirectedGraphArc(edgeId++, dict[edge.Node2.Id]);
                    }
                    else if (edge.Node2.Id == nodes[i].Id)
                    {
                        arc = new DirectedGraphArc(edgeId++, dict[edge.Node1.Id]);
                    }
                    else
                    {
                        throw new InvalidEdgeException($"Edge {edge.Id} does not connect node {nodes[i].Id}, is in its edge list.");
                    }
                    nodes[i].Add(arc);
                }
            }
            return nodes;
        }
    }
}