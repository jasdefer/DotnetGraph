using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;

public class WeightedUndirectedGraphGenerator : IWeightedUndirectedGraphGenerator
{
    public IUndirectedGraphGenerator UndirectedGraphGenerator { get; set; } = new ErdosRenyiGenerator();

    public WeightedUndirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator weightGenerator)
    {
        if (weightGenerator is null)
        {
            throw new ArgumentNullException(nameof(weightGenerator));
        }

        var undirectedGraphNodes = UndirectedGraphGenerator.Generate(numberOfNodes, density);
        var nodes = new WeightedUndirectedGraphNode[undirectedGraphNodes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new WeightedUndirectedGraphNode(undirectedGraphNodes[i].Id);
        }
        var dict = nodes.ToDictionary(x => x.Id, x => x);
        for (int i = 0; i < nodes.Length; i++)
        {
            foreach (var edge in undirectedGraphNodes[i].Edges)
            {
                var node1 = dict[edge.Node1.Id];
                var node2 = dict[edge.Node2.Id];
                var weightedEdge = new WeightedUndirectedGraphEdge(edge.Id, weightGenerator.Generate(), node1, node2);
                node1.Add(weightedEdge);
                node2.Add(weightedEdge);
            }
        }
        return nodes;
    }
}
