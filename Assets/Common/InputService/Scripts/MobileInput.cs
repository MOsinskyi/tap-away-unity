using Common.InputService.Interfaces;
using UnityEngine;

namespace Common.InputService.Scripts
{
    public sealed class MobileInput : IInputHandler 
    {
        public Ray GetClickRay(Camera camera)
        {
            return camera.ScreenPointToRay(Input.GetTouch(0).position);
        }
    }
}