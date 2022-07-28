namespace DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph;

public class FlowDirectedGraphArc : IHasId, IHasCapacity, IHasDestination<FlowDirectedGraphNode>, IHasFlow
{
    public FlowDirectedGraphArc(int id,
        double capacity,
        FlowDirectedGraphNode destination,
        double flow = 0)
    {
        Id = id;
        Capacity = capacity;
        Destination = destination;
        Flow = flow;
    }

    public int Id { get; }
    public double Capacity { get; }
    public FlowDirectedGraphNode Destination { get; }
    public double Flow { get; set; }

    public override string ToString()
    {
        return $"{Id}: to {Destination.Id} ({Flow}/{Capacity})";
    }
}
