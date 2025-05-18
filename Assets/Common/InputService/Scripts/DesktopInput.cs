using Common.InputService.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common.InputService.Scripts
{
    public sealed class DesktopInput : IInputHandler
    {
        private Mouse _mouse;

        [Inject]
        private void Construct()
        {
            _mouse = Mouse.current;
        }
        
        public Ray GetClickRay(Camera camera)
        {
            if (_mouse == null) return default;
            
            var mousePosition = _mouse.position.ReadValue();;
            return camera.ScreenPointToRay(mousePosition);
        }
        
        public bool IsPressed => _mouse != null && _mouse.leftButton.wasPressedThisFrame;
    }
}