using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotnetGraph.Helper;
using System.Linq;

namespace DotnetGraphTest.Helper
{
    [TestClass]
    public class CollectionExtensionsTest
    {
        [TestMethod]
        public void ToDictionary()
        {
            var graph = GraphGenerator.GetSmallGraph();
            var nodes = graph.ExtractNodes();
            var dict = nodes.ToDictionary();
            Assert.IsNotNull(dict);
            Assert.AreEqual(0, dict.Min(x => x.Value));
            Assert.AreEqual(5, dict.Max(x => x.Value));
        }
    }
}