using Unity.Mathematics;

namespace Assets.CodeBase.Logic
{
    public struct TileDrop
    {
        public int2 Coordinates;
        public int FromY;

        public TileDrop(int x, int y, int distance)
        {
            Coordinates.x = x;
            Coordinates.y = y;
            FromY = y+distance;
        }
    }

    public struct TileShift
    {
        public int2 Coordinates;
        public int FromX;

        public TileShift(int x, int y, int distance)
        {
            Coordinates.x = x;
            Coordinates.y = y;
            FromX = x + distance;
        }
    }
}