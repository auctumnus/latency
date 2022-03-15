using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Scenes.Actions;
using UnityEngine;

namespace Scenes
{
    public class PlayerInput : MonoBehaviour
    {
        private List<(int, int)> _playerPos = new();
        public int currentPlayer;
        [SerializeField] private int numPlayers;
        private List<Command> _queue = new();
        public int delay;
        private Command _table;

        [SerializeField] private Camera camera;

        private void Start()
        {
            _playerPos.Add((5, 3));
        }
        private void Update()
        {
            var o = Orchestrator.Instance;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                o.NewTurn();
            }

            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                delay += 1;
                Orchestrator.Instance.Rerender();
            } else if (Input.GetKeyDown(KeyCode.LeftBracket) && delay > 0)
            {
                delay -= 1;
                Orchestrator.Instance.Rerender();
            }
            if (Input.GetMouseButtonDown(1)) // right click opens menu and confirms orders, left click will trigger buttons
            {
                Vector2 position = camera.ScreenToWorldPoint(Input.mousePosition);
                int x = (int) Math.Round(position.x);
                int y = (int) Math.Round(position.y);
                Debug.Log("Mouse position: (" + x + ", " + y + ")");
                if (_table != null)
                {
                    _table.payload.Specify(x, y);
                    _queue.Add(_table);
                    _table = null;
                }
                else
                {
                    ClickMenu menu = Orchestrator.Instance.menu; // gets the menu
                    menu.gameObject.SetActive(true); // sets it to be active
                    var transform1 = menu.transform;
                    Vector2 size = ((RectTransform) transform1).rect.size / 2 * transform1.localScale.x;
                    size.y = -size.y;
                    transform1.position = ((Vector2) Input.mousePosition) +  size;
                    //menu.transform.position = Input.mousePosition; // changes its position. 
                }
            }
        }
        
        public int MinimumLatency(int x, int y)
        {
            return MinimumLatency(x, y, _playerPos[currentPlayer].Item1, _playerPos[currentPlayer].Item2);
        }

        public int MinimumLatency(int x, int y, int px, int py)
        {
            return Math.Max(Math.Abs(x - px), Math.Abs(y - py));
        }
        public void SwitchControl()
        {
            currentPlayer++;
            if (currentPlayer == numPlayers)
            {
                currentPlayer = 0;
            }
            foreach (Command cmd in _queue)
            {
                Orchestrator.Instance.commandProcessor.Add(cmd);   
            }
            _queue = new List<Command>();
            if (currentPlayer == 0)
            {
                Orchestrator.Instance.NewTurn();
            }
        }
        public void StartOrder(int x, int y, Action payload)
        {
            if (payload is DefendAction)
            {
                _queue.Add(new Command(x, y, Math.Max(MinimumLatency(x, y), delay), payload));
            }
            else
            {
                _table = new Command(x, y, Math.Max(MinimumLatency(x, y), delay), payload);  
            }
            Rerender();
        }

        public void Rerender()
        {
            for (int i = 0; i < _queue.Count; i++)
            {
                _queue[i].Render();
            }
        }
    }
}