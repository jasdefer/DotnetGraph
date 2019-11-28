using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotnetGraph.Helper;
using System.Linq;

namespace DotnetGraphTest.Helper
{
    [TestClass]
    public class ArcExtensionsTest
    {
        [TestMethod]
        public void ExtractNodes()
        {
            var graph = GraphGenerator.GetSmallGraph();
            var nodes = graph.ExtractNodes();
            Assert.IsNotNull(nodes);
            Assert.AreEqual(6, nodes.Count());
        }
    }
}