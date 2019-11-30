using System.Collections.Generic;

namespace DotnetGraph.Model
{
    public class DistanceMatrix<T>
    {
        private readonly Dictionary<T, Dictionary<T, double>> distances;

        public DistanceMatrix(Dictionary<T, Dictionary<T, double>> distances)
        {
            this.distances = new Dictionary<T, Dictionary<T, double>>();
            if (!(distances is null))
            {
                foreach (var origin in distances.Keys)
                {
                    this.distances.Add(origin, new Dictionary<T, double>());
                    foreach (var destination in distances[origin])
                    {
                        this.distances[origin].Add(destination.Key, destination.Value);
                    }
                }
            }
        }
        public double GetDistance(T origin, T destination)
        {
            return distances[origin][destination];
        }
    }
}