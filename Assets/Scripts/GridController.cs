using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Scenes
{
    public class GridController: MonoBehaviour
    {
        public const int GridSize = 18;

        private readonly Unit[,] _units = new Unit[GridSize, GridSize];
        private readonly List<Unit> _unitQueue = new();
        private readonly Tile[,] _tiles = new Tile[GridSize, GridSize];
        public GameObject prefab;
        
        public Unit GETUnit(int x, int y)
        { 
            return _units[x, y];
        }

        public void Start()
        {
            Vector3 v = transform.position;
            int startX = (int) v.x;
            int startY = (int) v.y;
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    SpriteRenderer sr = Instantiate(prefab, new Vector3(x + startX, y + startY), Quaternion.Euler(0, 0, 0))
                        .GetComponent<SpriteRenderer>();
                    _tiles[x,y] = new Tile(x, y, sr);
                }
            }
        }

        public void Rerender()
        {
            for (int x = 0; x < GridSize; x++)
                for (int y = 0; y < GridSize; y++)
                    _tiles[x, y].Redraw();
            bool[] toRemove = new bool[_unitQueue.Count];
            for (int x = 0; x < _unitQueue.Count; x++)
                if (_unitQueue[x] == null)
                    toRemove[x] = true;
                else
                    _unitQueue[x].Rerender();
            for (int x = toRemove.Length - 1; x >= 0; x--)
                if(toRemove[x])
                    _unitQueue.RemoveAt(x);
        }

        public void SetUnit(int x, int y, Unit unit)
        {
            if (_units[x, y] != null)
            {
                Destroy(_units[x, y].gameObject);
            }
            Debug.Log($"Moving unit's position to {x}, {y}");
            _units[x, y] = unit;
            unit.x = x;
            unit.y = y;
        }

        public void AddUnit(int x, int y, Unit unit)
        {
            _unitQueue.Add(unit);
            SetUnit(x, y, unit);
        }
        public void ClearUnit(int x, int y)
        {
            _units[x, y] = null;
        }

        public void MoveUnit(int x1, int y1, int x2, int y2)
        {
            Debug.Log($"Moving unit on {x1}, {y1} to {x2}, {y2}");
            Unit unit = GETUnit(x1, y1);
            SetUnit(x2, y2, unit);
            ClearUnit(x1, y1);
        }
    }
}