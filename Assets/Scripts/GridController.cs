using UnityEditor.SceneManagement;
using UnityEngine;

namespace Scenes
{
    public class GridController: MonoBehaviour
    {
        public const int GridSize = 15;

        private readonly Unit[,] _units = new Unit[GridSize, GridSize];
        private readonly Tile[,] _tiles = new Tile[GridSize, GridSize];
        public GameObject prefab;
        
        public Unit GETUnit(int x, int y)
        { 
            return _units[x, y];
        }

        public void Start()
        {
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    SpriteRenderer sr = Instantiate(prefab, new Vector3(x, y), Quaternion.Euler(0, 0, 0), transform)
                        .GetComponent<SpriteRenderer>();
                    _tiles[x,y] = new Tile(x, y, sr);
                }
            }
        }

        public void Rerender()
        {
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    _tiles[x, y].Redraw();
                }
            }
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
            Unit unit = GETUnit(x1, y1);
            SetUnit(x2, y2, unit);
            ClearUnit(x1, y1);
        }
    }
}