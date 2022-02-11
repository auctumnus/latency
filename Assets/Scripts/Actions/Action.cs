using Scenes;
using UnityEngine;

public abstract class Action
{ 
        public abstract void Fail(GridController gc);
        public abstract void Execute(Unit u, GridController gc);
}
