namespace DotnetGraph.Helper.Exceptions;

public class InvalidDestinationException : Exception
{
    public InvalidDestinationException(string message) : base(message)
    {
    }

    public InvalidDestinationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public InvalidDestinationException()
    {
    }
}
