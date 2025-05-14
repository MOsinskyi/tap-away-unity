using DG.Tweening;
using UnityEngine;

namespace UIService.Presenters
{
  public sealed class SwipePresenter : HandPresenter
  {
    [SerializeField, Range(30f, 120f)] private float radius = 50f;
    
    private Tween _tween;

    private Vector3[] _path;
    
    public override void Construct()
    {
      _path = new Vector3[]
      {
        new(-radius, 0),                         // Start left
        new(-radius/2, radius),                    // Top left
        new(0, 0),                               // Center
        new(radius/2, -radius),                    // Bottom right
        new(radius, 0),                          // Right
        new(radius/2, radius),                     // Top right
        new(0, 0),                               // Center again
        new(-radius/2, -radius),                   // Bottom left
        new(-radius, 0)                          // Start left
      };
      
      base.Construct();
      
      HandImage.rectTransform.localPosition = _path[0];
    }

    protected override void PlayAnimation()
    {
      _tween = HandImage.rectTransform
        .DOLocalPath(_path, animationDuration, PathType.CatmullRom)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
      _tween.Kill(true);
    }
  }
}