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

    public bool battleReady = false;
    public bool hunkeredDown = false;

    /// <summary>
    /// How many moves a unit can make in one turn.
    /// </summary>
    public int staminaCapacity = 1;

    public int currentStamina = 1;

    public abstract void Battle(Unit other);
    public abstract void ReceiveDamage(int damage);
    
    public abstract void MoveInternal(int x, int y);
}
