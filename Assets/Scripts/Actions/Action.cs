using Scenes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class Action
{
        public abstract void Fail(GridController gc);
        public abstract void Execute(Unit u, GridController gc);
        public abstract void Specify(int x, int y);
        protected SpriteRenderer arrow;
        protected SpriteRenderer tip;

        public abstract void Render(int x, int y, int delay);
}
