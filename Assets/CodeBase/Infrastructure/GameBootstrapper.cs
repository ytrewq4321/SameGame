using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        private IStateFactory _stateFactory;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, IStateFactory stateFactory)
        {
            _gameStateMachine = gameStateMachine;
            _stateFactory = stateFactory;
        }
        
        private void Start()
        {
            Application.targetFrameRate = 60;

            _gameStateMachine.RegisterState(_stateFactory.Create<GameBootstrapState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadLevelState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameplayState>());
            
            _gameStateMachine.Enter<GameBootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}