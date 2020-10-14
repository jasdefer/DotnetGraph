using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DotnetGraphTest.Algorithms.GraphGeneration
{
    [TestClass]
    public abstract class WeightedDirectedGraphGenerationTests
    {
        protected abstract IWeightedDirectGraphGenerator GetWeightedDirectGraphGenerator();

        [TestMethod]
        public void Monkey()
        {
            var generator = GetWeightedDirectGraphGenerator();
            var weightGenerator = new UniformWeightGenerator();
            var random = new Random(1);
            var instances = 10;
            for (int i = 0; i < instances; i++)
            {
                var numberOfNodes = random.Next(100, 1000);
                var density = 5d / (numberOfNodes + 1);
                density *= 1 + random.Next(0, 10) / 100d;
                var nodes = generator.Generate(numberOfNodes, density, weightGenerator);
                Assert.AreEqual(numberOfNodes, nodes.Length);
                Assert.AreEqual(numberOfNodes, nodes.Select(x => x.Id).Distinct().Count());
                var arcs = nodes.SelectMany(x => x.OutgoingArcs).ToList();
                Assert.AreEqual(arcs.Count, arcs.Select(x => x.Id).Distinct().Count());
            }
        }
    }
}