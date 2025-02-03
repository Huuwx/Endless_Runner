using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerGroundTile : MonoBehaviour
{
    public GroundTileSpawner spawner;


    void Update()
    {
        if (PlayerController.Instance.GetIsAlive() == true && GameManager.Instance.isStarted)
        {
            transform.Translate(spawner.moveDirection * spawner.movingSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        switch (spawner.axis)
        {
            case GroundTileSpawner.AXIS.XPositive:
                if (transform.position.x > spawner.destoryZone)
                    spawner.DestroyChunk(this);
                break;

            case GroundTileSpawner.AXIS.XNegative:
                if (transform.position.x < -spawner.destoryZone)
                    spawner.DestroyChunk(this);
                break;

            case GroundTileSpawner.AXIS.ZPositive:
                if (transform.position.z > spawner.destoryZone)
                    spawner.DestroyChunk(this);
                break;

            case GroundTileSpawner.AXIS.ZNegative:
                if (transform.position.z < -spawner.destoryZone)
                    spawner.DestroyChunk(this);
                break;
        }

    }
}
