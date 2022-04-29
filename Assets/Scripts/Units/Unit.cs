using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    public int x;
    public int y;

    public int damage;
    public int health;
    public TMP_Text HealthText;
    public int owner;
    
    public bool battleReady = false;
    public bool hunkeredDown = false;
    private Transform _transform;
    
    /// <summary>
    /// How many moves a unit can make in one turn.
    /// </summary>
    public int staminaCapacity = 1;
    /// <summary>
    /// Amount of moves left this unit can make.
    /// </summary>
    public int currentStamina = 1;

    public virtual void Start()
    {
        _transform = GetComponent<SpriteRenderer>().transform;
        //HealthText = GetComponent<TextMeshPro>();    
    }

    public void Update()
    {
        HealthText.text = health.ToString();
    }

    public abstract bool CanMove(int x, int y);
    public virtual void Move(int x,  int y)
    {
        currentStamina -= 1;
        this.x = x;
        this.y = y;
    }
    public abstract bool CanAttack(int x, int y);
    // returns true if the battle was won
    public abstract bool Attack(int x, int y, Unit unit);

    public virtual bool CanDefend()
    {
        return currentStamina != 0;
    }

    public virtual void Defend()
    {
        currentStamina = 0;
        hunkeredDown = true;
    }
    // returns true if the battle was won
    public abstract bool SurpriseAttack(int x, int y, Unit other);
    public abstract void Defend(Unit other);
    public abstract void ReceiveDamage(int damage, Unit other);

    public void NextTurn()
    {
    }

    public void Rerender()
    {
        GetComponent<SpriteRenderer>().transform.position = new Vector3(x, y);
    }
}
