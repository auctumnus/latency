using System.Runtime.CompilerServices;
using UnityEngine;

namespace Scenes.Actions
{
    public class Liaison: MonoBehaviour
    {
        [SerializeField] public GameObject arrowPrefab;
        [SerializeField] public GameObject attackTipPrefab;
        [SerializeField] public GameObject defendTipPrefab;
        [SerializeField] public GameObject moveTipPrefab;
        public GameObject Create(GameObject toCreate)
        {
            return Instantiate(toCreate);
        }

        public void Delete(GameObject toDestroy)
        {
            Debug.Log("Deleted something... for some reason...");
            Destroy(toDestroy);
        }
    }
}