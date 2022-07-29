using DotnetGraph.Algorithms.ShortestPath;
using DotnetGraph.Algorithms.ShortestPath.Dijkstra;

namespace DotnetGraphTest.Algorithms.ShortestPath;

[TestClass]
public class DijkstraAlgorithmTests : ShortestPathTests
{
    protected override IShortestPathAlgorithm GetShortestPathAlgorithm()
    {
        return new DijkstraAlgorithm();
    }
}
