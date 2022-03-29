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
            Debug.Log($"{commands.Count} commands to process");
            if (commands.Count == 0)
                return;
            for (LinkedListNode<Command> node = commands.First; node != null; node = node.Next)
            {
                var command = node.Value;
                Debug.Log($"processing {command}");
                // Command.Tick() returns true if the delay is 0.
                // Thus, if this if branch occurs, then the command has run.
                if (command.Tick(gc))
                {
                    Debug.Log($"finished processing {command}, removing");
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