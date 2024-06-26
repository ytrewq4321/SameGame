using System;

namespace CodeBase.Logic
{
    public class ScoreData
    {
        public int CurrentScore { get; private set; }
        public event Action ScoreChanged;

        public void AddScore(int score)
        {
            CurrentScore += score;
            ScoreChanged?.Invoke();
        }

        public void ResetScore()
        {
            CurrentScore = 0;
            ScoreChanged?.Invoke();
        }
    }
}