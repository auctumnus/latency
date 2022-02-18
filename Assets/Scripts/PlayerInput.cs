using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Scenes.Actions;
using UnityEngine;

namespace Scenes
{
    public class PlayerInput : MonoBehaviour
    {
        private List<(int, int)> _playerPos;
        public int currentPlayer;
        private int _numPlayers;
        private List<Command> _queue;
        private int _delay;
        private Command _table;

        private void Update()
        {
            var o = Orchestrator.Instance;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                o.NewTurn();
            }
            if (Input.GetMouseButtonDown(1)) // right click opens menu and confirms orders, left click will trigger buttons
            {
                int x = (int) Input.mousePosition.x;
                int y = (int) Input.mousePosition.y;
                if (_table != null)
                {
                    _table.payload.Specify(x, y);
                    _queue.Add(_table);
                }
                else
                {
                    ClickMenu menu = Orchestrator.Instance.menu; // gets the menu
                    menu.gameObject.SetActive(true); // sets it to be active
                    menu.transform.position = Input.mousePosition; // changes its position. 
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
            if (currentPlayer == _numPlayers)
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
                _queue.Add(new Command(x, y, Math.Max(MinimumLatency(x, y), _delay), payload));
            }
            else
            {
                _table = new Command(x, y, Math.Max(MinimumLatency(x, y), _delay), payload);   
            }
        }
    }
}