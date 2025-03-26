using Cannon;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.UI;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticData;
        private readonly ICustomPhysicsService _customPhysicsService;
        private readonly IUIService _uiService;
        
        private CannonController _cannonController;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            IGameFactory gameFactory, IStaticDataService staticData , ICustomPhysicsService customPhysicsService
            , IUIService uiService)
        {
            _uiService = uiService;
            _customPhysicsService = customPhysicsService;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _gameStateMachine.Enter<GameLoopState>();
        }
        
        private void InitGameWorld()
        {
            LevelStaticData levelData = LevelStaticData();

            _cannonController = _gameFactory.GetCannon(levelData.CannonSpawnPos);
            _gameFactory.Instantiate(AssetsAddress.ObstaclesPath, levelData.ObstacleSpawnPos);
            
            _customPhysicsService.Init(_staticData.GetStaticData<CannonStaticData>());
            
            _uiService.LoadPowerPanel();
        }

        private LevelStaticData LevelStaticData() =>
            _staticData.GetStaticData<LevelStaticData>();
    }
}