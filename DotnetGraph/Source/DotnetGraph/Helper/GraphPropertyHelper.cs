using System;

namespace DotnetGraph.Helper
{
    public static class GraphPropertyHelper
    {
        public static int NumberOfPossibleEdges(int numberOfNodes)
        {
            if (numberOfNodes <= 1)
            {
                return 0;
            }
            if (numberOfNodes > 65000)
            {
                throw new Exception($"The number of possible edges is bigger than the maximum int value for a graph with {numberOfNodes} nodes.");
            }
            var numberOfPossibleEdges = numberOfNodes / 2 * (numberOfNodes - 1);
            return numberOfPossibleEdges;
        }

        public static int NumberOfPossibleArcs(int numberOfNodes)
        {
            var numberOfPossibleArcs = 2 * NumberOfPossibleEdges(numberOfNodes);
            return numberOfPossibleArcs;
        }

        public static int NumberOfEdges(int numberOfNodes, double density)
        {
            if (numberOfNodes <= 1)
            {
                return 0;
            }
            if (density < 0 || density > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(density));
            }

            var numberOfPossibleEdges = (int)Math.Round((numberOfNodes * density) * ((numberOfNodes - 1) / 2));
            return numberOfPossibleEdges;
        }

        public static int NumberOfArcs(int numberOfNodes, double density)
        {
            var numberOfArcs = 2 * NumberOfEdges(numberOfNodes, density);
            return numberOfArcs;
        }
    }
}