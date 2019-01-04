using System;
using BlazorState;

// ReSharper disable CheckNamespace
#pragma warning disable CA1720 // Identifier contains type name

namespace OpenGraphTilemaker.Tests
{
    public class MockStore : IStore
    {
        private IState _state;

        public Guid Guid { get; } = Guid.NewGuid();

        public TState GetState<TState>() {
            return (TState)_state;
        }

        public void SetState(IState aState) {
            _state = aState;
        }
    }
}
