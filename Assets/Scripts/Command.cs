using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenes.Actions;
using Unity.VisualScripting;
using Action = Scenes.Actions.Action;

public class Command : MonoBehaviour
{
    private int delay;

    public int x;

    public int y;

    public Action payload;
    // Start is called before the first frame update
    public void Execute()
    {
        Unit u = GetUnit();
        if (u == null)
        {
            payload.Fail();
        }
        else
        {
            payload.Execute(u);
        }
    }
    // gets an object on the location of this command. Returns 
    public Unit GetUnit()
    {
        throw new NotImplementedException();
    }
    // Update is called once per frame
    public bool Tick()
    {
        if (delay == 0)
        {
            Execute();
            return true;
        }
        delay -= 1;
        return false;
    }
}
