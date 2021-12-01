namespace DotnetGraph.Helper.Exceptions;

public class NegativeWeightException : Exception
{
    public NegativeWeightException(string message) : base(message)
    {
    }

    public NegativeWeightException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NegativeWeightException()
    {
    }
}
