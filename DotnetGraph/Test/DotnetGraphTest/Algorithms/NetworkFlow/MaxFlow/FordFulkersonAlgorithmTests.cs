using DotnetGraph.Algorithms.NetworkFlow.MaxFlow;
using DotnetGraph.Algorithms.NetworkFlow.MaxFlow.FordFulkerson;

namespace DotnetGraphTest.Algorithms.NetworkFlow.MaxFlow
{
    [TestClass]
    public class FordFulkersonAlgorithmTests : MaxFlowTests
    {
        protected override IMaxFlowAlgorithm GetAlgorithm()
        {
            return new FordFulkersonAlgorithm();
        }
    }
}