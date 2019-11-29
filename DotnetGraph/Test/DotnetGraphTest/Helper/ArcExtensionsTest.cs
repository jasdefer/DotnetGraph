using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotnetGraph.Helper;
using System.Linq;
using DotnetGraph.Model;

namespace DotnetGraphTest.Helper
{
    [TestClass]
    public class ArcExtensionsTest
    {
        [TestMethod]
        public void ExtractNodes()
        {
            var graph = DirectedGraphGenerator.GetSmallGraph();
            var nodes = graph.ExtractNodes();
            Assert.IsNotNull(nodes);
            Assert.AreEqual(6, nodes.Count());
        }

        [TestMethod]
        public void GetTotalWeight()
        {
            var arcs = new Arc<string>[]
            {
                new Arc<string>("A","B",3),
                new Arc<string>("B","C",-1),
                new Arc<string>("C","D",7),
            };
            var totalWeight = arcs.TotalWeight();
            Assert.AreEqual(9, totalWeight);
        }
    }
}