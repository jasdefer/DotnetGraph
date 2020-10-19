using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.UndirectedToDirectedGraph;
using DotnetGraph.Algorithms.ShortestPath;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraphTest.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DotnetGraphTest.Algorithms.ShortestPath
{
    [TestClass]
    public abstract class ShortestPathTests
    {
        protected abstract IShortestPathAlgorithm GetShortestPathAlgorithm();

        [TestMethod]
        public void SmallGraph()
        {
            //Data preparation
            var nodes = GraphLibrary.SmallWeightedDirectedGraph();

            //Run test method
            var algorithm = GetShortestPathAlgorithm();
            var result = algorithm.GetShortestPath<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, 6);

            //Validate results
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.TotalWeight);
            Assert.AreEqual(3, result.Arcs.Count);

            Assert.AreEqual(3, result.Arcs[0].Id);
            Assert.AreEqual(11, result.Arcs[1].Id);
            Assert.AreEqual(13, result.Arcs[2].Id);

            Assert.AreEqual(nodes[0].OutgoingArcs.ElementAt(1), result.Arcs[0]);
            Assert.AreEqual(nodes[3].OutgoingArcs.ElementAt(1), result.Arcs[1]);
            Assert.AreEqual(nodes[4].OutgoingArcs.ElementAt(2), result.Arcs[2]);
        }

        [TestMethod]
        public void Monkey()
        {
            var algorithm = GetShortestPathAlgorithm();
            var generator = new UndirectedToDirectedGraphGenerator();
            var weightGenerator = new UniformWeightGenerator();
            var random = new Random(1);
            for (int i = 0; i < 10; i++)
            {
                var numberOfNodes = random.Next(100, 1000);
                var density = 4d / (numberOfNodes + 1);
                var nodes = generator.Generate(numberOfNodes, density, weightGenerator);
                var shortestPathResult = algorithm.GetShortestPath<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, nodes.Length - 1);
                Assert.IsNotNull(shortestPathResult);
                Assert.IsTrue(shortestPathResult.TotalWeight >= 0);
                Assert.AreEqual(shortestPathResult.Arcs.Sum(x => x.Weight), shortestPathResult.TotalWeight);
                Assert.IsTrue(shortestPathResult.Arcs.Count > 0);
            }
        }
    }
}