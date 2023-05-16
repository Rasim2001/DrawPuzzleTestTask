namespace Infastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadState<TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payLoad);
    }

    public interface IExitableState
    {
        void Exit();
    }
}