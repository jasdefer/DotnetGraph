using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Graphs.WeightedDirectedGraph
{
    public interface IWeightedDirectedGraphArc : IHasDestination<IWeightedDirectedGraphNode>, IHasWeight
    {
        
    }
}