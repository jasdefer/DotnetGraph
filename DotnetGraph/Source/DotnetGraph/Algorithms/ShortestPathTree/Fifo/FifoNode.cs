namespace DotnetGraph.Algorithms.ShortestPathTree.Fifo;

public class FifoNode :
    IHasOutgoingArcs<FifoArc>,
    IHasId
{
    private readonly List<FifoArc> outgoingArcs;
    public FifoNode(int id, IList<FifoArc> outgoingArcs = null)
    {
        Id = id;
        this.outgoingArcs = outgoingArcs == null ? new List<FifoArc>() : new List<FifoArc>(outgoingArcs);
    }

    public double? DistanceFromOrigin { get; internal set; }
    public FifoArc BestPredecessor { get; internal set; }
    public IReadOnlyCollection<FifoArc> OutgoingArcs => outgoingArcs;
    public int Id { get; }
    public void Add(FifoArc arc)
    {
        outgoingArcs.Add(arc);
    }
}
