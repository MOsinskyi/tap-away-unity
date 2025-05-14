using UnityEngine;

namespace Common.Utils
{
    public class ConfigLoader
    {
        public T Load<T>() where T : Object
        {
            var path = $"Configs/{typeof(T).Name}";
            return Resources.Load<T>(path);
        }
    }
}