using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration
{
    public interface IWeightedUndirectedGraphGenerator
    {
        WeightedUndirectedGraphNode[] Generate(int numberOfNodes, double density, IWeightGenerator weightGenerator);
    }
}