using DotnetGraph.Model.Properties;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    public record DijkstraArc(int Id, double Weight, DijkstraNode Origin, DijkstraNode Destination) :
        IHasWeight,
        IHasDestination<DijkstraNode>,
        IHasId;
}