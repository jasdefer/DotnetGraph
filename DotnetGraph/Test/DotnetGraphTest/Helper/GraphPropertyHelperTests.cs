using DotnetGraph.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Helper
{
    [TestClass]
    public class GraphPropertyHelperTests
    {
        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 0)]
        [DataRow(2, 1)]
        [DataRow(3, 3)]
        [DataRow(4, 6)]
        [DataRow(5, 10)]
        public void NumberOfPossibleEdges(int numberOfNodes, int numberOfExpectedPossibleEdges)
        {
            var numberOfPossiblesEdges = GraphPropertyHelper.NumberOfPossibleEdges(numberOfNodes);
            Assert.AreEqual(numberOfExpectedPossibleEdges, numberOfPossiblesEdges);
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 0)]
        [DataRow(2, 2)]
        [DataRow(3, 6)]
        [DataRow(4, 12)]
        [DataRow(5, 20)]
        public void NumberOfPossibleArcs(int numberOfNodes, int numberOfExpectedPossibleArcs)
        {
            var numberOfPossiblesArcs = GraphPropertyHelper.NumberOfPossibleArcs(numberOfNodes);
            Assert.AreEqual(numberOfExpectedPossibleArcs, numberOfPossiblesArcs);
        }
    }
}