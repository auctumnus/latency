namespace Scenes.Actions
{
    public abstract class Action
    {
        public abstract void Fail();
        public abstract void Execute(Unit u);
    }
}