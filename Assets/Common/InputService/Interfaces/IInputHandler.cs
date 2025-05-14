using UnityEngine;

namespace Common.InputService.Interfaces
{
    public interface IInputHandler
    {
        public Ray GetClickRay(Camera camera);
    }
}