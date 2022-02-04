using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
abstract public class Unit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    public bool selected = false;

    // This needs to be re-implemented.
    // The basic system should be a general grid controller, which knows the location of units,
    // and which one is currently selected. That should handle moving units to the right location.
    
    public void Update()
    {
        /*if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                OnFocus();
            }
            else
            {
                OnFocusLost();
            }
        }*/
    }
}
