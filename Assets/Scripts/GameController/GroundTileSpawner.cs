using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTileSpawner : MonoBehaviour
{
    public enum AXIS { XPositive, XNegative, ZPositive, ZNegative }

    public GameObject[] grounds;
    public int initialSpawnCount = 5;
    public float destoryZone = 300;

    [Space(10)]
    public AXIS axis;

    [HideInInspector]
    public Vector3 moveDirection = new Vector3(-1, 0, 0);

    public float movingSpeed = 15f;
    public float maxSpeed = 25f;


    public float groundSize = 30;
    GameObject lastGround;

    [SerializeField] Transform groundSpawner;


    void Awake()
    {
        groundSpawner = gameObject.transform;

        for (int i = 0; i < initialSpawnCount; i++)
        {
            int groundIndex = Random.Range(0, grounds.Length);
            GameObject ground = (GameObject)Instantiate(grounds[groundIndex], groundSpawner);
            ground.SetActive(true);

            ground.GetComponent<RunnerGroundTile>().spawner = this;

            switch (axis)
            {
                case AXIS.XPositive:
                    ground.transform.localPosition = new Vector3(transform.position.x, 0, i * groundSize);
                    break;

                case AXIS.XNegative:
                    ground.transform.localPosition = new Vector3(transform.position.x, 0, i * groundSize);
                    break;

                case AXIS.ZPositive:
                    ground.transform.localPosition = new Vector3(transform.position.x, 0, i * groundSize);
                    moveDirection = new Vector3(0, 0, -1);
                    break;

                case AXIS.ZNegative:
                    ground.transform.localPosition = new Vector3(transform.position.x, 0, i * groundSize);
                    moveDirection = new Vector3(0, 0, -1);
                    break;
            }


            lastGround = ground;
        }
    }

    public void DestroyChunk(RunnerGroundTile thisGround)
    {
        Vector3 newPos = lastGround.transform.position;
        switch (axis)
        {
            case AXIS.XPositive:
                newPos.x -= groundSize;
                break;

            case AXIS.XNegative:
                newPos.x -= groundSize;
                break;

            case AXIS.ZPositive:
                newPos.z -= groundSize;
                break;

            case AXIS.ZNegative:
                newPos.z += groundSize;
                break;
        }

        int groundIndex = Random.Range(0, grounds.Length);

        GameObject ground = Instantiate(grounds[groundIndex], groundSpawner);
        ground.SetActive(true);
        ground.GetComponent<RunnerGroundTile>().spawner = this;

        lastGround = ground.gameObject;
        lastGround.transform.position = newPos;
    }
}
