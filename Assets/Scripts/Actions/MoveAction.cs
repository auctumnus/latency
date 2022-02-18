using UnityEngine;

namespace Scenes.Actions
{
    public class MoveAction : Action
    {
        private int x;
        private int y;
        private int player;

        public MoveAction(int x, int y, int player)
        {
            this.x = x;
            this.y = y;
            this.player = player;
        }
        
        public override void Specify(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override void Fail(GridController gc)
        {
            
        }

        public override void Execute(Unit unit, GridController gc)
        {
            Unit otherUnit = gc.GETUnit(x, y);
            if (otherUnit == null)
            {
                gc.MoveUnit(unit.x, unit.y, x, y);
                return;
            }

            if (otherUnit.owner == unit.owner)
            {
                Fail(gc);
            }
            else
            {
                
            }
        }
    }
}