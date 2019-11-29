using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    internal class FifoArguments<T>
    {
        internal int Origin { get; set; }
        internal CompactGraph<T> Graph { get; }
        internal int?[] BestArrivingArc { get; set; }
        internal double[] BestDistances { get; set; }
        internal Queue<int> Queue { get; set; }

        internal FifoArguments(IEnumerable<Arc<T>> arcs, T origin)
        {
            Graph = new CompactGraph<T>(arcs);
            Origin = Graph.GetIndex(origin);
            BestArrivingArc = new int?[Graph.NodeCount];
            BestDistances = new double[Graph.NodeCount];
            Queue = new Queue<int>();
            Queue.Enqueue(Origin);
            for (int i = 0; i < Graph.NodeCount; i++)
            {
                BestArrivingArc[i] = null;
                BestDistances[i] = double.PositiveInfinity;
                if(i == Origin)
                {
                    BestDistances[i] = 0;
                }
            }
        }
    }
}