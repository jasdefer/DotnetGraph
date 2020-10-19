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
            var numberOfPossibleEdges = numberOfNodes * (numberOfNodes - 1) / 2;
            return numberOfPossibleEdges;
        }

        public static int NumberOfPossibleArcs(int numberOfNodes)
        {
            var numberOfPossibleArcs = 2 * NumberOfPossibleEdges(numberOfNodes);
            return numberOfPossibleArcs;
        }
    }
}