using DotnetGraph.Algorithms.DepthFirstSearch;
using DotnetGraph.Algorithms.DepthFirstSearch.CormenDepthFirstSearch;
using DotnetGraphTest.Algorithms.DfSearch;

namespace DotnetGraphTest.Algorithms.DepthFirstSearch;

[TestClass]
public class CormenDepthFirstSearchAlgorithmTests : DepthFirstSearchTests
{
    protected override IDepthFirstSearchAlgorithm GetAlgorithm()
    {
        return new CormenDepthFirstSearchAlgorithm();
    }
}
