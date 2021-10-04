using DotnetGraph.Algorithms.GraphGeneration.DirectedGraphGeneration;
using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.DirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DotnetGraphTest.Algorithms.GraphGeneration.DirectedGraphGeneration
{
    [TestClass]
    public abstract class DirectedGraphGenerationFixture
    {
        protected abstract IDirectedGraphGenerator GetGenerator();

        protected virtual void AssertNodes(DirectedGraphNode[] nodes) { }

        [TestMethod]
        public void Monkey()
        {
            var generator = GetGenerator();
            for (int i = 0; i < 10; i++)
            {
                var numberOfNodes = 4 + 100 * i;
                var density = GraphPropertyHelper.GetDensityByArcsPerNode(numberOfNodes, 2.5);
                var expectedNumberOfArcs = density * GraphPropertyHelper.NumberOfPossibleArcs(numberOfNodes);
                var nodes = generator.Generate(numberOfNodes, density).ToArray();

                Assert.IsNotNull(nodes);
                Assert.AreEqual(numberOfNodes, nodes.Length);
                GraphValidation.ValidateUniqueIds(nodes);
                GraphValidation.ValidateUniqueArcIds<DirectedGraphNode, DirectedGraphArc>(nodes);
                var directedEdgeIds = nodes.SelectMany(x => x.OutgoingArcs.Select(y => y.Id)).Distinct().ToArray();
                Assert.AreEqual(expectedNumberOfArcs, directedEdgeIds.Length, Math.Ceiling(expectedNumberOfArcs * 0.1) + 2);
                AssertNodes(nodes);
            }
        }
    }
}