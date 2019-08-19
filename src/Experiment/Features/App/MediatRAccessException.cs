using System;
using System.Runtime.Serialization;

namespace Experiment.Features.App
{
    [Serializable]
    public class MediatRAccessException : Exception
    {
        public MediatRAccessException() { }
        public MediatRAccessException(string message) : base(message) { }
        public MediatRAccessException(string message, Exception inner) : base(message, inner) { }

        protected MediatRAccessException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
