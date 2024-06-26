using Assets.CodeBase.Logic;
using Assets.CodeBase.Services.Input;
using Assets.CodeBase.UI.Elements;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PoolObjectService;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.GameInstaller
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();

            BindAssetProvider();

            BindInputService();

            BindGameFactory();

            BindStateFactory();

            BindCoroutineRunner();

            BindUIFactory();

            BindStaticDataService();

            BindLoadingCurtain();

            BindSceneLoader();

            BindTileFactory();

            BindPoolService();

            BindPoolFactory();

            BindGridController();

            BindScoreData();

            BindGridView();
        }

        private void BindScoreData() => Container.Bind<ScoreData>().AsSingle();
        private void BindGridView()
        {
            var gridView = Container.InstantiatePrefabForComponent<GridView>(Resources.Load(AssetAdress.GridView));
            Container.Bind<GridView>().FromInstance(gridView).AsSingle();
        }

        private void BindLoadingCurtain()
        {
            var curtainPrefab = Resources.Load<LoadingCurtain>(AssetAdress.LoadingCurtain);
            var curtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(curtainPrefab);
            Container.Bind<LoadingCurtain>().FromInstance(curtain).AsSingle();
        }
        private void BindPoolFactory() => Container.BindInterfacesTo<PoolFactory>().AsSingle();

        private void BindGridController() => Container.Bind<GridController>().AsSingle();

        private void BindPoolService() => Container.BindInterfacesTo<PoolService>().AsSingle();

        private void BindSceneLoader() => Container.Bind<SceneLoader>().AsSingle();

        private void BindTileFactory() => Container.BindInterfacesAndSelfTo<TileFactory>().AsSingle();

        private void BindStaticDataService() => Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

        private void BindUIFactory() => Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();

        private void BindCoroutineRunner()
        {
            var coroutineRunnerPrefab = Resources.Load<CoroutineRunner>(AssetAdress.CoroutineRunner);
            var coroutineRunner = Container.InstantiatePrefabForComponent<CoroutineRunner>(coroutineRunnerPrefab);
            Container.BindInterfacesAndSelfTo<CoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
        }

        private void BindStateFactory() => Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();

        private void BindGameFactory() => Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();

        private void BindInputService() => Container.BindInterfacesAndSelfTo<InputService>().AsSingle();

        private void BindAssetProvider() => Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();

        private void BindGameStateMachine() => Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
    }
}