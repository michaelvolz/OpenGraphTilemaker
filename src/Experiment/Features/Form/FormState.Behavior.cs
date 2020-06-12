using Ardalis.GuardClauses;
using BlazorState;
using Common;

namespace Experiment.Features.Form
{
    public partial class FormState : State<FormState>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FormState" /> class.
        ///     Required Parameterless constructor for automatic creation of the State.
        /// </summary>
        [IoC]
        public FormState() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FormState" /> class.
        ///     Creates new instance based off an existing one.
        /// </summary>
        /// <remarks>Constructor used for Clone.</remarks>
        /// <param name="state">The item we want to clone.</param>
        protected FormState(FormState state) => Person = Guard.Against.Null(state, nameof(state)).Person;

        /// <summary>
        ///     Set the Initial State.
        /// </summary>
        public override void Initialize() => Person = new Person { FirstName = "John", LastName = "Doe", Age = 32 };
    }
}
