using System;
using BlazorState;

// ReSharper disable CheckNamespace

namespace OpenGraphTilemaker.Tests
{
    public class MockStore : IStore
    {
        private IState _state;

#pragma warning disable CA1720 // Identifier contains type name
        public Guid Guid { get; } = Guid.NewGuid();
#pragma warning restore CA1720 // Identifier contains type name

        public TState GetState<TState>() {
            return (TState)_state;
        }

        public void SetState(IState aState) {
            _state = aState;
        }
    }
}
