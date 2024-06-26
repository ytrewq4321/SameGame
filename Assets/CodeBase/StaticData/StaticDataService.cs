using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticdataLevels = "StaticData/Levels";
        private Dictionary<int, LevelStaticData> _levels;

        public void Load()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(StaticdataLevels)
                .ToDictionary(x => x.LevelNumber, x => x);
        }

        public LevelStaticData ForLevel(int level)
        {
            return _levels.TryGetValue(level, out LevelStaticData levelStaticData) ? levelStaticData : null;
        }
    }
}