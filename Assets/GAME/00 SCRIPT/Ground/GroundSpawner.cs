using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public Vector3 nextSpawnPoint;

    public List<GameObject> groundTile;
    [SerializeField] Transform groundSpawner;
    private void Awake()
    {
        groundSpawner = gameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 15; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        int indexGroundTile = Random.Range(0, groundTile.Count);

        GameObject temp = Instantiate(groundTile[indexGroundTile], nextSpawnPoint, Quaternion.identity, groundSpawner);

        // temp.transform.GetChild(1).position: la bang vi tri hien tai cua GroundTile cong voi vi tri hien tai cua GameObject con thu 1 (la NextSpawnPoint)
        nextSpawnPoint = temp.transform.GetChild(1).position;
    }
}
