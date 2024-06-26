using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.CodeBase.UI.Windows
{
    public class LoseWindow : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        public void AddListener(UnityAction action)
        {
            _restartButton.onClick.AddListener(action);
        }

        public void RemoveListener()
        {
            _restartButton.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            RemoveListener();
        }
    }
}
