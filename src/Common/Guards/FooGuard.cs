using System;
using System.Globalization;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
// ReSharper disable CheckNamespace
#pragma warning disable SA1614 // Element parameter documentation should have text
#pragma warning disable SA1627 // Documentation text should not be empty

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// Foo Guard.
    /// </summary>
    public static partial class GuardClauseExtensions
    {
        public static void Foo(this IGuardClause guardClause, string input, string parameterName) {
            if (input?.ToUpperInvariant() == "FOO")
                throw new ArgumentException("Should not have been foo!", parameterName);
        }
    }
}

#pragma warning restore SA1614 // Element parameter documentation should have text
#pragma warning restore SA1627 // Documentation text should not be empty