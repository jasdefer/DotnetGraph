using DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration;
using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.UndirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DotnetGraphTest.Algorithms.GraphGeneration.UndirectedGraphGeneration
{
    [TestClass]
    public abstract class UndirectedGraphGeneratorFixture
    {
        protected abstract IUndirectedGraphGenerator GetGenerator();

        protected virtual void AssertNodes(UndirectedGraphNode[] nodes) { }

        [TestMethod]
        public void Monkey()
        {
            var generator = GetGenerator();
            for (int i = 0; i < 10; i++)
            {
                var numberOfNodes = 4 + 100 * i;
                var density = GraphPropertyHelper.GetDensityByEdgesPerNode(numberOfNodes, 2.5);
                var nodes = generator.Generate(numberOfNodes, density).ToArray();

                Assert.IsNotNull(nodes);
                Assert.AreEqual(numberOfNodes, nodes.Length);
                GraphValidation.ValidateUniqueIds(nodes);
                GraphValidation.ValidateUniqueEdgeIds<UndirectedGraphNode, UndirectedGraphEdge>(nodes);
                GraphValidation.ValidateConsistentEdgeNodes<UndirectedGraphNode, UndirectedGraphEdge>(nodes);
                AssertNodes(nodes);
            }
        }

        [TestMethod]
        public void MonkeyDensity()
        {
            var generator = GetGenerator();
            var delta = 0d;
            var numberOfNodes = 1000;

            var density = GraphPropertyHelper.GetDensityByEdgesPerNode(numberOfNodes, 3);
            var expectedNumberOfEdges = density * GraphPropertyHelper.NumberOfPossibleEdges(numberOfNodes);
            for (int i = 0; i < 10; i++)
            {
                var nodes = generator.Generate(numberOfNodes, density).ToArray();

                delta += nodes.Sum(x => x.Edges.Count) / 2d;
                delta -= expectedNumberOfEdges;
            }

            Assert.AreEqual(0, delta, expectedNumberOfEdges * 0.5);
        }
    }
}