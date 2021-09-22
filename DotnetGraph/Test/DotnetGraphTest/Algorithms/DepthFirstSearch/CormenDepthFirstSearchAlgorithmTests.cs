using DotnetGraph.Algorithms.DepthFirstSearch;
using DotnetGraph.Algorithms.DepthFirstSearch.CormenDepthFirstSearch;
using DotnetGraphTest.Algorithms.DfSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.DepthFirstSearch
{
    [TestClass]
    public class CormenDepthFirstSearchAlgorithmTests : DepthFirstSearchTests
    {
        protected override IDepthFirstSearchAlgorithm GetAlgorithm()
        {
            return new CormenDepthFirstSearchAlgorithm();
        }
    }
}