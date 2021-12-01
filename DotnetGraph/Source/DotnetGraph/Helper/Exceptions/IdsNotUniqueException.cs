namespace DotnetGraph.Helper.Exceptions;

public class IdsNotUniqueException : Exception
{
    public IdsNotUniqueException(string message) : base(message)
    {
    }

    public IdsNotUniqueException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public IdsNotUniqueException()
    {
    }
}
