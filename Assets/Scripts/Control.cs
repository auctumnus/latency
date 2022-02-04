using System.Collections.Generic;

namespace Scenes
{
    public class CommandProcessor
    {
        private LinkedList<Command> commands = new LinkedList<Command>();

        public void NewTurn()
        {
            for (LinkedListNode<Command> node = commands.First; node == null; node = node.Next)
            {
                if (node.Value.Tick())
                {
                    commands.Remove(node);
                }
            }
        }
    }
}