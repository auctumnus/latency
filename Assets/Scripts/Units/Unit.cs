using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
abstract public class Unit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    public int x;
    public int y;

    public int damage;
    public int health;
    public int owner;
    public int isPrepared; // number of turns the unit is prepared for. 
    // Units lose all preparation when moving, and gain 1 turn of preparation on attack. 
    // Units gain 2 turns of preparation when defending. 


    public bool battleReady = false;
    public bool hunkeredDown = false;

    public abstract void Battle(Unit other);
    public abstract void ReceiveDamage(int damage);

    public void NextTurn()
    {
        if (isPrepared > 0)
            isPrepared -= 1;
    }
    public abstract void MoveInternal(int x, int y);
}
