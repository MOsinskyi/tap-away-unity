using LevelService.Scripts;
using TMPro;
using UnityEngine;

namespace UIService.Presenters
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class CurrentLevelPresenter : MonoBehaviour
    {
        private Level _level;
        private TMP_Text _text;
        
        public void Construct(Level currentLevel)
        {
            _text = GetComponent<TMP_Text>();
            _level = currentLevel;
            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = _level.LevelName;
        }
    }
}