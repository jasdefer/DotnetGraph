﻿using DotnetGraph.Algorithms.ShortestPathTree;
using DotnetGraph.Algorithms.ShortestPathTree.Fifo;

namespace DotnetGraphTest.Algorithms.ShortestPathTree;

[TestClass]
public class FifoAlgorithmTests : ShortestPathTreeTests
{
    protected override IShortestPathTreeAlgorithm GetAlgorithm()
    {
        return new FifoAlgorithm();
    }
}
