using System.Collections.Generic;
using UnityEngine;

namespace Scenes
{
    public class CommandProcessor
    {
        private LinkedList<Command> commands = new LinkedList<Command>();

        public void NewTurn()
        {
            GridController gc = Orchestrator.Instance.gridController;
            if (commands.Count == 0)
                return;
            
            foreach (Command node in commands)
            {
                // Command.Tick() returns true if the delay is 0.
                // Thus, if this if branch occurs, then the command has run.
                if (node.Tick(gc))
                {
                    commands.Remove(node);
                }
            }
        }

        public void Add(Command cmd)
        {
            commands.AddLast(cmd);
        }
    }
}