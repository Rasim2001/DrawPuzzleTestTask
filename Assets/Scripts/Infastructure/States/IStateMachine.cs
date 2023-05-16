namespace Infastructure.States
{
    public interface IStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPaylodad>(TPaylodad paylodad) where TState : class, IPayloadState<TPaylodad>;
    }
}