using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Scenes.Actions;
using UnityEngine;

namespace Scenes
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private GameObject messagePrefab;
        private List<(int, int)> _playerPos = new();
        public int currentPlayer;
        [SerializeField] private int numPlayers;
        public List<Command> queue = new();
        public int delay;
        private Command _table;
        public Icons icons;
        private List<int> _placeCD = new();
        private List<GameObject> messages = new();
        public const int PMOVE_CD = 10;

        [SerializeField] private Camera camera;
        private bool willAcceptInput;

        public void disableInput() {
            willAcceptInput = false;
        }

        private void Start()
        {
            _playerPos.Add((9, 0));
            _placeCD.Add(PMOVE_CD);
            _playerPos.Add((9, 18));
            _placeCD.Add(PMOVE_CD);
            Orchestrator.Instance.commandProcessor.Init(2);
            Orchestrator.Instance.liaison.SetPlayers(2);
            messages.Add(Orchestrator.Instance.liaison.Create(messagePrefab));
            Orchestrator.Instance.liaison.currentPlayer = 1;
            messages.Add(Orchestrator.Instance.liaison.Create(messagePrefab));
            Orchestrator.Instance.liaison.currentPlayer = 0;
            foreach (GameObject message in messages)
            {
                message.SetActive(false);
            }
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

            if (Input.GetKeyDown(KeyCode.P) && _placeCD[currentPlayer] <= 0)
            {
                Vector2 position = camera.ScreenToWorldPoint(Input.mousePosition);
                int x = (int) Math.Round(position.x);
                int y = (int) Math.Round(position.y);
                _playerPos[currentPlayer] = (x, y);
                _placeCD[currentPlayer] = PMOVE_CD;
                messages[currentPlayer].SetActive(false);
                Rerender();
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
                }
            }
        }
        
        public int MinimumLatency(int x, int y)
        {
            return MinimumLatency(x, y, _playerPos[currentPlayer].Item1, _playerPos[currentPlayer].Item2);
        }

        public int MinimumLatency(int x, int y, int px, int py)
        {
            return (Math.Max(Math.Abs(x - px), Math.Abs(y - py)) + 1 ) / 2;
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
                for (int i = 0; i < _placeCD.Count; i++)
                    _placeCD[i] -= 1;
                Orchestrator.Instance.NewTurn();
            }
            icons.Clear();
            delay = 0;
            for (int i = 0; i < _placeCD.Count; i++)
            {
                if(_placeCD[i] <= 0)
                    messages[i].SetActive(true);
            }
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