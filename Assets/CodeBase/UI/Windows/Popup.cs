using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.UI.Windows
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void Awake()
        {
            _button.onClick.AddListener(()=>gameObject.SetActive(false));
        }

        public void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveListener()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            RemoveListener();
        }
    }
}
