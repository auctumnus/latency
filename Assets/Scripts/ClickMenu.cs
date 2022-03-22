using System.Collections;
using System.Collections.Generic;
using Scenes;
using Scenes.Actions;
using UnityEditor;
using UnityEngine;

public class ClickMenu : MonoBehaviour
{
    public int x;
    public int y;
    [SerializeField] private PlayerInput pi;
    [SerializeField] private Liaison li;
    public void SelectMove()
    {
        pi.StartOrder(x, y, new MoveAction(-1, -1, pi.currentPlayer, li));
        gameObject.SetActive(false);
    }

    public void SelectAttack()
    {
        pi.StartOrder(x, y, new AttackAction(-1, -1, pi.currentPlayer, li));
        gameObject.SetActive(false);
    }

    public void SelectDefend()
    {
        pi.StartOrder(x, y, new DefendAction(pi.currentPlayer, li));
        gameObject.SetActive(false);
    }
}
