using System;
using System.Runtime.Serialization;

// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

namespace Common.Exceptions
{
    [Serializable]
    public class ArgumentNullOrWhiteSpaceException : ArgumentNullException
    {
        [NonSerialized]
        private const string CanNotBeNullOrWhiteSpace = "Value cannot be NullOrWhiteSpace.";

        public ArgumentNullOrWhiteSpaceException() { }

        public ArgumentNullOrWhiteSpaceException(string paramName) : base(paramName, CanNotBeNullOrWhiteSpace) { }

        public ArgumentNullOrWhiteSpaceException(string message, Exception innerException) : base(message, innerException) { }

        public ArgumentNullOrWhiteSpaceException(string paramName, string message) : base(paramName, message) { }

        protected ArgumentNullOrWhiteSpaceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}