using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGroundController : MonoBehaviour
{
    public GroundTileSpawner spawner;

    public Transform pivot;

    private void Start()
    {
        spawner = GameObject.Find("GroundSpawner").GetComponent<GroundTileSpawner>();
    }
}
