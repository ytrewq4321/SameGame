using Assets.CodeBase.Logic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;
        private readonly GridView _gridView;

        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IStaticDataService staticDataService,
            IUIFactory uiFactory, GridView gridView)
        {

            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _gridView = gridView;
        }
        

        public void Enter(string sceneName)
        {
            _curtain.Show();

            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        
        }
        
        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InitHud();

            _gameStateMachine.Enter<GameplayState>();
        }
        
        private void InitUIRoot()
        {
            _uiFactory.CreateUIRoot();
        }

        private void InitGameWorld()
        {
            var levelData = GetLevelStaticData();
            InitGrid(levelData);
        }

        private void InitGrid(LevelStaticData levelData)
        {
            _gridView.Init(levelData.GridSize, levelData.ObjectCount, levelData.ShiftDirection);
        }

        private void InitHud()
        {
            GameObject hud = _gameFactory.CreateHud();
        }

        private LevelStaticData GetLevelStaticData() =>
            _staticDataService.ForLevel(1);
    }
}