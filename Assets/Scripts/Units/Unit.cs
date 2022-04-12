using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
abstract public class Unit : MonoBehaviour
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

    public void Start()
    {
        _transform = GetComponent<SpriteRenderer>().transform;
        //HealthText = GetComponent<TextMeshPro>();    
    }

    public void Update()
    {
        HealthText.text = health.ToString();
    }

   

    public abstract void Battle(Unit other);
    public abstract void ReceiveDamage(int damage);

    public void NextTurn()
    {
    }

    public void Rerender()
    {
        GetComponent<SpriteRenderer>().transform.position = new Vector3(x, y);
    }
}
