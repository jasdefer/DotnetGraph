namespace DotnetGraph.Algorithms.NetworkFlow.MaxFlow.FordFulkerson;

public class FordFulkersonNode :
    IHasId,
    IHasOutgoingArcs<FordFulkersonArc>,
    IHasIncomingArcs<FordFulkersonArc>
{
    private readonly List<FordFulkersonArc> outgoingArcs;
    private readonly List<FordFulkersonArc> incomingArcs;
    public FordFulkersonNode(int id,
        IReadOnlyCollection<FordFulkersonArc> outgoingArcs = null)
    {
        Id = id;
        this.outgoingArcs = new List<FordFulkersonArc>();
        incomingArcs = new List<FordFulkersonArc>();
        if (outgoingArcs != null)
        {
            foreach (var arc in outgoingArcs)
            {
                AddOutgoingArc(arc);
            }
        }
    }

    public int Id { get; }
    public IReadOnlyCollection<FordFulkersonArc> OutgoingArcs => this.outgoingArcs;
    public IReadOnlyCollection<FordFulkersonArc> IncomingArcs => this.incomingArcs;

    #region Breadth-First-Search
    internal FordFulkersonArc Predecessor { get; set; }

    /// <summary>
    /// The status during the breadth-first-search
    /// 0: Not 
    /// </summary>
    internal SearchState SearchState { get; set; }
    #endregion

    public void AddOutgoingArc(FordFulkersonArc arc)
    {
        if (arc is null)
        {
            throw new System.ArgumentNullException(nameof(arc));
        }

        arc.Destination.AddIncomingArc(arc);
        outgoingArcs.Add(arc);
    }

    internal void AddIncomingArc(FordFulkersonArc arc)
    {
        incomingArcs.Add(arc);
    }

    public override string ToString()
    {
        return $"Id {Id}";
    }
}
