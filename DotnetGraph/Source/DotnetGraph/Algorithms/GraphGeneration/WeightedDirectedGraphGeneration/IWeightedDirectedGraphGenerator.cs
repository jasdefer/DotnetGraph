using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration
{
    public interface IWeightedDirectedGraphGenerator
    {
        public WeightedDirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator weightGenerator);
    }
}