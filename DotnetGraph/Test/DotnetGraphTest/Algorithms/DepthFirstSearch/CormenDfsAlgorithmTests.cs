using DotnetGraph.Algorithms.DepthFirstSearch;
using DotnetGraph.Algorithms.DepthFirstSearch.CormenDfs;
using DotnetGraphTest.Algorithms.DfSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.DepthFirstSearch
{
    [TestClass]
    public class CormenDfsAlgorithmTests : DepthFirstSearchTests
    {
        protected override IDepthFirstSearchAlgorithm GetAlgorithm()
        {
            return new CormenDfsAlgorithm();
        }
    }
}