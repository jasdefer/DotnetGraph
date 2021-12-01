namespace DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;

public class Iterator : INumberGenerator
{
    public Iterator()
    {

    }

    public Iterator(int start = 0)
    {
        NextNumber = start;
    }

    public int NextNumber { get; private set; }

    public double Generate()
    {
        return NextNumber++;
    }
}
