using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Scenes;
using Scenes.Actions;
using UnityEngine;
using UnityEngine.UI;

public class Icons: MonoBehaviour
{
    public List<GameObject> queue;
    public GameObject attackPrefab;
    public GameObject movePrefab;
    public GameObject defendPrefab;
    public void Check()
    {
        Debug.Log("Checking...");
        bool[] toRemove = new bool[queue.Count];
        for (int i = 0; i < queue.Count; i++)
        {
            Debug.Log("This is:" + queue[i]);
            try
            {
                Transform t = queue[i].transform;
            }
            catch (Exception _)
            {
                Debug.Log("A SUCCESS!");
                toRemove[i] = true;
            }
            if (queue[i] == null)
            {
                Debug.Log("A SUCCESS!");
                toRemove[i] = true;
            }
        }

        PlayerInput input = Orchestrator.Instance.player;
        for(int i = queue.Count - 1; i >= 0; i--)
            if (toRemove[i])
            {
                queue.RemoveAt(i);
                Debug.Log("Removing!");
                input.queue[i].payload.Fail(Orchestrator.Instance.gridController);
                Debug.Log("Removed!");
                input.queue.RemoveAt(i);
            }
    }

    public void Clear()
    {
        for(int i = queue.Count - 1; i >= 0; i--)
            {
                Destroy(queue[i]);
                queue.RemoveAt(i);
            }
        
    }
    public void Add(Command cmd)
    {
        GameObject parent = Orchestrator.Instance.orderQueue;
        GameObject obj;
        if (cmd.payload is AttackAction)
            obj = Instantiate(attackPrefab, parent.transform);
        else if (cmd.payload is DefendAction)
            obj = Instantiate(defendPrefab, parent.transform);
        else 
            obj = Instantiate(movePrefab, parent.transform);
        Image sr = obj.GetComponent<Image>();
        sr.color = Orchestrator.Instance.GetOpaqueColor(cmd.Delay);
        queue.Add(obj);
    }
}
/*
 * int x = if x == 5 {6} else {x};
 * return match x {
 *  0..=5 => 5,
 *  6..=10 => 10,
 *  _ => 0
 * }
*/