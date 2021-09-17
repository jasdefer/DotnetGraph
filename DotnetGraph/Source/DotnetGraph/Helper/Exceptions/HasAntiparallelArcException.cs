using System;

namespace DotnetGraph.Helper.Exceptions
{
    /// <summary>
    /// Is thrown, if a graph has antiparallel arcs.
    /// </summary>
    public class HasAntiparallelArcException : Exception
    {
        public HasAntiparallelArcException(string message) : base(message)
        {
        }

        public HasAntiparallelArcException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public HasAntiparallelArcException()
        {
        }
    }
}