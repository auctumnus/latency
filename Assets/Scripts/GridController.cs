using UnityEngine;

namespace Scenes
{
    public class GridController
    {
        public const int GRID_SIZE = 15;

        private readonly Unit[,] units = new Unit[GRID_SIZE, GRID_SIZE];

        public Unit getUnit(int x, int y)
        { 
            return units[x, y];
        }

        public void setUnit(int x, int y, Unit unit)
        {
            units[x, y] = unit;
            unit.x = x;
            unit.y = y;
        }

        public void clearUnit(int x, int y)
        {
            units[x, y] = null;
        }

        public void moveUnit(int x1, int y1, int x2, int y2)
        {
            var unit = getUnit(x1, y1);
            setUnit(x2, y2, unit);
            clearUnit(x1, y1);
        }
    }
}