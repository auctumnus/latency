using System;
using UnityEngine;

namespace Scenes
{
    public class Orchestrator : MonoBehaviour
    {
        public static Orchestrator instance;

        private void OnEnable()
        {
            instance = this;
        }

        public GridController gridController     = new GridController();
        public CommandProcessor commandProcessor = new CommandProcessor();

        public int currentPlayer = 0;

        public void NewTurn()
        {
            commandProcessor.NewTurn(gridController);
        }
    }
}