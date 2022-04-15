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
        public List<Command> queue = new();
        public int delay;
        private Command _table;
        public Icons icons;

        [SerializeField] private Camera camera;
        private bool willAcceptInput;
        private void Start()
        {
            _playerPos.Add((9, 0));
            _playerPos.Add((9, 18));
            Orchestrator.Instance.commandProcessor.Init(2);
            Orchestrator.Instance.liaison.SetPlayers(2);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                willAcceptInput = true;
                Orchestrator.Instance.panel.SetActive(false);
            }
            
            if (!willAcceptInput)
                return;
            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                delay += 1;
                Orchestrator.Instance.Rerender();
            } else if (Input.GetKeyDown(KeyCode.LeftBracket) && delay > 0)
            { 
                delay -= 1; 
                Orchestrator.Instance.Rerender();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SwitchControl();
            }
            if (Input.GetMouseButtonDown(1)) // right click opens menu and confirms orders, left click will trigger buttons
            {
                
                Vector2 position = camera.ScreenToWorldPoint(Input.mousePosition);
                int x = (int) Math.Round(position.x);
                int y = (int) Math.Round(position.y);
                
                if (_table != null)
                {
                    Debug.Log("Specifying");
                    _table.payload.Specify(x, y);
                    queue.Add(_table);
                    icons.Add(_table);
                    _table = null;
                    Orchestrator.Instance.Rerender();
                }
                else
                {
                    Debug.Log("Putting down menu...");
                    ClickMenu menu = Orchestrator.Instance.menu; // gets the menu
                    menu.x = x;
                    menu.y = y;
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
            willAcceptInput = false;
            Orchestrator.Instance.panel.SetActive(true);
            Orchestrator.Instance.liaison.NextPlayer();
            currentPlayer++;
            if (currentPlayer == numPlayers)
            {
                currentPlayer = 0;
            }
            foreach (Command cmd in queue)
            {
                if(cmd != null)
                    Orchestrator.Instance.commandProcessor.Add(cmd);   
            }
            queue = new List<Command>();
            if (currentPlayer == 0)
            {
                Orchestrator.Instance.NewTurn();
            }
            icons.Clear();
            delay = 0;
            Orchestrator.Instance.Rerender();
        }
        public void StartOrder(int x, int y, Action payload)
        {
            if (payload is DefendAction)
            {
                Command c = new Command(x, y, Math.Max(MinimumLatency(x, y), delay), payload, currentPlayer);
                queue.Add(c);
                icons.Add(c);
            }
            else
            {
                _table = new Command(x, y, Math.Max(MinimumLatency(x, y), delay), payload, currentPlayer);  
            }
            Rerender();
        }

        public void Rerender()
        {
            foreach (Command cmd in queue)
            {
                if(cmd != null)
                    cmd.Render();
            }
        }
    }
}