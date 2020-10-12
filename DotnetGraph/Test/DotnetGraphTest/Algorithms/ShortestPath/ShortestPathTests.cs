using DotnetGraph.Algorithms.ShortestPath;
using DotnetGraph.Model.Graphs.WeightedDirectedGraph;
using DotnetGraph.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DotnetGraphTest.Algorithms.ShortestPath
{
    [TestClass]
    public abstract class ShortestPathTests
    {
        protected abstract IShortestPathAlgorithm GetShortestPathAlgorithm();

        [TestMethod]
        public void Test()
        {
            var algorithm = GetShortestPathAlgorithm();

            var nodes = new List<IWeightedDirectedGraphNode>()
            {
                new WeightedDirectedGraphNode(),
                new WeightedDirectedGraphNode(),
                new WeightedDirectedGraphNode(),
                new WeightedDirectedGraphNode(),
                new WeightedDirectedGraphNode(),
                new WeightedDirectedGraphNode()
            };

            nodes.AddArcInBothDirections(0, 1, 5);
            nodes.AddArcInBothDirections(0, 3, 1);
            nodes.AddArcInBothDirections(1, 2, 3);
            nodes.AddArcInBothDirections(1, 4, 2);
            nodes.AddArcInBothDirections(2, 5, 1);
            nodes.AddArcInBothDirections(3, 4, 1);
            nodes.AddArcInBothDirections(4, 5, 1);
            var result = algorithm.GetShortestPath(nodes, 0, 5);
            Assert.IsNotNull(result);
        }
    }
}
