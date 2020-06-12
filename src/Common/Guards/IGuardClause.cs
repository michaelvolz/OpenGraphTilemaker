// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     Simple interface to provide a generic mechanism to build guard clause extension methods from.
    /// </summary>
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Marker Interface")]
    public interface IGuardClause { }
}
