using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;
using Unity.VisualScripting;

public class Command
{
    private int delay;

    public Vector2 destination;

    public Action payload;
    // Start is called before the first frame update
    public void Execute(GridController gc)
    {
        Unit u = GetUnit();
        if (u == null)
        {
            payload.Fail(gc);
        }
        else
        {
            payload.Execute(u, gc);
        }
    }
    // gets an object on the location of this command. Returns 
    public Unit GetUnit()
    {
        throw new NotImplementedException();
    }
    // Update is called once per frame
    public bool Tick(GridController gc)
    {
        if (delay == 0)
        {
            Execute(gc);
            return true;
        }
        delay -= 1;
        return false;
    }
}
