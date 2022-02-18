using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    public int damage = 5;
    public override void Battle(Unit other)
    {
        throw new System.NotImplementedException();
    }

    public override void ReceiveDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void MoveInternal(int x, int y)
    {
        throw new System.NotImplementedException();
    }
}
