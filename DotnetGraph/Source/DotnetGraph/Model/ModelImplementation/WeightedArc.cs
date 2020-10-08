namespace DotnetGraph.Model.ModelImplementation
{
    public class WeightedArc : Arc, IWeightedArc
    {
        public WeightedArc(INode destination, double weight) : base(destination)
        {
            Weight = weight;
        }

        public double Weight { get; }
    }
}