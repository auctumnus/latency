using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    public int damage = 5;
    public bool battleReady = false;
    public bool hunkeredDown = false;
    public override void Battle(Unit other)
    {
        if(!other.battleReady) {
            // bonus damage!
            other.ReceiveDamage((int) Math.floor(damage * 1.5));
        } else if(other.hunkeredDown) {
            // reduce damage
            other.ReceiveDamage((int) Math.floor(damage * 0.5));
        } else {
            other.ReceiveDamage(damage);
        }
    }

    public override void ReceiveDamage(int damage)
    {
        health -= damage;
    }

    public override void MoveInternal(int x, int y)
    {
        throw new System.NotImplementedException();
    }
}
