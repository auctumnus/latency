using System.Runtime.CompilerServices;
using UnityEngine;

namespace Scenes.Actions
{
    public class Liaison: MonoBehaviour
    {
        [SerializeField] public GameObject attackArrowPrefab;
        [SerializeField] public GameObject attackTipPrefab;
        [SerializeField] public GameObject defendArrowPrefab;
        [SerializeField] public GameObject defendTipPrefab;
        [SerializeField] public GameObject moveArrowPrefab;
        [SerializeField] public GameObject moveTipPrefab;
        public GameObject Create(GameObject gameObject)
        {
            return Instantiate(gameObject);
        }

        public void Delete(GameObject gameObject)
        {
            Debug.Log("Deleted something... for some reason...");
            Destroy(gameObject);
        }
    }
}