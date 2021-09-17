using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;
using DotnetGraph.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration
{
    [TestClass]
    public abstract class WeightedUndirectedGraphGenerationTests
    {
        protected abstract IWeightedUndirectedGraphGenerator GetGenerator();

        [TestMethod]
        public void Monkey()
        {
            var generator = GetGenerator();
            var weightGenerator = new UniformNumberGenerator();
            for (int i = 0; i < 10; i++)
            {
                var numberOfNodes = 4 + 100 * i;
                var numberOfConnections = GraphPropertyHelper.NumberOfPossibleEdges(numberOfNodes);
                var density = Math.Min(numberOfNodes * 2.5d, numberOfConnections) / numberOfConnections;
                var expectedNumberOfEdges = density * numberOfConnections;
                var nodes = generator.Generate(numberOfNodes, density, weightGenerator).ToArray();

                Assert.IsNotNull(nodes);
                Assert.AreEqual(numberOfNodes, nodes.Length);
                var numberOfEdges = nodes.SelectMany(x => x.Edges.Select(y => y.Id)).Distinct().Count();
                Assert.AreEqual(expectedNumberOfEdges, numberOfEdges, Math.Ceiling(expectedNumberOfEdges * 0.1));
                var edges = nodes.SelectMany(x => x.Edges.Select(y => y));
                Assert.AreEqual(numberOfEdges, edges.Distinct().Count());
                Assert.AreEqual(numberOfEdges, edges.Count() / 2);
            }
        }
    }
}