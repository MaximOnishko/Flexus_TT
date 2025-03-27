using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Gameplay.GameInput
{
    public class InputHandler : MonoBehaviour
    {
        private IInputService _inputService;
        private Vector2 _inputDirection = Vector2.zero;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _inputService.Fire();

            _inputDirection.Set(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            if (_inputDirection != Vector2.zero)
                _inputService.Rotate(_inputDirection);
        }
    }
} 