using CodeBase.Infrastructure.Services.Input;
using CodeBase.Logic;
using CodeBase.StaticData;
using System.Collections.Generic;

namespace Assets.CodeBase.Logic
{
    public class LevelController
    {
        private int _currentLevel=1;
        private Dictionary<int, LevelStaticData> _levels;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly GridView _gridView;
        private readonly ScoreData _scoreData;

        public LevelController (GridView gridView, IStaticDataService staticDataService, IInputService inputService, ScoreData scoreData, LoadingCurtain loadingCurtain)
        {
            _gridView = gridView;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _scoreData = scoreData;
        }

        public void NextLevel()
        {
            var levelData = _staticDataService.ForLevel(_currentLevel + 1);
            if (levelData == null)
                levelData = _staticDataService.ForLevel(1);

            _gridView.Init(levelData.GridSize, levelData.ObjectCount, levelData.ShiftDirection);
        }

        public void ResetGame()
        {
            var levelData = _staticDataService.ForLevel(_currentLevel);
            _gridView.Init(levelData.GridSize, levelData.ObjectCount, levelData.ShiftDirection);
            _scoreData.ResetScore();
        }
    }
}
