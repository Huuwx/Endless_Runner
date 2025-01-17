using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCoin();
    }

    public void SpawnCoin()
    {
        int numberOfCoin = Random.Range(1, 6);
        for (int i = 0; i < numberOfCoin; i++)
        {
            GameObject temp = Instantiate(coinPrefabs, transform);
            temp.transform.position = RandomPos(GetComponent<Collider>());
        }
    }

    Vector3 RandomPos(Collider collider)
    {
        GameObject lane = GameObject.Find("Lane");
        Vector3 point = new Vector3(
            Random.Range(0, 3),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        if (point.x == 0)
        {
            point.x = lane.transform.GetChild(0).position.x;
        }
        else if (point.x == 1)
        {
            point.x = lane.transform.GetChild(1).position.x;
        }
        else
        {
            point.x = lane.transform.GetChild(2).position.x;
        }

        if (point != collider.ClosestPoint(point))
        {
            point = RandomPos(collider);
        }
        point.y = 1;
        return point;

    }
}
