using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;
using Unity.VisualScripting;

public class Command
{
    private int _delay;

    public int x;
    public int y;

    public Action payload;
    // Start is called before the first frame update
    public Command(int x, int y, int delay, Action payload)
    {
        this.x = x;
        this.y = y;
        _delay = delay;
        this.payload = payload;
    }
    private void Execute(GridController gc)
    {
        Unit u = Orchestrator.Instance.gridController.GETUnit(x, y);
        if (u == null)
        {
            payload.Fail(gc);
        }
        else
        {
            payload.Execute(u, gc);
        }
    }
    // Update is called once per frame
    public bool Tick(GridController gc)
    {
        if (_delay == 0)
        {
            Execute(gc);
            return true;
        }
        _delay -= 1;
        return false;
    }

    public void Render()
    {
        payload.Render(x, y, _delay);
    }
}
