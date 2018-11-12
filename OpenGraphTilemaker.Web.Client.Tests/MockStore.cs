using System;
using BlazorState;

namespace OpenGraphTilemaker.Web.Client.Tests
{
    public class MockStore : IStore
    {
        private IState _state;

        public TState GetState<TState>() {
            return (TState) _state;
        }

        public void SetState(IState aState) {
            _state = aState;
        }

        public Guid Guid { get; } = Guid.NewGuid();
    }
}