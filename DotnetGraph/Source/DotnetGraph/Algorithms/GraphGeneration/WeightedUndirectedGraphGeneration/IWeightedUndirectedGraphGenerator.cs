using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;

public interface IWeightedUndirectedGraphGenerator
{
    WeightedUndirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator weightGenerator);
}
