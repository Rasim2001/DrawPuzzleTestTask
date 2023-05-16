using Infastructure.Services;
using Infastructure.States;

namespace Infastructure
{
    public class Game
    {
        public readonly StateMachine StateMachine;
        
        public Game(ICoroutineRunner coroutine) => 
            StateMachine = new StateMachine(new SceneLoader(coroutine), AllServices.Container);
    }
}