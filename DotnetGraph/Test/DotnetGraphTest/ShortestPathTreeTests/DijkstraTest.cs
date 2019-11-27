using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Algorithms.ShortestPathTree;

namespace DotnetGraphTest.ShortestPathTreeTests
{
    public class DijkstraTest : ShortestPathTreeFixture
    {
        public override IShortestPathTreeAlgorithm GetShortestPathTreeAlgorithm()
        {
            return new Dijkstra();
        }
    }
}