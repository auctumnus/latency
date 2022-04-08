using System.Collections.Generic;
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
        [SerializeField] public GameObject playerTemplate;
        public Transform[] players;
        public int currentPlayer = 0;
        private Transform _transform;

        public void Start()
        {
            _transform = transform;
        }
        public GameObject Create(GameObject toCreate)
        {
            return Instantiate(toCreate, players[currentPlayer]);
        }

        public void NextPlayer()
        {
            players[currentPlayer].gameObject.SetActive(false);
            currentPlayer += 1;
            if (currentPlayer == players.Length)
                currentPlayer = 0;
            players[currentPlayer].gameObject.SetActive(true);
        }

        public void SetPlayers(int numPlayers)
        {
            players = new Transform[numPlayers];
            for (int i = 0; i < numPlayers; i++)
                players[i] = Instantiate(playerTemplate, _transform).transform;
        }

        public void Delete(GameObject toDestroy)
        {
            Debug.Log("Deleted something... for some reason...");
            Destroy(toDestroy);
        }
    }
}