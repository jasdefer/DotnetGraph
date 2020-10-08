namespace DotnetGraph.Model
{
    public interface IWeightedArc : IArc
    {
        double Weight { get; }
    }
}