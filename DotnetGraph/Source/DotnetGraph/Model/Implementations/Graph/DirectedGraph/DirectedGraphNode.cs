using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Model.Implementations.Graph.DirectedGraph
{
    public class DirectedGraphNode : Node<DirectedGraphArc>,
        IHasOutgoingArcs<DirectedGraphArc>,
        IHasId
    {
        public DirectedGraphNode(int id)
        {
            Id = id;
        }

        public DirectedGraphNode(int id, IEnumerable<DirectedGraphArc> outgoingArcs) : base(outgoingArcs)
        {
            Id = id;
        }

        public int Id { get; }
    }
}