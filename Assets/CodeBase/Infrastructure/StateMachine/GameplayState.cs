using CodeBase.Logic;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameplayState : IState
    {
        private readonly LoadingCurtain _loadingCurtain;

        public GameplayState(GameStateMachine gameStateMachine,  LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Hide();      
        }

        public void Exit()
        {
        
        }
    }
}