using DG.Tweening;
using UnityEngine;

namespace Cube.Configs
{
  [CreateAssetMenu(fileName = "CubeConfig", menuName = "Scriptable Objects/CubeConfig")]
  public sealed class CubeConfig : ScriptableObject
  {
    [field: Header("MOVEMENT")]
    [field: SerializeField, Range(1f, 50f)]
    public float MoveDistance { get; private set; }

    [field: SerializeField] public Ease MoveEase { get; private set; }

    [field: SerializeField, Range(0f, 2f)] public float Duration { get; private set; }

    [field: Header("EFFECTS")]
    [field: Header("Colors")]
    [field: SerializeField]
    public Color WrongMoveColor { get; private set; }

    [field: SerializeField] public Color RightMoveColor { get; private set; }

    [field: SerializeField, Range(.3f, 1f)]
    public float SwitchColorDuration { get; private set; }

    [field: Header("Pop")]
    [field: SerializeField, Range(0f, 1f)] public float PopStrength { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float PopDuration { get; private set; }

    [field: Header("Bounce")]
    [field: SerializeField, Range(0f, 1f)]
    public float PunchStrength { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float PunchDuration { get; private set; }
    [field: SerializeField, Range(0, 5)] public int PunchVibrato { get; private set; }
    [field: SerializeField, Range(0.5f, 1.5f)] public float BounceDistanceThreshold { get; private set; }

    [field: Header("INSTANTIATE ANIMATION")]
    [field: SerializeField, Range(1f, 5f)]
    public float SpawnRange { get; private set; }
    [field: SerializeField, Range(0f, 2f)] public float SpawnDuration { get; private set; }
  }
}