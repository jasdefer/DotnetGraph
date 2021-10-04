using DotnetGraph.Model.Implementations.Graph.DirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.DirectedGraphGeneration
{
    public interface IDirectedGraphGenerator
    {
        DirectedGraphNode[] Generate(int numberOfNodes, double density);
    }
}