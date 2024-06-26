using System;
using Unity.Mathematics;

namespace Assets.CodeBase.Logic
{
    public struct Grid2D<T>
    {
        public T[] _tiles;
        private int _tileCount;
        private int2 _size;

        public int TileCount => _tileCount;
        public int2 Size => _size;
        public int SizeX => _size.x;
        public int SizeY => _size.y;


        public Grid2D(int2 size)
        { 
            _size = size;
            _tiles = new T[size.x*size.y];
            _tileCount = size.x * size.y;
        }

        public bool AreValidCoordinates(int2 coord)
        {
            return 0<=coord.x && coord.x<_size.x && 0<=coord.y&&coord.y<_size.y; 
        }

        public T this[int x,int y]
        {
            get => _tiles[y*_size.x+x];
            set => _tiles[y * _size.x + x] = value;
        }
        public T this[int2 coord]
        {
            get => _tiles[coord.y * _size.x + coord.x];
            set => _tiles[coord.y * _size.x + coord.x] = value;
        }
    }
}
