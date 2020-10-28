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
            //Applying the commutative law help reducing the max number in the calculation
            //This prevents some interim result bigger than int.MaxValue
            var numberOfPossibleEdges = numberOfNodes / 2d * (numberOfNodes - 1);
            return (int)numberOfPossibleEdges;
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

            var numberOfPossibleEdges = (int)Math.Round((numberOfNodes * density) * ((numberOfNodes - 1) / 2d));
            return numberOfPossibleEdges;
        }

        public static int NumberOfArcs(int numberOfNodes, double density)
        {
            var numberOfArcs = 2 * NumberOfEdges(numberOfNodes, density);
            return numberOfArcs;
        }
    }
}