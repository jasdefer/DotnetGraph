namespace DotnetGraph.Helper.Exceptions;

public class InvalidEdgeException : Exception
{
    public InvalidEdgeException(string message) : base(message)
    {
    }

    public InvalidEdgeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public InvalidEdgeException()
    {
    }
}
