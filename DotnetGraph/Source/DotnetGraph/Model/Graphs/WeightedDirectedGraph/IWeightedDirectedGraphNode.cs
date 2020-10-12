using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Graphs.WeightedDirectedGraph
{
    public interface IWeightedDirectedGraphNode : IHasOutgoingArcs<IWeightedDirectedGraphArc>
    {
        
    }
}