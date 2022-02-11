using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes
{
    public class PlayerInput : MonoBehaviour
    {
        private List<(int, int)> _playerPos;
        private int _currentPlayer;
        private int _numPlayers;
        private List<Command> _queue;
        private void Update()
        {
            var o = Orchestrator.Instance;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                o.NewTurn();
            }
            if (Input.GetMouseButtonDown(0))
            {
                int x = (int) Input.mousePosition.x;
                int y = (int) Input.mousePosition.y;

                var gridController = o.gridController;
                var unit = gridController.GETUnit(x, y);
                
                if (unit && unit.owner == o.currentPlayer)
                {
                    
                }
                else
                {
                    // prompt to create a moveaction if we have a selected unit
                }
            }
        }

        public int MinimumLatency(int x, int y)
        {
            return MinimumLatency(x, y, _playerPos[_currentPlayer].Item1, _playerPos[_currentPlayer].Item2);
        }

        public int MinimumLatency(int x, int y, int px, int py)
        {
            return Math.Max(Math.Abs(x - px), Math.Abs(y - py));
        }
        public void SwitchControl()
        {
            _currentPlayer++;
            if (_currentPlayer == _numPlayers)
            {
                _currentPlayer = 0;
            }
            foreach (Command cmd in _queue)
            {
                Orchestrator.Instance.commandProcessor.Add(cmd);   
            }
            _queue = new List<Command>();
            if (_currentPlayer == 0)
            {
                Orchestrator.Instance.NewTurn();
            }
        }

        public void MakeOrder(int x, int y, int delay, Action payload)
        {
            if (delay < MinimumLatency(x, y))
            {
                // FAIL
            }
            else
            {
                _queue.Add(new Command(x, y, delay, payload));
            }
        }
    }
}