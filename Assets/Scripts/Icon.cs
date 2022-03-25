using UnityEngine;

namespace Scenes
{
    public class Icon: MonoBehaviour
    {
        public void RemoveSelf()
        {
            Destroy(gameObject);
            Debug.Log("Okay, it's fine...");
            Orchestrator.Instance.willUpdate = 2;
        }
    }
}