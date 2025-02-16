using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTileSpawner : MonoBehaviour
{
    public enum AXIS { XPositive, XNegative, ZPositive, ZNegative }

    public GameObject[] grounds;
    public int initialSpawnCount = 15;
    public float destoryZone = 300;

    [Space(10)]
    public AXIS axis;

    [HideInInspector]
    public Vector3 moveDirection = new Vector3(-1, 0, 0);

    public float movingSpeed = 15f;
    public float maxSpeed = 25f;


    //public float groundSize = 30;
    GameObject lastGround;

    public Vector3 nextSpawnPoint;

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

            ground.transform.position = nextSpawnPoint;

            switch (axis)
            {
                case AXIS.XPositive:
                    break;

                case AXIS.XNegative:
                    break;

                case AXIS.ZPositive:
                    moveDirection = new Vector3(0, 0, -1);
                    break;

                case AXIS.ZNegative:
                    moveDirection = new Vector3(0, 0, -1);
                    break;
            }


            lastGround = ground;
            nextSpawnPoint = ground.transform.GetChild(1).position;
        }
    }

    public void DestroyChunk(RunnerGroundTile thisGround)
    {
        nextSpawnPoint = lastGround.transform.GetChild(1).position;
        //switch (axis)
        //{
        //    case AXIS.XPositive:
        //        newPos.x -= groundSize;
        //        break;

        //    case AXIS.XNegative:
        //        newPos.x -= groundSize;
        //        break;

        //    case AXIS.ZPositive:
        //        newPos.z -= groundSize;
        //        break;

        //    case AXIS.ZNegative:
        //        newPos.z += groundSize;
        //        break;
        //}

        int groundIndex = Random.Range(0, grounds.Length);

        GameObject ground = Instantiate(grounds[groundIndex], nextSpawnPoint, Quaternion.identity, groundSpawner);
        ground.SetActive(true);
        ground.GetComponent<RunnerGroundTile>().spawner = this;

        lastGround = ground.gameObject;
    }
}
