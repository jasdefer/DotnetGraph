using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.DirectedGraph
{
    public class DirectedGraphArc : Arc<DirectedGraphNode>,
        IHasDestination<DirectedGraphNode>,
        IHasId
    {
        public DirectedGraphArc(int id, DirectedGraphNode destination) : base(destination)
        {
            Id = id;
        }

        public int Id { get; }
    }
}