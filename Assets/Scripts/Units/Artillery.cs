using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scenes;
using Unity.Mathematics;

public class Artillery : Unit
{
    public override void Start()
    {
        base.Start();
        Orchestrator.Instance.gridController.AddUnit(x, y, this);
    }

    public override bool SurpriseAttack(int x, int y, Unit other)
    {
        battleReady = false;
        if(other.currentStamina == 0) {
            other.ReceiveDamage(damage, this);
        } else {
            other.ReceiveDamage((int) Math.Floor(damage * 0.5), this);
        }
        
        // other.Defend(this);
        return other.health <= 0;
    }

    public override void Defend(Unit other)
    {
        if (!other.battleReady)
        {
            other.ReceiveDamage((int) Math.Floor(damage * 1.5), this);
        }
        else if (currentStamina == 0)
        {
            other.ReceiveDamage((int) Math.Floor(damage * 0.5), this);
        } else {
            other.ReceiveDamage(damage, this);
        }
    }


    public override void Move(int x,  int y)
    {
        currentStamina -= 1;
        this.x = x;
        this.y = y;
    }
    public override bool CanMove(int x, int y)
    {
        return currentStamina > 0 && math.abs(this.x - x) <= 1 && math.abs(this.y - y) <= 1;
    }
    
    public override bool Attack(int x,  int y, Unit other)
    {
        Debug.Log("ATTACKING");
        currentStamina -= 1;
        battleReady = true;
        if (other.currentStamina == 0) { 
            other.ReceiveDamage((int) Math.Floor(damage * 1.5), this);
        } else { 
            other.ReceiveDamage(damage, this);
        }
        return false;
    }

    public override bool CanAttack(int x, int y)
    {
        var dx = Math.Abs(this.x - x);
        var dy = Math.Abs(this.y - y);
        Debug.Log("Can attack: " + (currentStamina > 0 && (dx >= 2 || dy >= 2) && dx <= 6 && dy <= 6));
        return currentStamina > 0 && (dx >= 2 || dy >= 2) && dx <= 6 && dy <= 6;
    }
    public override void ReceiveDamage(int damage, Unit other)
    {
        health -= damage;
    }
}
