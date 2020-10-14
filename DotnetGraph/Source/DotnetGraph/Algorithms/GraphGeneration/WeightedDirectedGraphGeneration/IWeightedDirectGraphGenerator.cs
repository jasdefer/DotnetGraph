using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration
{
    public interface IWeightedDirectGraphGenerator
    {
        public WeightedDirectedGraphNode[] Generate(int numberOfNodes, double density, IWeightGenerator weightGenerator);
    }
}