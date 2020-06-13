using System;
using System.Diagnostics.CodeAnalysis;
using BlazorState;

namespace Experiment.Tests
{
    public class MockStore : IStore
    {
        private IState? _state;

        [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Defined by Interface")]
        public Guid Guid { get; } = Guid.NewGuid();

        public void Reset() => throw new NotImplementedException();

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

        public void SetState([JetBrains.Annotations.NotNull] IState aState)
        {
            if (aState == null) throw new ArgumentNullException(nameof(aState));

            aState.Initialize();
            _state = aState;
        }
    }
}
