using Common.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIService.Presenters
{
  public sealed class TapPresenter : HandPresenter
  {
    [SerializeField, Range(0f, 1f)] private float tapScale;
    
    private UniversalUtils _utils;
    
    private Sprite _tapOn;
    private Sprite _tapOff;

    private bool _isTapOn;

    public override void Construct()
    {
      _utils = new UniversalUtils();
      
      _tapOn = _utils.ResourcesLoader.LoadSprite("tap_on");
      _tapOff = _utils.ResourcesLoader.LoadSprite("tap_off");
      
      base.Construct();
    }

    protected override void PlayAnimation()
    {
      Animation = DOTween.Sequence();

      Animation
        .AppendCallback(ChangeSprite)
        .Append(HandImage.rectTransform.DOScale(Vector3.one * tapScale, animationDuration))
        .AppendCallback(ChangeSprite)
        .Append(HandImage.rectTransform.DOScale(Vector3.one, animationDuration))
        .SetLoops(-1);
    }
    
    private void ChangeSprite()
    {
      if (!_isTapOn)
      {
        HandImage.sprite = _tapOn;
        _isTapOn = true;
      }
      else
      {
        HandImage.sprite = _tapOff;
        _isTapOn = false;
      }
    }
  }
}