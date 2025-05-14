using LevelService.Interfaces;
using UnityEngine;

namespace LevelService.Scripts
{
  public sealed class MediumLevel : Level, IFixedMoves
  {
    [field: SerializeField, Min(1)] public int MovesCount { get; private set; }
  }
}