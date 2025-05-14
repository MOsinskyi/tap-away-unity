using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Utils
{
  public sealed class ResourcesLoader
  {
    private readonly SortingTools _sortingTools;

    public ResourcesLoader(SortingTools sortingTools)
    {
      _sortingTools = sortingTools;
    }

    public List<T> LoadLevels<T>() where T : Object
    {
      var path = $"Prefabs/{typeof(T).Name}s";
      var levels = Resources.LoadAll<T>(path);
      if (levels.Length == 0)
      {
        Debug.LogError($"Failed to load levels from {path}");
        return null;
      }

      return _sortingTools.SortByNumber(levels);
    }

    public Sprite LoadSprite(string name)
    {
      var path = $"Sprites/{name}";
      if (Resources.Load<Sprite>(path) != null)
      {
        return Resources.Load<Sprite>(path);
      }

      Debug.LogError($"Failed to load sprite from {path}");
      return null;
    }
  }
}