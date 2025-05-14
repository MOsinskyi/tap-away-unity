using UnityEngine;

namespace Common.Utils
{
    public sealed class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
    {
        private void Awake()
        {
            if (this == null)
            {
                DontDestroyOnLoad(this);
            }
        }
    }
}