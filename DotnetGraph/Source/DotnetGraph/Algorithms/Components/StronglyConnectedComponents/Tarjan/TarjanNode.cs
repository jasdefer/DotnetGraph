namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents.Tarjan
{
    public class TarjanNode :
        IHasOutgoingArcs<TarjanArc>,
        IHasId
    {
        private readonly List<TarjanArc> outgoingArcs;
        public TarjanNode(int id, IList<TarjanArc> outgoingArcs = null)
        {
            Id = id;
            this.outgoingArcs = outgoingArcs is null ? new List<TarjanArc>() : new List<TarjanArc>(outgoingArcs);
        }

        public int? Index { get; set; }
        public int LowLink { get; set; }
        public bool IsOnStack { get; set; }
        public int Component { get; set; }
        public int Id { get; }
        public IReadOnlyCollection<TarjanArc> OutgoingArcs => outgoingArcs;
        public void Add(TarjanArc arc)
        {
            outgoingArcs.Add(arc);
        }
    }
}