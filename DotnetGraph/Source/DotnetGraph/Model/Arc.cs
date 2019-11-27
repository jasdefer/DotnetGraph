namespace DotnetGraph.Model
{
    public class Arc<T>
    {
        public T Origin { get; }
        public T Destination { get; }
        public double Weight { get; }

        public Arc(T origin, T destination, double weight)
        {
            Origin = origin;
            Destination = destination;
            Weight = weight;
        }

        public Arc<T> Reverse()
        {
            return new Arc<T>(Destination, Origin, Weight);
        }
    }
}