using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.UI;
using CodeBase.StaticData;
using Gameplay.BulletFactory;
using Infrastructure.Services.CustomPhysics;

namespace CodeBase.Infrastructure.States
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, ICoroutineRunner coroutineRunner, AllServices services)
    {
        _states = new Dictionary<Type, IExitableState>
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, coroutineRunner,services),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader,
                services.Single<IGameFactory>(),
                services.Single<IStaticDataService>(), 
                services.Single<ICustomPhysicsService>(),
                services.Single<IUIService>(), services.Single<IBulletPool>()),
            [typeof(GameLoopState)] = new GameLoopState(this)
        };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      IPayloadedState<TPayload> state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}