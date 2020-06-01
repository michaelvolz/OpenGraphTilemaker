using BlazorState;
using Common;

// ReSharper disable MemberCanBePrivate.Global

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
        protected FormState(FormState state) => Person = state.Person;

        /// <summary>
        ///     Clone the existing object.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        //public override object Clone() => new FormState(this);

        /// <summary>
        ///     Set the Initial State.
        /// </summary>
        public override void Initialize() => Person = new Person {FirstName = "John", LastName = "Doe", Age = 32};
    }
}