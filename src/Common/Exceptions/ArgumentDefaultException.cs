using System;
using System.Runtime.Serialization;

namespace Common.Exceptions
{
    [Serializable]
    public class ArgumentDefaultException : ArgumentOutOfRangeException
    {
        [NonSerialized] private const string CanNotBeDefault = "Value cannot be default.";

        public ArgumentDefaultException() { }
        public ArgumentDefaultException(string paramName) : base(paramName, CanNotBeDefault) { }
        public ArgumentDefaultException(string message, Exception innerException) : base(message, innerException) { }
        public ArgumentDefaultException(string paramName, string message) : base(paramName, message) { }
        public ArgumentDefaultException(string paramName, object actualValue, string message) : base(paramName, actualValue, message) { }

        protected ArgumentDefaultException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
