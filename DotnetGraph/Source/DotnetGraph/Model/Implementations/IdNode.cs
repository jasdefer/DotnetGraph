using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Model.Implementations
{
    public class IdNode<TArc> : Node<TArc>,
        IHasOutgoingArcs<TArc>,
        IHasId
        where TArc : IHasDestination<Node<TArc>>
    {
        public IdNode(int id)
        {
            Id = id;
        }

        public IdNode(int id, IEnumerable<TArc> outgoingArcs) : base(outgoingArcs)
        {
            Id = id;
        }

        public int Id { get; }
    }
}