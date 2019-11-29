using DotnetGraph.Algorithms.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.DistanceMatrixTests
{
    [TestClass]
    public abstract class DistanceMatrixFixture
    {
        protected abstract IDistanceMatrixAlgorithm GetDistanceMatrixAlgorithm();

        [TestMethod]
        public void SmallGraph()
        {
            var arcs = DirectedGraphGenerator.GetSmallGraph();
            var algorithm = GetDistanceMatrixAlgorithm();
            var matrix = algorithm.GetDistanceMatrix(arcs);
            Assert.AreEqual(0, matrix.GetDistance("A", "A"));
            Assert.AreEqual(4, matrix.GetDistance("A", "B"));
            Assert.AreEqual(4, matrix.GetDistance("A", "C"));
            Assert.AreEqual(1, matrix.GetDistance("A", "D"));
            Assert.AreEqual(2, matrix.GetDistance("A", "E"));
            Assert.AreEqual(3, matrix.GetDistance("A", "F"));

            Assert.AreEqual(4, matrix.GetDistance("B", "A"));
            Assert.AreEqual(0, matrix.GetDistance("B", "B"));
            Assert.AreEqual(4, matrix.GetDistance("B", "C"));
            Assert.AreEqual(3, matrix.GetDistance("B", "D"));
            Assert.AreEqual(2, matrix.GetDistance("B", "E"));
            Assert.AreEqual(3, matrix.GetDistance("B", "F"));
        }
    }
}