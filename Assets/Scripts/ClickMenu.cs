using System.Collections;
using System.Collections.Generic;
using Scenes;
using Scenes.Actions;
using UnityEditor;
using UnityEngine;

public class ClickMenu : MonoBehaviour
{
    private int x;
    private int y;
    [SerializeField] private PlayerInput pi;
    void SelectMove()
    {
        pi.StartOrder(x, y, new MoveAction(-1, -1, pi.currentPlayer));
        gameObject.SetActive(false);
    }

    void SelectAttack()
    {
        pi.StartOrder(x, y, new AttackAction(-1, -1, pi.currentPlayer));
        gameObject.SetActive(false);
    }

    void SelectDefend()
    {
        pi.StartOrder(x, y, new DefendAction(pi.currentPlayer));
        gameObject.SetActive(false);
    }
}
