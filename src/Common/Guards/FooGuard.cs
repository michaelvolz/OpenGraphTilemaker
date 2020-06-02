using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     Foo Guard.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
    public static partial class GuardClauseExtensions
    {
        public static void Foo(this IGuardClause guardClause, string input, string parameterName)
        {
            if (input.ToUpperInvariant() == "FOO")
                throw new ArgumentException("Should not have been foo!", parameterName);
        }
    }
}