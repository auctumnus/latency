using System;
using Scenes.Actions;
using UnityEngine;
using UnityEngine.UIElements;

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
        public GameObject orderQueue;
        public Icons icons;
        public Liaison liaison;
        public int willUpdate;
        public GameObject panel;

        public Color[] colors; // colors, corresponding to delay
        public int colorBias = 10; // the bias
        public void NewTurn()
        {
            commandProcessor.NewTurn();
            Rerender();
        }

        public void Start()
        {
            willUpdate = 2;
        }

        public void Update()
        {
            if(willUpdate >= 0)
                willUpdate -= 1;
            if (willUpdate == 0)
            {
                icons.Check();
                Rerender();
            }
        }

        public Color GetColor(int delay)
        {
            int index = delay +
                Instance.colorBias;
            if (index >= Instance.colors.Length)
            {
                index = Instance.colors.Length - 1;
            }
            return Instance.colors[index];
        }

        public Color GetOpaqueColor(int delay)
        {
            Color color = GetColor(delay);
            color.a = 1f;
            return color;
        }
        public void Rerender()
        {
            player.Rerender();
            gridController.Rerender();
        }
    }
}