using System;
using UnityEngine;

namespace Scenes
{
    public class PlayerInput : MonoBehaviour
    {
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
    }
}