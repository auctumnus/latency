using Scenes;

public abstract class Action
{ 
        public abstract void Fail();
        public abstract void Execute(Unit u, GridController gc);
}
