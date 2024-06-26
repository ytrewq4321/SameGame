using Assets.CodeBase.UI.Elements;
using Assets.CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Popup _winPopup;
        [SerializeField] private Popup _losePopup;
        [SerializeField] private RestartButton _resetButton;

        public Popup WinPopup =>_winPopup;
        public Popup LosePopup => _losePopup;
        public RestartButton RestartButton => _resetButton;

        public void OpenWinPopup()
        {
            _winPopup.gameObject.SetActive(true);
        }

        public void OpenLosePopup()
        {
            _losePopup.gameObject.SetActive(true);
        }

    }
}