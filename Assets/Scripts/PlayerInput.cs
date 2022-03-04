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

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                delay += 1;
                Orchestrator.Instance.gridController.Rerender();
                Debug.Log("New delay: " + delay);
            } else if (Input.GetKeyDown(KeyCode.DownArrow) && delay > 0)
            {
                delay -= 1;
                Orchestrator.Instance.gridController.Rerender();
                Debug.Log("New delay: " + delay);
            }
            if (Input.GetMouseButtonDown(1)) // right click opens menu and confirms orders, left click will trigger buttons
            {
                int x = (int) Input.mousePosition.x;
                int y = (int) Input.mousePosition.y;
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
                    Debug.Log("ACTIVE");
                    menu.transform.position = new Vector2(x, y);
                    //menu.transform.position = Input.mousePosition; // changes its position. 
                }
            }
        }
        
        public int MinimumLatency(int x, int y)
        {
            Debug.Log("Current player: " + currentPlayer);
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
                Debug.Log("Start!!!");
                _queue.Add(new Command(x, y, Math.Max(MinimumLatency(x, y), delay), payload));
                Debug.Log("End!!!");
            }
            else
            {
                Debug.Log("Start!!!");
                _table = new Command(x, y, Math.Max(MinimumLatency(x, y), delay), payload);  
                Debug.Log("End!!!"); 
            }
        }
    }
}