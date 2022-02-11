using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes
{
    public class PlayerInput : MonoBehaviour
    {
        private int playerX;
        private int playerY;
        private List<Command> queue;
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
            return MinimumLatency(x, y, playerX, playerY);
        }

        public int MinimumLatency(int x, int y, int px, int py)
        {
            return Math.Max(Math.Abs(x - px), Math.Abs(y - py));
        }

        public void NewTurn()
        {
            foreach (Command cmd in queue)
            {
                Orchestrator.Instance.commandProcessor.Add(cmd);   
            }
            queue = new List<Command>();
        }
    }
}