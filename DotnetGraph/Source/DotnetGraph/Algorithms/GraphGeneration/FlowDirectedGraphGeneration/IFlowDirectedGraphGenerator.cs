using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration
{
    public interface IFlowDirectedGraphGenerator
    {
        public FlowDirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator capacityGenerator);
    }
}