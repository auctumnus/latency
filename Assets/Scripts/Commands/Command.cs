using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    private int delay;
    // Start is called before the first frame update
    public abstract void Execute();

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
