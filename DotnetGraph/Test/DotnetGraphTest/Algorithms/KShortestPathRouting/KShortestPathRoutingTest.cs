using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Algorithms.KShortestPathRouting;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraphTest.Helper;

namespace DotnetGraphTest.Algorithms.KShortestPathRouting;

[TestClass]
public abstract class KShortestPathRoutingTest
{
    protected abstract IKShortestPathRoutingAlgorithm GetAlgorithm();

    [TestMethod]
    public void OneArc()
    {
        // Assign
        var nodes = new WeightedDirectedGraphNode[]
        {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2)
        };
        nodes[0].Add(new WeightedDirectedGraphArc(1, 1, nodes[1]));
        var algorithm = GetAlgorithm();

        // Act
        var result = algorithm.GetKShortestPaths<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, 2, 1);

        // Assert
        result.SetOfPaths.Should().HaveCount(1);
        result.SetOfPaths.Single().Should().Equal(nodes[0].OutgoingArcs.Single());
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(10)]
    public void TwoNodesMultipleArcs(int k)
    {
        // Assign
        var nodes = new WeightedDirectedGraphNode[]
        {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2)
        };
        for (int i = 0; i < 10; i++)
        {
            nodes[0].Add(new WeightedDirectedGraphArc(i + 1, i, nodes[1]));
        }

        // Act
        var algorithm = GetAlgorithm();
        var result = algorithm.GetKShortestPaths<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, 2, k);

        result.SetOfPaths.Should().HaveCount(k);
        result.SetOfPaths.Min(x => x.Sum(y => y.Weight)).Should().Be(0);
        result.SetOfPaths.Max(x => x.Sum(y => y.Weight)).Should().Be(k - 1);
    }

    [DataTestMethod]
    [DataRow(-10)]
    [DataRow(-1)]
    [DataRow(0)]
    public void InvalidK(int k)
    {
        // Assign
        var nodes = new WeightedDirectedGraphNode[]
        {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2)
        };
        nodes[0].Add(new WeightedDirectedGraphArc(1, 1, nodes[1]));
        var algorithm = GetAlgorithm();

        // Act
        algorithm.Invoking(x => x.GetKShortestPaths<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, 2, k))
            .Should()
            .Throw<ArgumentException>();
    }

    [DataTestMethod]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(10)]
    public void KBiggerThanPaths(int k)
    {
        // Assign
        var nodes = new WeightedDirectedGraphNode[]
        {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2)
        };
        nodes[0].Add(new WeightedDirectedGraphArc(1, 1, nodes[1]));
        var algorithm = GetAlgorithm();

        // Act
        var result = algorithm.GetKShortestPaths<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, 2, k);

        // Assert
        result.SetOfPaths.Should().HaveCount(1);
        result.SetOfPaths.Single().Should().Equal(nodes[0].OutgoingArcs.Single());
    }

    [TestMethod]
    public void SmallGraph()
    {
        //Data preparation
        var nodes = GraphLibrary.SmallWeightedDirectedGraph();

        //Run test method
        var algorithm = GetAlgorithm();
        var result = algorithm.GetKShortestPaths<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, 6, 3);

        //Validate results
        result.Should().NotBeNull();
        result.SetOfPaths.Should().HaveCount(3);
        var shortestPaths = new List<int[]>
        {
            new int[]{3,11,13},
            new int[]{3,11,8,5,9},
            new int[]{1,7,13},
        };
        foreach (var path in result.SetOfPaths)
        {
            path[^1].Destination.Id.Should().Be(6);
            var arcIds = new List<int>();
            foreach (var arc in path)
            {
                arcIds.Add(arc.Id);
            }
            var match = shortestPaths.SingleOrDefault(x => Enumerable.SequenceEqual(x, arcIds));
            match.Should().NotBeNull();
            shortestPaths.Remove(match);
        }
        shortestPaths.Should().BeEmpty();
    }

    [TestMethod]
    public void Monkey()
    {
        var algorithm = GetAlgorithm();
        var generator = new WeightedDirectedGraphGenerator();
        var weightGenerator = new UniformNumberGenerator();
        for (int i = 0; i < 10; i++)
        {
            var numberOfNodes = 10 + 100 * i;
            var density = 4d / (numberOfNodes + 1);
            var nodes = generator.Generate(numberOfNodes, density, weightGenerator);
            var k = i % 5 + 1;
            var kShortestPaths = algorithm.GetKShortestPaths<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, nodes.Length - 1, k);
            kShortestPaths.Should().NotBeNull();
            kShortestPaths.SetOfPaths.Should().HaveCount(k);
        }
    }
}
