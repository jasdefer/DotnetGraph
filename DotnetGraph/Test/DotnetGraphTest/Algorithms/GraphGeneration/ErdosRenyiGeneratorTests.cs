using DotnetGraph.Algorithms.Components.StronglyConnectedComponents.Tarjan;
using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.ErdosRenyi;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotnetGraphTest.Algorithms.GraphGeneration
{
    [TestClass]
    public class ErdosRenyiGeneratorTests : WeightedDirectedGraphGenerationTests
    {
        protected override IWeightedDirectGraphGenerator GetWeightedDirectGraphGenerator()
        {
            return new ErdosRenyiGenerator();
        }

        [TestMethod]
        public void MonkeySingleComponent()
        {
            var generator = GetWeightedDirectGraphGenerator();
            var weightGenerator = new UniformWeightGenerator();
            var random = new Random(1);
            var instances = 10;
            var tarjanAlgorithm = new TarjanAlgorithm();
            for (int i = 0; i < instances; i++)
            {
                var numberOfNodes = random.Next(100, 1000);
                var density = 5d / (numberOfNodes + 1);
                density *= 1 + random.Next(0, 10) / 100d;
                var nodes = generator.Generate(numberOfNodes, density, weightGenerator);
                Assert.AreEqual(numberOfNodes, nodes.Length);
                var componentResult = tarjanAlgorithm.GetCompontents<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes);
                Assert.AreEqual(1, componentResult.NumberOfComponents);
            }
        }
    }
}