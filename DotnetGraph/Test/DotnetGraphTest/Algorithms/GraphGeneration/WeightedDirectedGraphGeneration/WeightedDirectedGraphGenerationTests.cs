using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration
{
    [TestClass]
    public abstract class WeightedDirectedGraphGenerationTests
    {
        protected abstract IWeightedDirectedGraphGenerator GetGenerator();

        [TestMethod]
        public void Monkey()
        {
            var generator = GetGenerator();
            var weightGenerator = new UniformNumberGenerator();
            for (int i = 0; i < 10; i++)
            {
                var numberOfNodes = 4 + 100 * i;
                var numberOfConnections = GraphPropertyHelper.NumberOfPossibleArcs(numberOfNodes);
                var density = Math.Min(numberOfNodes * 4.5d, numberOfConnections) / numberOfConnections;
                var expectedNumberOfArcs = density * numberOfConnections;
                var nodes = generator.Generate(numberOfNodes, density, weightGenerator).ToArray();

                Assert.IsNotNull(nodes);
                Assert.AreEqual(numberOfNodes, nodes.Length);
                var directedEdgeIds = nodes.SelectMany(x => x.OutgoingArcs.Select(y => y.Id)).Distinct().ToArray();
                Assert.AreEqual(expectedNumberOfArcs, directedEdgeIds.Length, Math.Ceiling(expectedNumberOfArcs * 0.1));
            }
        }
    }
}