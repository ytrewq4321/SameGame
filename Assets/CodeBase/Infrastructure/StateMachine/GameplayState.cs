using Assets.CodeBase.Logic;
using Assets.CodeBase.UI.Elements;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Logic;
using CodeBase.StaticData;
using System;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameplayState : IState, IDisposable
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly GridView _gridView;
        private readonly ScoreData _scoreData;
        private readonly LoadingCurtain _loadingCurtain;
        private int _currentLevel=1;

        public GameplayState(GameStateMachine gameStateMachine, GridView gridView, IStaticDataService staticDataService, IInputService inputService, ScoreData scoreData, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _gridView = gridView;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _scoreData = scoreData;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Hide();
            _gridView.GameOver += ResetGame;
            _gridView.WinLevel += NextLevel;
        }

        public void Exit()
        {
        
        }

        private void NextLevel()
        {
            var levelData = _staticDataService.ForLevel(_currentLevel+1);
            if(levelData ==null)
                levelData = _staticDataService.ForLevel(1);

            _inputService.Disable();
            _gridView.Init(levelData.GridSize, levelData.ObjectCount, levelData.ShiftDirection);
            _inputService.Enable();
        }

        private void ResetGame()
        {
            var levelData = _staticDataService.ForLevel(_currentLevel);
            _inputService.Disable();
            _gridView.Init(levelData.GridSize, levelData.ObjectCount, levelData.ShiftDirection);
            _scoreData.ResetScore();
            _inputService.Enable();
        }

        public void Dispose()
        {
            _gridView.GameOver -= ResetGame;
            _gridView.WinLevel -= NextLevel;
        }

    }
}