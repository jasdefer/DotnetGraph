using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations
{
    public class IdNodeWithEdge<TEdge> : NodeWithEdges<TEdge>,
        IHasEdges<TEdge>,
        IHasId
        where TEdge : IConnectsNodes<IdNodeWithEdge<TEdge>>
    {
        public IdNodeWithEdge(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}