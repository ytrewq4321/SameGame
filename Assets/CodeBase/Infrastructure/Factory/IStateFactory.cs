using CodeBase.Infrastructure.StateMachine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IStateFactory
    {
        public TState Create<TState>() where TState : IExitableState;
    }
}