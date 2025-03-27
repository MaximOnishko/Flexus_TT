using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Camera;
using Gameplay.GameInput;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        public GameLoopState(GameStateMachine gameStateMachine)
        {
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            AddInputProvider();
        }
        
        private void AddInputProvider()
        {
            var go = new GameObject("[InputHandler]");
            go.AddComponent<InputHandler>();
        }
    }
}