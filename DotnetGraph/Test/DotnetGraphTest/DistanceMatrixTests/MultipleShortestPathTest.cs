using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Algorithms.DistanceMatrix;
using DotnetGraph.Algorithms.ShortestPathTree;

namespace DotnetGraphTest.DistanceMatrixTests
{
    public class MultipleShortestPathTest : DistanceMatrixFixture
    {
        protected override IDistanceMatrixAlgorithm GetDistanceMatrixAlgorithm()
        {
            return new MultipleShortestPath(new Fifo());
        }
    }
}