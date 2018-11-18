using System;
using System.Runtime.Serialization;

// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable IdentifierTypo

namespace Ardalis.GuardClauses
{
    [Serializable]
    public class GuardException : ArgumentException
    {
        public const string GuardPrefix = "GUARD";
        private static readonly string PrefixedMessage = $"{GuardPrefix} exception!";

        public GuardException() { }
        public GuardException(Exception innerException) : base(PrefixedMessage, innerException) { }
        public GuardException(string message) : base($"{PrefixedMessage} {message}") { }
        public GuardException(string message, Exception innerException) : base($"{PrefixedMessage} {message}", innerException) { }
        public GuardException(string message, string paramName) : base($"{PrefixedMessage} {message}", paramName) { }
        public GuardException(string message, string paramName, Exception innerException) : base($"{PrefixedMessage} {message}", paramName, innerException) { }

        protected GuardException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString() {
            return this.RewindStackTraceMessage() + Environment.NewLine + base.ToString();;
        }
    }
}