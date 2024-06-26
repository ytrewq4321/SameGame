using Assets.CodeBase.Logic;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelData",menuName ="StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public int LevelNumber =>_levelNumber;
        public int2 GridSize=>_gridSize;
        public int ObjectCount=> _objectCount;
        public ShiftDirection ShiftDirection => _shiftDiection;

        [SerializeField] private int2 _gridSize;
        [SerializeField] private int _objectCount; 
        [SerializeField] private int _levelNumber; 
        [SerializeField] private ShiftDirection _shiftDiection; 

    }

}