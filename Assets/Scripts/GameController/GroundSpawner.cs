using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    private static GroundSpawner instance;
    public static GroundSpawner Instance {  get { return instance; } }

    public Vector3 nextSpawnPoint;

    public GameObject groundTile;
    [SerializeField] Transform groundSpawner;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        groundSpawner = gameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 20; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity, groundSpawner);

        // temp.transform.GetChild(1).position: la bang vi tri hien tai cua GroundTile cong voi vi tri hien tai cua GameObject con thu 1 (la NextSpawnPoint)
        nextSpawnPoint = temp.transform.GetChild(1).position;
    }
}