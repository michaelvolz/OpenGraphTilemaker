using System;
using BlazorState;

namespace Experiment.Tests
{
    public class MockStore : IStore
    {
        private IState? _state;

        public void Reset() => throw new NotImplementedException();

        public Guid Guid { get; } = Guid.NewGuid();

        public TState GetState<TState>()
        {
            if (_state == null) throw new InvalidOperationException("State not set!");

            return (TState)_state;
        }

        public object GetState(Type aType)
        {
            if (_state == null) throw new InvalidOperationException("State not set!");

            return _state;
        }

        public void SetState(IState aState)
        {
            aState.Initialize();
            _state = aState;
        }
    }
}