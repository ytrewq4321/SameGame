using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Input;
using DG.Tweening;
using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using static Unity.Mathematics.math;

namespace Assets.CodeBase.Logic
{
    public class GridView : MonoBehaviour
    {
        public event Action GameOver; 
        public event Action WinLevel; 

        [SerializeField] private GameObject[] _tilePrefabs; 
        [SerializeField] private ShiftDirection _shiftDirection; 
        [SerializeField] private float _dropSpeed;
        [SerializeField] private float _shiftSpeed;
        private GridController _gridController;
        private Grid2D<Tile> _tilesView;
        private float2  _tileOffset;
        private ITileFactory _tileFactory;
        private IInputService _inputInputService;
        private bool _isBusy;

        [Inject]
        public void Construct(GridController gridController,ITileFactory tileFactory, IInputService inputService)
        { 
            _gridController = gridController;
            _tileFactory = tileFactory;
            _inputInputService = inputService;
            inputService.ClickPosition += OnClickTile;
        }

        public void Init(int2 gridSize, int objectCount, ShiftDirection shiftDirection)
        {
            _gridController.CreateGrid(gridSize, shiftDirection);
            _gridController.FillGrid(objectCount);

            CreateGrid();
            _isBusy = false;
        }

        private void OnClickTile(Vector3 mousePosition)
        {
            if (_isBusy) return;

            var clickPosition = ScreenToTileSpace(mousePosition);
            var startCoordinate = (int2)floor(clickPosition);

            if (FindMathces(startCoordinate))
            {
                _isBusy = true;
                HandingMathces();

                var dropTween = DropTiles();
                dropTween.OnComplete(() =>
                {
                    var shitTween = ShiftTiles().Play().OnComplete(() =>
                    {
                        if (IsWinLevel())
                        {
                            WinLevel?.Invoke();
                            return;
                        }
                        else if (!IsMoveExist())
                        {
                            GameOver?.Invoke();
                            return;
                        }
                       
                        _isBusy = false;
                    });
                });
            }
        }

        private bool IsMoveExist()
        {
            return _gridController.IsMoveExist();
        }

        private bool IsWinLevel()
        {
            return _gridController.IsWinLevel();
        }

        public Tween DropTiles()
        {
            _gridController.DropTiles();
            Sequence dropSequence = DOTween.Sequence();
            for (int i = 0; i < _gridController.DroppedTiles.Count; i++)
            {
                TileDrop tileDrop = _gridController.DroppedTiles[i];
                Tile tile = _tilesView[tileDrop.Coordinates.x, tileDrop.FromY];
                _tilesView[tileDrop.Coordinates] = tile;
                dropSequence.Join(tile.Fall(tileDrop.Coordinates.y + _tileOffset.y, _dropSpeed));
            }
            return dropSequence;
        }

        public Tween ShiftTiles()
        {
            if (_shiftDirection == ShiftDirection.Left)
                _gridController.ShiftTileLeft();
            else
                _gridController.ShiftTileRight();

            Sequence shiftSequence = DOTween.Sequence();
            for (int i = 0; i < _gridController.ShiftedTiles.Count; i++)
            {
                TileShift tileShift = _gridController.ShiftedTiles[i];
                Tile tile = _tilesView[tileShift.FromX, tileShift.Coordinates.y];
                _tilesView[tileShift.Coordinates] = tile;

                shiftSequence.Join(tile.Shift(tileShift.Coordinates.x + _tileOffset.x, _shiftSpeed));
            }
            return shiftSequence;
        }

        private bool FindMathces(int2 startCoordinate)
        {
            return _gridController.FindMatchs(startCoordinate);
        }

        private void HandingMathces()
        {    
            _gridController.HandlingMatches();
            for (int i = 0; i < _gridController.ConnectedTiles.Count; i++)
            {
                int2 coordinate = _gridController.ConnectedTiles[i];
                _tilesView[coordinate].Despawn();
            }
        }

        public void CreateGrid()
        {
            DespawnGrid();

            _tileOffset = -0.5f * (float2)(_gridController.GridSize - 1);
            _tilesView = new Grid2D<Tile>(_gridController.GridSize);

            for (int y = 0; y < _tilesView.Size.y; y++)
            {
                for (int x = 0; x < _tilesView.Size.x; x++)
                {
                    _tilesView[x,y]= SpawnTile(_gridController.GetTileState(x, y), x, y);
                }
            }
        }

        private void DespawnGrid()
        {
            for (int y = 0; y < _tilesView.Size.y; y++)
            {
                for (int x = 0; x < _tilesView.Size.x; x++)
                {
                    if (_tilesView[x, y] != null)
                        _tilesView[x, y].Despawn();
                }
            }
        }

        private Tile SpawnTile(TileState tileState, float x, float y)
        {
            GameObject currentPrefabTile = _tilePrefabs[(int)tileState-1];
            Vector3 position = new Vector3(x + _tileOffset.x, y + _tileOffset.y);


            return _tileFactory.CreateTile(currentPrefabTile,position);
        }

        private float2 ScreenToTileSpace(Vector3 screenPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            Vector3 p = ray.origin - ray.direction * (ray.origin.z / ray.direction.z);
            return new float2(p.x - _tileOffset.x + 0.5f, p.y - _tileOffset.y + 0.5f);
        }

        private void OnDestroy()
        {
            _inputInputService.ClickPosition -= OnClickTile;
        }
    }
}
