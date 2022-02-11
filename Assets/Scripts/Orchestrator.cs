using System;
using UnityEngine;

namespace Scenes
{
    public class Orchestrator : MonoBehaviour
    {
        public static Orchestrator Instance;

        private void OnEnable()
        {
            Instance = this;
        }

        public GridController gridController     = new GridController();
        public CommandProcessor commandProcessor = new CommandProcessor();
        public PlayerInput p1;
        public PlayerInput p2;

        public int currentPlayer = 0;

        public void NewTurn()
        {
            p1.NewTurn();
            p2.NewTurn();
            commandProcessor.NewTurn();
        }
    }
}