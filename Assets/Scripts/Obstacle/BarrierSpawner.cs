using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    public GameObject BarrierPrefab;

    // Start is called before the first frame update
    void Start()
    {
        BarrierSpawn();
    }

    void BarrierSpawn()
    {
        int indexBarrier = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(indexBarrier).transform;
        Instantiate(BarrierPrefab, spawnPoint.position, Quaternion.identity, transform);
    }
}
