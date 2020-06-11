using Ardalis.GuardClauses;
using BlazorState;
using Common;

namespace Experiment.Features.Counter
{
    public partial class CounterState : State<CounterState>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CounterState" /> class.
        ///     Required Parameterless constructor for automatic creation of the State.
        /// </summary>
        [IoC]
        public CounterState() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CounterState" /> class.
        ///     Creates new instance based off an existing one.
        /// </summary>
        /// <remarks>Constructor used for Clone.</remarks>
        /// <param name="state">The item we want to clone.</param>
        protected CounterState(CounterState state) => Count = Guard.Against.Null(state, nameof(state)).Count;

        /// <summary>
        ///     Set the Initial State.
        /// </summary>
        public override void Initialize() => Count = 3;
    }
}
