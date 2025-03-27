using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.CustomDrawer;
using CodeBase.Infrastructure.Services.Data;
using CodeBase.Infrastructure.Services.ProceduralMesh;
using CodeBase.Infrastructure.Services.UI;
using CodeBase.StaticData;
using Gameplay.Bullet.BulletService;
using Gameplay.BulletFactory;
using Infrastructure.Services.CustomPhysics;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Boot";
        private const string MainScene = "MainScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ICoroutineRunner coroutineRunner,
            AllServices services)
        {
            _coroutineRunner = coroutineRunner;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>(MainScene);

        private void RegisterServices()
        {
            _services.RegisterSingle<ICoroutineRunner>(_coroutineRunner);
            _services.RegisterSingle<IStaticDataService>(new StaticDataService());
            _services.RegisterSingle<IDataService>(new DataService());
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>()));
            _services.RegisterSingle<ICustomPhysicsService>(new CustomPhysicsService());
            _services.RegisterSingle<IUIService>(new UIService());
            _services.RegisterSingle<IInputService>(new StandaloneInput());
            _services.RegisterSingle<IProceduralMeshService>(new ProceduralMeshService());
            _services.RegisterSingle<IBulletPool>(new BulletPoolService(_services.Single<IGameFactory>(),
                _services.Single<IProceduralMeshService>(), _services.Single<IStaticDataService>()));
            _services.RegisterSingle<IDrawerService>(new RenderTextureDrawerService(_services.Single<IStaticDataService>()));
            _services.RegisterSingle<IBulletService>(new BulletService());
        }
    }
}