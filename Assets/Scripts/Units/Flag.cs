using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scenes;
using Unity.Mathematics;

public class Flag : Unit
{
    public override void Start()
    {
        Orchestrator.Instance.gridController.AddUnit(x, y, this);
    }

    public override bool SurpriseAttack(int x, int y, Unit enemy)
    {
        return false;
    }
    public override void Defend(Unit other)
    {
    }
    
    public override bool CanMove(int x, int y)
    {
        return false;
    }
    public override bool Attack(int _1,  int _, Unit other)
    {
        return false;
    }

    public override bool CanAttack(int x, int y)
    {
        return false;
    }
    public override void ReceiveDamage(int damage, Unit other)
    {
        Debug.Log("Damage received!");
        health -= damage;
        if (health <= 0)
        {
            Orchestrator.Instance.player.DisableInput();
            Orchestrator.Instance.GameOver(other.owner);
        }
    }
}
