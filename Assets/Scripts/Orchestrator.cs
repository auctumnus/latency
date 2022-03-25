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
        public GameObject orderQueue;
        public Icons icons;
        public int willUpdate;

        public Color[] colors; // colors, corresponding to delay
        public int colorBias = 10; // the bias
        public void NewTurn()
        {
            Debug.Log("New turn... for some reason.");
            commandProcessor.NewTurn();
        }

        public void Update()
        {
            if(willUpdate >= 0)
                willUpdate -= 1;
            if (willUpdate == 0)
                icons.Check();
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
            gridController.Rerender();
            player.Rerender();
        }
    }
}