using CodeBase.Logic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.CodeBase.UI.Elements
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreValue;
        private ScoreData _scoreData;

        [Inject]
        public void Construct(ScoreData scoreData)
        {
            _scoreData = scoreData;
            _scoreData.ScoreChanged += UpdateScore;

            UpdateScore();
        }

        private void UpdateScore()
        {
            _scoreValue.text = $"{_scoreData.CurrentScore}";
        }
    }
}
