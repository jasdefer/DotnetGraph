namespace DotnetGraph.Algorithms.ShortestPathTree.Fifo
{
    public record FifoArc(int Id, double Weight, FifoNode Origin, FifoNode Destination) :
        IHasWeight,
        IHasDestination<FifoNode>,
        IHasId;
}