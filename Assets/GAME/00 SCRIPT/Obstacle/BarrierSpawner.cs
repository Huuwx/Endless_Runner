using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    [SerializeField] GameObject BarrierPrefab;

    // Start is called before the first frame update
    void Start()
    {
        BarrierSpawn();
    }

    void BarrierSpawn()
    {
        int numberOfBarrier = Random.Range(1, 4);
        Queue<int> previousNumber = new Queue<int>();
        for (int i = 1; i <= numberOfBarrier; i++)
        {
            int indexBarrier;
            do
            {
                indexBarrier = Random.Range(2, 5);
            } while(previousNumber.Contains(indexBarrier));

            previousNumber.Enqueue(indexBarrier);

            Transform spawnPoint = transform.GetChild(indexBarrier).transform;
            Instantiate(BarrierPrefab, spawnPoint.position, Quaternion.identity, transform);
        }
    }
}
