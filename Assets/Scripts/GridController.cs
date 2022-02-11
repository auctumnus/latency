using UnityEngine;

namespace Scenes
{
    public class GridController
    {
        public const int GridSize = 15;

        private readonly Unit[,] _units = new Unit[GridSize, GridSize];

        public Unit GETUnit(int x, int y)
        { 
            return _units[x, y];
        }

        public void SetUnit(int x, int y, Unit unit)
        {
            _units[x, y] = unit;
            unit.x = x;
            unit.y = y;
        }

        public void ClearUnit(int x, int y)
        {
            _units[x, y] = null;
        }

        public void MoveUnit(int x1, int y1, int x2, int y2)
        {
            var unit = GETUnit(x1, y1);
            SetUnit(x2, y2, unit);
            ClearUnit(x1, y1);
        }
    }
}