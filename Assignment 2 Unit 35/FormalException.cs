using System;
using System.Runtime.Serialization;

namespace Assignment_2_Unit_35
{
    [Serializable]
    internal class FormalException : Exception
    {
        public FormalException()
        {
        }

        public FormalException(string message) : base(message)
        {
        }

        public FormalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FormalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}