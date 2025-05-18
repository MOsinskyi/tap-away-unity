using System.Linq;
using Common.InputService.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common.InputService.Scripts
{
    public sealed class MobileInput : IInputHandler 
    {
        private Touchscreen _touchscreen;

        [Inject]
        private void Construct()
        {
            _touchscreen = Touchscreen.current;
        }
        
        public Ray GetClickRay(Camera camera)
        {
            if (_touchscreen == null) return default;
            
            var touchPosition = Vector2.zero;
            foreach (var touch in _touchscreen.touches
                         .Where(touch => touch.IsPressed()))
            {
                touchPosition = touch.position.ReadValue();
            }
            return camera.ScreenPointToRay(touchPosition);
        }

        public bool IsPressed => _touchscreen != null && _touchscreen.primaryTouch.press.wasPressedThisFrame;
    }
}