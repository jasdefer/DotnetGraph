using DotnetGraph.Algorithms.GraphGeneration.DirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;

public class WeightedDirectedGraphGenerator : IWeightedDirectedGraphGenerator
{
    public IDirectedGraphGenerator DirectedGraphGenerator { get; set; } = new UndirectedToDirectedGraphGenerator();
    public WeightedDirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator weightGenerator)
    {
        if (weightGenerator is null)
        {
            throw new System.ArgumentNullException(nameof(weightGenerator));
        }

        var directedGraphNodes = DirectedGraphGenerator.Generate(numberOfNodes, density);
        var nodes = new WeightedDirectedGraphNode[directedGraphNodes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new WeightedDirectedGraphNode(directedGraphNodes[i].Id);
        }

        var dict = nodes.ToDictionary(x => x.Id, x => x);
        for (int i = 0; i < nodes.Length; i++)
        {
            foreach (var arc in directedGraphNodes[i].OutgoingArcs)
            {
                var weight = weightGenerator.Generate();
                var destination = dict[arc.Destination.Id];
                var weightedArc = new WeightedDirectedGraphArc(arc.Id, weight, destination);
                nodes[i].Add(weightedArc);
            }
        }
        return nodes;
    }
}
