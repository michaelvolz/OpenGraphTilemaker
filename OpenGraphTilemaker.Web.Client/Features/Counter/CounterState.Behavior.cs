﻿using BlazorState;
using Common;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features.Counter
{
    public partial class CounterState : State<CounterState>
    {
        /// <summary>
        ///     Required Parameterless constructor for automatic creation of the State.
        /// </summary>
        [IoC]
        public CounterState() { }

        /// <summary>
        ///     Creates new instance based off an existing one.
        /// </summary>
        /// <remarks>Constructor used for Clone</remarks>
        /// <param name="state">The item we want to clone</param>
        protected CounterState(CounterState state) {
            Count = state.Count;
        }

        /// <summary>
        ///     Clone the existing object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone() => new CounterState(this);

        /// <summary>
        ///     Set the Initial State
        /// </summary>
        protected override void Initialize() => Count = 3;
    }
}