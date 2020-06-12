// ReSharper disable once CheckNamespace

namespace Ardalis.GuardClauses
{
    /// <summary>
    ///     An entry point to a set of Guard Clauses defined as extension methods on IGuardClause.
    /// </summary>
    /// <remarks>See http://www.weeklydevtips.com/004 on Guard Clauses.</remarks>
    public class Guard : IGuardClause
    {
        private Guard() { }

        public static IGuardClause Against { get; } = new Guard();
    }
}
