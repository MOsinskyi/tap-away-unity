using Common.InputService.Interfaces;
using UnityEngine;

namespace Common.InputService.Scripts
{
    public sealed class DesktopInput : IInputHandler
    {
        public Ray GetClickRay(Camera camera)
        {
            return camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}