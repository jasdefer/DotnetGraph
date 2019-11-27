namespace DotnetGraph.Algorithms.ShortestPathTree
{
    /// <summary>
    /// All information of the graph are stored here in a way suitable for Dijkstra's algorithm.
    /// </summary>
    internal class DijkstraArguments
    {
        internal int Origin { get; }
        internal int?[] BestPredecessors { get; }
        internal double[] BestDistances { get; }
        internal Successor[][] Successors { get; }

        internal DijkstraArguments(int nodes, int origin)
        {
            BestPredecessors = new int?[nodes];
            BestDistances = new double[nodes];
            Successors = new Successor[nodes][];
            for (int i = 0; i < nodes; i++)
            {
                BestPredecessors[i] = null;
                BestDistances[i] = double.PositiveInfinity;
            }
            Origin = origin;
        }
    }
}