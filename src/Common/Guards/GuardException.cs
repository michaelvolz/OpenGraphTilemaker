using System;
using System.Runtime.Serialization;

// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     Specialized Guard <see cref="Exception" /> which overwrites the <see cref="ToString" /> method to trim the
    ///     included <see cref="System.Diagnostics.StackTrace" />.
    /// </summary>
    [Serializable]
    public class GuardException : ArgumentException
    {
        [NonSerialized] public const string GuardPrefix = "GUARD";

        [NonSerialized] private static readonly string PrefixedMessage = $"{GuardPrefix} exception!";

        public GuardException() { }

        public GuardException(Exception innerException)
            : base(PrefixedMessage, innerException) { }

        public GuardException(string message)
            : base($"{PrefixedMessage} {message}") { }

        public GuardException(string message, Exception innerException)
            : base($"{PrefixedMessage} {message}", innerException) { }

        public GuardException(string message, string paramName)
            : base($"{PrefixedMessage} {message}", paramName) { }

        public GuardException(string message, string paramName, Exception innerException)
            : base($"{PrefixedMessage} {message}", paramName, innerException) { }

        protected GuardException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override string StackTrace => this.RewindStackTraceMessage();

        public override string ToString() => this.RewindStackTraceMessage() + base.ToString();
    }
}
