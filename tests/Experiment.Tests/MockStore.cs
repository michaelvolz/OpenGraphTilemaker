using System;
using BlazorState;
using JetBrains.Annotations;

namespace Experiment.Tests
{
    public class MockStore : IStore
    {
        private IState? _state;

        public void Reset() => throw new NotImplementedException();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
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

        public void SetState([NotNull] IState aState)
        {
            if (aState == null) throw new ArgumentNullException(nameof(aState));
            
            aState.Initialize();
            _state = aState;
        }
    }
}