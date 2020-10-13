using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Model.Implementations
{
    public class Node<TArc> : IHasOutgoingArcs<TArc> where TArc : IHasDestination<Node<TArc>>
    {
        private readonly List<TArc> outgoingArcs = new List<TArc>();
        public Node()
        {

        }

        public Node(IEnumerable<TArc> outgoingArcs)
        {
            this.outgoingArcs = new List<TArc>(outgoingArcs);
        }

        public IReadOnlyCollection<TArc> OutgoingArcs => outgoingArcs;

        public void AddArc(TArc arc)
        {
            outgoingArcs.Add(arc);
        }

        public void RemoveArc(TArc arc)
        {
            outgoingArcs.Remove(arc);
        }
    }
}