using DotnetGraph.Algorithms.GraphGeneration.DirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.DiscoverableDirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.DiscoverableDirectedGraphGeneration;

public class DiscoverableDirectedGraphGenerator : IDiscoverableDirectedGraphGenerator
{
    public IDirectedGraphGenerator DirectedGraphGenerator { get; set; } = new UndirectedToDirectedGraphGenerator();
    public DiscoverableDirectedGraphNode[] Generate(int numberOfNodes, double density)
    {
        var directedGraphNodes = DirectedGraphGenerator.Generate(numberOfNodes, density);
        var nodes = new DiscoverableDirectedGraphNode[directedGraphNodes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new DiscoverableDirectedGraphNode(directedGraphNodes[i].Id);
        }

        var dict = nodes.ToDictionary(x => x.Id, x => x);
        for (int i = 0; i < nodes.Length; i++)
        {
            foreach (var arc in directedGraphNodes[i].OutgoingArcs)
            {
                var destination = dict[arc.Destination.Id];
                var weightedArc = new DiscoverableDirectedGraphArc(arc.Id, destination);
                nodes[i].AddArc(weightedArc);
            }
        }
        return nodes;
    }
}
