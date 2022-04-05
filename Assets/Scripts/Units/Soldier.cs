using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scenes;

public class Soldier : Unit
{
    public int damage = 5;
    public int health = 10;

    public void Start()
    {
        Orchestrator.Instance.gridController.SetUnit(x, y, this);
    }
    public override void Battle(Unit other)
    {
        if(!other.battleReady) {
            // bonus damage!
            other.ReceiveDamage((int) Math.Floor(damage * 1.5));
        } else if(other.hunkeredDown) {
            // reduce damage
            other.ReceiveDamage((int) Math.Floor(damage * 0.5));
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
