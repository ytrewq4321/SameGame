using CodeBase.StaticData;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameBootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;

        public GameBootstrapState(IGameStateMachine gameStateMachine, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _staticDataService.Load();
            
            _gameStateMachine.Enter<LoadLevelState,string>("Main");
        }

        public void Exit()
        {
            
        }
    }
}