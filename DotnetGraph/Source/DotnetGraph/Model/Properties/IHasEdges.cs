using System.Collections.Generic;

namespace DotnetGraph.Model.Properties
{
    public interface IHasEdges<out TEdge>
    {
        IReadOnlyCollection<TEdge> Edges { get; }
    }
}