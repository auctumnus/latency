using System.Collections.Generic;
using UnityEngine;

namespace Scenes
{
    public class CommandProcessor
    {
        private List<List<Command>> commands = new();

        public void NewTurn()
        {
            GridController gc = Orchestrator.Instance.gridController;
            for (int player = 0; player < commands.Count; player++)
            {
                gc.ResetStamina(player);
                bool[] toRemove = new bool[commands[player].Count];
                for (int i = 0; i < commands[player].Count; i++)
                {
                    bool x = commands[player][i].Tick(gc);
                    Debug.Log(x);
                    toRemove[i] = x;
                }
                Debug.Log("Commands length: " + commands.Count);
                for (int i = commands[player].Count - 1; i >= 0; i--)
                    if (toRemove[i])
                        commands[player].RemoveAt(i);
            }
            Render();
        }

        public void Init(int numPlayers)
        {
            for(int i = 0; i < numPlayers; i++)
                commands.Add(new List<Command>());
        }
        public void Render()
        {
            foreach(List<Command> commands in commands)
                foreach(Command cmd in commands)
                    cmd.Render();
        }

        public void Add(Command cmd)
        {
            commands[cmd.player].Add(cmd);
        }
    }
}