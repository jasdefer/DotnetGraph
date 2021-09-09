using DotnetGraph.Helper;
using DotnetGraph.Helper.Exceptions;
using DotnetGraph.Model.Implementations;
using DotnetGraph.Model.Implementations.Graph.DirectedGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Helper
{
    [TestClass]
    public class GraphValidationTests
    {
        [TestMethod]
        public void ValidateUniqueIdsValid()
        {
            var entites = CreateEntities(2);
            GraphValidation.ValidateUniqueIds(entites);
        }

        [TestMethod]
        public void ValidateUniqueIdsInvalid()
        {
            var entites = new Entity[2]
            {
                new Entity(1),
                new Entity(1)
            };
            Assert.ThrowsException<IdsNotUniqueException>(() => GraphValidation.ValidateUniqueIds(entites));
        }

        [TestMethod]
        public void ValidateUniqueArcIdsValid()
        {
            var nodes = new DirectedGraphNode[]
            {
                new DirectedGraphNode(1),
                new DirectedGraphNode(2),
                new DirectedGraphNode(3),
            };

            nodes[0].Add(new DirectedGraphArc(1, nodes[1]));
            nodes[0].Add(new DirectedGraphArc(2, nodes[2]));
            nodes[1].Add(new DirectedGraphArc(3, nodes[2]));
            GraphValidation.ValidateUniqueArcIds(nodes);
        }

        [TestMethod]
        public void ValidateUniqueArcIdsInvalid()
        {
            var nodes = new DirectedGraphNode[]
            {
                new DirectedGraphNode(1),
                new DirectedGraphNode(2),
                new DirectedGraphNode(3),
            };

            nodes[0].Add(new DirectedGraphArc(1, nodes[1]));
            nodes[0].Add(new DirectedGraphArc(2, nodes[2]));
            nodes[1].Add(new DirectedGraphArc(2, nodes[2]));
            Assert.ThrowsException<IdsNotUniqueException>(() => GraphValidation.ValidateUniqueArcIds(nodes));
        }

        [TestMethod]
        public void IdExistsValid()
        {
            var entites = CreateEntities(4);
            GraphValidation.IdExists(entites, 1, 4, 2);
        }

        [TestMethod]
        public void IdExistsInvalid()
        {
            var entites = CreateEntities(4);
            Assert.ThrowsException<InvalidIdException>(() => GraphValidation.IdExists(entites, 1, 5, 2));
        }

        [TestMethod]
        public void ValidateOnlyPositiveWeightsValid()
        {
            var nodes = new WeightedDirectedGraphNode[]
            {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2)
            };

            nodes[0].Add(new WeightedDirectedGraphArc(1, 0, nodes[1]));
            nodes[0].Add(new WeightedDirectedGraphArc(2, double.MaxValue, nodes[1]));
            GraphValidation.ValidateOnlyPositiveWeights<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes);
        }

        [TestMethod]
        public void ValidateOnlyPositiveWeightsInvalid()
        {
            var nodes = new WeightedDirectedGraphNode[]
            {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2)
            };

            nodes[0].Add(new WeightedDirectedGraphArc(1, 1, nodes[1]));
            nodes[0].Add(new WeightedDirectedGraphArc(2, -1, nodes[1]));
            Assert.ThrowsException<NegativeWeightException>(() => GraphValidation.ValidateOnlyPositiveWeights<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes));
        }

        [TestMethod]
        public void ValidateNoAntiparallelArcsInvalid()
        {
            var nodes = new DirectedGraphNode[]
            {
                new DirectedGraphNode(1),
                new DirectedGraphNode(2)
            };

            nodes[0].Add(new DirectedGraphArc(1, nodes[1]));
            nodes[1].Add(new DirectedGraphArc(2, nodes[0]));
            Assert.ThrowsException<Exception>(() => GraphValidation.ValidateNoAntiparallelArcs<DirectedGraphNode, DirectedGraphArc>(nodes));
        }

        [TestMethod]
        public void ValidateNoAntiparallelArcsValid()
        {
            var nodes = new DirectedGraphNode[]
            {
                new DirectedGraphNode(1),
                new DirectedGraphNode(2)
            };

            nodes[0].Add(new DirectedGraphArc(1, nodes[1]));
            GraphValidation.ValidateNoAntiparallelArcs<DirectedGraphNode, DirectedGraphArc>(nodes);
        }

        private static Entity[] CreateEntities(int numberOfEntities)
        {
            var entities = new Entity[numberOfEntities];
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i] = new Entity(i + 1);
            }
            return entities;
        }
    }
}