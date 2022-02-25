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
        public PlayerInput player;
        public ClickMenu menu;

        public Color[] colors; // colors, corresponding to delay
        public int colorBias = 10; // the bias
        public void NewTurn()
        {
            commandProcessor.NewTurn();
        }
    }
}