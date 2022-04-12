using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scenes;
using Unity.Mathematics;

public class Soldier : Unit
{
    public int damage = 5;
    public int health = 10;
    

    public void Start()
    {
        Orchestrator.Instance.gridController.AddUnit(x, y, this);
    }

    public override bool SurpriseAttack(int x, int y, Unit other)
    {
        battleReady = false;
        if (!other.hunkeredDown) {
            if(other.currentStamina == 0) {
                other.ReceiveDamage(damage);
            } else {
                other.ReceiveDamage((int) Math.Floor(damage * 0.5));
            }
        }
        other.Defend(this);
        return other.health <= 0;
    }

    public override void Defend(Unit other)
    {
        if (!other.battleReady)
        {
            other.ReceiveDamage((int) Math.Floor(damage * 1.5));
        }
        else if (currentStamina == 0)
        {
            other.ReceiveDamage((int) Math.Floor(damage * 0.5));
        } else {
            other.ReceiveDamage(damage);
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
    }public override bool Attack(int x,  int y, Unit other)
    {
        currentStamina -= 1;
        this.x = x;
        this.y = y;
        battleReady = true;
        if (other.hunkeredDown)
        {
            other.ReceiveDamage((int) Math.Floor(damage * 0.5));
        } else if (currentStamina == 0) {
            other.ReceiveDamage((int) Math.Floor(damage * 1.5));
        } else {
            other.ReceiveDamage(damage);
        }
        other.Defend(this);
        return other.health <= 0;
    }

    public override bool CanAttack(int x, int y)
    {
        return currentStamina > 0 && math.abs(this.x - x) <= 1 && math.abs(this.y - y) <= 1;
    }
    public override void ReceiveDamage(int damage)
    {
        health -= damage;
    }
}
