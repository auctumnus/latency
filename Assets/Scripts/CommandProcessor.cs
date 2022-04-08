using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Scenes
{
    public class CommandProcessor
    {
        private List<Command> commands = new();

        public void NewTurn()
        {
            GridController gc = Orchestrator.Instance.gridController;
            Debug.Log($"{commands.Count} commands to process");
            if (commands.Count == 0)
                return;
            Debug.Log("Commands length: " + commands.Count);
            bool[] toRemove = new bool[commands.Count];
            for (int i = 0; i < commands.Count; i++)
            {
                bool x = commands[i].Tick(gc);
                Debug.Log(x);
                toRemove[i] = x;   
            }
            Debug.Log("Commands length: " + commands.Count);
            for (int i = commands.Count - 1; i >= 0; i--)
                if (toRemove[i])
                    commands.RemoveAt(i);
            Render();
        }

        public void Render()
        {
            foreach(Command cmd in commands)
            {
                cmd.Render();
            }
        }

        public void Add(Command cmd)
        {
            commands.Add(cmd);
        }
    }
}