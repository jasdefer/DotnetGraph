using DotnetGraph.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.ModelTests
{
    [TestClass]
    public class CompactGraphTest
    {
        private static readonly Arc<string>[] smallGraph = GraphGenerator.GetSmallGraph();
        private static readonly CompactGraph<string> smallCompactGraph = new CompactGraph<string>(smallGraph);
        
        [TestMethod]
        public void SmallGraphDimension()
        {
            Assert.IsNotNull(smallCompactGraph);
            Assert.IsNotNull(smallCompactGraph.Successors);
            Assert.IsNotNull(smallCompactGraph.Predecessors);
            Assert.IsNotNull(smallCompactGraph.Arcs);
            Assert.IsNotNull(smallCompactGraph.DestinationSortedArcs);
            Assert.AreEqual(7, smallCompactGraph.Successors.Length);
            Assert.AreEqual(7, smallCompactGraph.Predecessors.Length);
            Assert.AreEqual(7, smallCompactGraph.Predecessors.Length);
            Assert.AreEqual(14, smallCompactGraph.DestinationSortedArcs.Length);
            Assert.AreEqual(14, smallCompactGraph.Arcs.Length);
        }

        [TestMethod]
        public void SmallGraphCountSuccessors()
        {
            Assert.AreEqual(2, smallCompactGraph.CountSuccessors(0));
        }

        [TestMethod]
        public void BaseArcs()
        {
        }
    }
}