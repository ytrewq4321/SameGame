using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.CodeBase.UI.Elements
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void AddListener(UnityAction action )
        {
            _button.onClick.AddListener( action );
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}