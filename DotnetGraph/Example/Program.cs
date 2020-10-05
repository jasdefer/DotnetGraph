using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var arcs = GenRan(500,0.6).ToArray();
            Console.WriteLine($"Generated {arcs.Count()} arcs.");

            DotnetGraph.Algorithms.ShortestPathTree.Dijkstra dijkstraOld = new DotnetGraph.Algorithms.ShortestPathTree.Dijkstra();

            int trials = 5;
            Stopwatch w = new Stopwatch();
            var tr = new double[trials];
            for (int i = 0; i < trials; i++)
            {
                w.Restart();
                dijkstraOld.GetShortestPathTree(arcs, arcs[0].Origin);
                w.Stop();
                tr[i] = w.ElapsedMilliseconds;
            }
            double avg = tr.Average();
            Console.WriteLine($"Average Dijkstra: {avg:N4}");


            DotnetGraph.Algorithms.ShortestPathTree.SpDijkstra dijkstraNew = new DotnetGraph.Algorithms.ShortestPathTree.SpDijkstra();

            w.Reset();
            for (int i = 0; i < trials; i++)
            {
                w.Restart();
                dijkstraNew.GetShortestPathTree(arcs, arcs[0].Origin);
                w.Stop();
                tr[i] = w.ElapsedMilliseconds;
            }
            avg = tr.Average();
            Console.WriteLine($"Average Dijkstra (new): {avg:N4}");
        }


        private static IEnumerable<Arc<SimpleNode>> GenRan(int numberOfNodes = 200, double density = 0.4)
        {

            Random rndSrc = new Random();

            if (numberOfNodes < 3)
                throw new ArgumentException("Number of Nodes too low");
            if (density < 0.001 || density > 1)
                throw new ArgumentException("Density not between 0 and 1");
            SimpleNode[] nodes = new SimpleNode[numberOfNodes];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new SimpleNode() { Id = i + 1 };
            }

            for (int i = 0; i < numberOfNodes - 1; i++)
            {
                for (int j = i + 1; j < numberOfNodes; j++)
                {
                    if (rndSrc.NextDouble() > (1 - density))
                    {
                        double w = rndSrc.Next((j - i), 2 * (j - i));
                        yield return new Arc<SimpleNode>(nodes[i], nodes[j], w);
                    }
                }
            }
        }

        private class SimpleNode
        {
            public int Id;
        }

    }
}
