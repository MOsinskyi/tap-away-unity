using NaughtyAttributes;
using UIService.Presenters;
using UnityEngine;

namespace LevelService.Scripts
{
  public class TutorialLevel : Level
  {
    [SerializeField] private bool hasTutorialMessage;

    [SerializeField, ShowIf(nameof(hasTutorialMessage))]
    private TutorialMessagePresenter tutorialMessagePresenter;

    public void Construct(RectTransform tutorialScreen)
    {
      base.Construct();

      if (!hasTutorialMessage) tutorialMessagePresenter = null;

      if (tutorialMessagePresenter)
      {
        var instance = Instantiate(tutorialMessagePresenter, tutorialScreen);
        instance.Construct();
      }
    }
  }
}