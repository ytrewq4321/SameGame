using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.UI.Windows
{
    public class WinWindow
    {
        [SerializeField] private Button _nextLevelButton;

        public void AddListener(UnityAction action)
        {
            _nextLevelButton.onClick.AddListener(action);
        }

        public void RemoveListener()
        {
            _nextLevelButton.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            RemoveListener();
        }
    }
}
