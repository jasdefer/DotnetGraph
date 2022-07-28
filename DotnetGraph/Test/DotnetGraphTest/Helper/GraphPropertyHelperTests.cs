using DotnetGraph.Helper;

namespace DotnetGraphTest.Helper;

[TestClass]
public class GraphPropertyHelperTests
{
    [DataTestMethod]
    [DataRow(0, 0)]
    [DataRow(1, 0)]
    [DataRow(2, 1)]
    [DataRow(3, 3)]
    [DataRow(4, 6)]
    [DataRow(5, 10)]
    public void NumberOfPossibleEdges(int numberOfNodes, int numberOfExpectedPossibleEdges)
    {
        var numberOfPossiblesEdges = GraphPropertyHelper.NumberOfPossibleEdges(numberOfNodes);
        Assert.AreEqual(numberOfExpectedPossibleEdges, numberOfPossiblesEdges);
    }

    [DataTestMethod]
    [DataRow(0, 0)]
    [DataRow(1, 0)]
    [DataRow(2, 2)]
    [DataRow(3, 6)]
    [DataRow(4, 12)]
    [DataRow(5, 20)]
    public void NumberOfPossibleArcs(int numberOfNodes, int numberOfExpectedPossibleArcs)
    {
        var numberOfPossiblesArcs = GraphPropertyHelper.NumberOfPossibleArcs(numberOfNodes);
        Assert.AreEqual(numberOfExpectedPossibleArcs, numberOfPossiblesArcs);
    }

    [TestMethod]
    public void GetDensityWithEdges()
    {
        var density = GraphPropertyHelper.GetDensityWithEdges(4, 0);
        Assert.AreEqual(0, density);

        density = GraphPropertyHelper.GetDensityWithEdges(4, 1);
        Assert.AreEqual(1 / 6d, density);

        density = GraphPropertyHelper.GetDensityWithEdges(4, 2);
        Assert.AreEqual(1 / 3d, density);

        density = GraphPropertyHelper.GetDensityWithEdges(4, 3);
        Assert.AreEqual(0.5, density);

        density = GraphPropertyHelper.GetDensityWithEdges(4, 4);
        Assert.AreEqual(2 / 3d, density);
    }

    [TestMethod]
    public void GetDensityWithArcs()
    {
        var density = GraphPropertyHelper.GetDensityWithArcs(2, 2);
        Assert.AreEqual(1, density);

        density = GraphPropertyHelper.GetDensityWithArcs(2, 1);
        Assert.AreEqual(0.5, density);

        density = GraphPropertyHelper.GetDensityWithArcs(2, 0);
        Assert.AreEqual(0, density);

        density = GraphPropertyHelper.GetDensityWithArcs(4, 3);
        Assert.AreEqual(0.25, density);
    }

    [TestMethod]
    public void GetDensityByEdgesPerNode()
    {
        var density = GraphPropertyHelper.GetDensityByEdgesPerNode(2, 0);
        Assert.AreEqual(0, density);

        density = GraphPropertyHelper.GetDensityByEdgesPerNode(3, 1);
        Assert.AreEqual(0.5, density);

        density = GraphPropertyHelper.GetDensityByEdgesPerNode(4, 1);
        Assert.AreEqual(1 / 3d, density);
    }

    [TestMethod]
    public void GetDensityByArcsPerNode()
    {
        var density = GraphPropertyHelper.GetDensityByArcsPerNode(2, 0);
        Assert.AreEqual(0, density);

        density = GraphPropertyHelper.GetDensityByArcsPerNode(2, 1);
        Assert.AreEqual(1, density);

        density = GraphPropertyHelper.GetDensityByArcsPerNode(3, 1);
        Assert.AreEqual(0.5, density);

        density = GraphPropertyHelper.GetDensityByArcsPerNode(4, 1);
        Assert.AreEqual(1 / 3d, density);
    }
}
