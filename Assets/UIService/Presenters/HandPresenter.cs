using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIService.Presenters
{
  public abstract class HandPresenter : MonoBehaviour
  {
    [SerializeField, Range(0f, 3f)] protected float animationDuration;
    protected Image HandImage;
    protected Sequence Animation;

    public virtual void Construct()
    {
      HandImage = GetComponentInChildren<Image>();
      PlayAnimation();
    }

    protected abstract void PlayAnimation();

    private void OnDestroy()
    {
      Animation?.Kill(true);
    }
  }
}