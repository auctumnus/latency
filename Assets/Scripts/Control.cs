using System.Collections.Generic;

namespace Scenes
{
    public class CommandProcessor
    {
        private LinkedList<Command> commands = new LinkedList<Command>();

        public void NewTurn()
        {
            for (LinkedListNode<Command> command = commands.First;command == null; command = command.Next)
            {
                if (command.Value.Tick())
                {
                    commands.Remove(command);
                }
            }
        }
    }
}