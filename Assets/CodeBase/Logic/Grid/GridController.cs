using CodeBase.Logic;
using System.Collections.Generic;
using Unity.Mathematics;

namespace Assets.CodeBase.Logic
{
    public class GridController
    {
        public TileState GetTileState(int x, int y) => _grid[x, y];
        public TileState GetTileState(int2 c) => _grid[c];
        public int2 GridSize => _gridSize;
        public List<TileDrop> DroppedTiles { get; private set; } = new();
        public List<TileShift> ShiftedTiles { get; private set; } = new();
        public List<int2> ConnectedTiles { get; private set; } = new();

        private readonly ScoreData _scoreData;
        private ShiftDirection _shiftDirection;
        private Grid2D<TileState> _grid;
        private int2[] _directions;
        private int2 _gridSize;
        private int _objectCount;
        private int _tilesCount;

        public GridController(ScoreData scoreData)
        {
            _scoreData = scoreData;
        }

        public void CreateGrid(int2 gridSize, ShiftDirection shiftDirection)
        {
            _gridSize = gridSize;
            _grid = new Grid2D<TileState>(_gridSize);
            _tilesCount = gridSize.x*gridSize.y;
            _shiftDirection = shiftDirection;

            _directions = new int2[]
            {
                new(1, 0),
                new(-1, 0),
                new(0, 1),
                new(0, -1)
            };
        }

        public void FillGrid(int objectCount)
        {
            _objectCount = objectCount;
            for (int y = 0; y < _gridSize.y; y++)
            {
                for (int x = 0; x < _gridSize.x; x++)
                {
                    _grid[x, y] = (TileState)UnityEngine.Random.Range(1, _objectCount+1);
                }
            }
        }

        public void DropTiles()
        {
            DroppedTiles.Clear();

            for (int x = 0; x < _gridSize.x; x++)
            {
                int holeCount = 0;
                for (int y = 0; y < _gridSize.y; y++)
                {
                    if (_grid[x, y] == TileState.None)
                    {
                        holeCount+=1;
                    }
                    else if(holeCount>0)
                    {
                        _grid[x, y-holeCount] = _grid[x, y];
                        _grid[x, y] = TileState.None;
                        DroppedTiles.Add(new TileDrop(x, y - holeCount, holeCount));
                    }
                }
            }
        }

        public void ShiftTileLeft()
        {
            ShiftedTiles.Clear();
            bool columnIsEmpty = false;
            int emptyColumnIndex = 0;

            for (int x = 0; x < _gridSize.x; x++)
            {
                if (_grid[x, 0] == TileState.None)
                {
                    columnIsEmpty = true;
                    emptyColumnIndex = x;
                    break;
                }
            }

            if (columnIsEmpty)
            {
                int holeCount = 0;
                for (int x = emptyColumnIndex; x <_gridSize.x; x++)
                {
                    if (_grid[x, 0] == TileState.None)
                    {
                        holeCount++;
                    }
                    else if (holeCount > 0)
                    {
                        for (int y = 0; y < _gridSize.y; y++)
                        {
                            _grid[x - holeCount, y] = _grid[x, y];
                            _grid[x, y] = TileState.None;
                            ShiftedTiles.Add(new TileShift(x - holeCount, y, holeCount));
                        }
                    }
                }
            }
        }

        public void ShiftTileRight()
        {
            ShiftedTiles.Clear();
            bool columnIsEmpty = false;
            int emptyColumnIndex = 0;

            for (int x = _gridSize.x-1; x> 0; x--)
            {
                if (_grid[x, 0] == TileState.None)
                {
                    columnIsEmpty = true;
                    emptyColumnIndex = x;
                    break;
                }
            }

            if (columnIsEmpty)
            {
                int holeCount = 0;
                for (int x = emptyColumnIndex; x >= 0; x--)
                {
                    if (_grid[x, 0] == TileState.None)
                    {
                        holeCount++;
                    }
                    else if (holeCount > 0)
                    {
                        for (int y = 0; y < _gridSize.y; y++)
                        {
                            _grid[x + holeCount, y] = _grid[x, y];
                            _grid[x, y] = TileState.None;
                            ShiftedTiles.Add(new TileShift(x + holeCount, y, -holeCount));
                        }
                    }
                }
            }
        }

        public void HandlingMatches()
        {   
            for (int i = 0; i < ConnectedTiles.Count; i++)
            {
                _grid[ConnectedTiles[i]] = TileState.None;
                _tilesCount--;
            }
            var score = (ConnectedTiles.Count - 1) * (ConnectedTiles.Count - 1);
            _scoreData.AddScore(score);
        }

        public bool FindMatchs(int2 startCoordinate)
        {
            if(!_grid.AreValidCoordinates(startCoordinate)) return false;

            TileState targetState = GetTileState(startCoordinate);
            if (targetState == TileState.None) return false;

            ConnectedTiles.Clear();

            Queue<int2> toExplore = new Queue<int2>();
            HashSet<int2> visited = new HashSet<int2>();

            toExplore.Enqueue(startCoordinate);
            visited.Add(startCoordinate);

            while (toExplore.Count > 0)
            {
                int2 current = toExplore.Dequeue();
                ConnectedTiles.Add(current);

                foreach (var direction in _directions)
                {
                    int2 neighbor = current + direction;
                    if (_grid.AreValidCoordinates(neighbor) && !visited.Contains(neighbor) && GetTileState(neighbor) == targetState)
                    {
                        toExplore.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }

            if (ConnectedTiles.Count > 1)
                return true;
            else
                return false;
        }

        public bool IsMoveExist()
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    int2 startCoordinate = new int2(x, y);
                    TileState targetState = GetTileState(startCoordinate);

                    if (targetState == TileState.None) continue;

                    foreach (var direction in _directions)
                    {
                        int2 neighbor = startCoordinate + direction;
                        if (_grid.AreValidCoordinates(neighbor) && GetTileState(neighbor) == targetState)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IsWinLevel()
        {
            return _tilesCount == 0;
        }
    }
}
