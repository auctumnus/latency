﻿using UnityEngine;

namespace Scenes.Actions
{
    public class MoveAction : Action
    {
        public int x;
        public int y;

        MoveAction(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public override void Fail()
        {
            
        }

        public override void Execute(Unit unit, GridController gc)
        {
            if (gc.getUnit(x, y))
            {
                Fail();
            }
            else
            {
                gc.moveUnit(unit.x, unit.y, x, y);
            }
        }
    }
}