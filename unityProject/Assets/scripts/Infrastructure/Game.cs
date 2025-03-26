using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, HUDLoading hudLoading)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner, hudLoading), AllServices.Container);
    }
  }
}