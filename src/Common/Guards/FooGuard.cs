using System;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
// ReSharper disable CheckNamespace

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     Foo Guard.
    /// </summary>
    public static partial class GuardClauseExtensions
    {
        public static void Foo(this IGuardClause guardClause, string input, string parameterName)
        {
            if (input?.ToUpperInvariant() == "FOO")
                throw new ArgumentException("Should not have been foo!", parameterName);
        }
    }
}