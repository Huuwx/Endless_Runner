using System;
using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerGroundTile : MonoBehaviour
{
    public GroundTileSpawner spawner;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance.Player.playerParameters.IsAlive && GameManager.Instance.isStarted)
        {
            if(spawner.movingSpeed < spawner.maxSpeed)
            {
                spawner.movingSpeed += 0.005f * Time.deltaTime;
            }
            transform.Translate(spawner.moveDirection * (spawner.movingSpeed * Time.deltaTime));
        }
    }

    void FixedUpdate()
    {
        // if (GameManager.Instance.Player.playerParameters.IsAlive && GameManager.Instance.isStarted)
        // {
        //     rb.MovePosition(rb.position + spawner.moveDirection * (spawner.movingSpeed * Time.fixedDeltaTime));
        // }

        switch (spawner.axis)
        {
            case GroundTileSpawner.AXIS.XPositive:
                if (transform.position.x > spawner.destoryZone)
                {
                    gameObject.SetActive(false);
                    spawner.SpawnNextGround(this);
                    // Destroy(gameObject);
                }
                break;

            case GroundTileSpawner.AXIS.XNegative:
                if (transform.position.x < -spawner.destoryZone)
                {
                    gameObject.SetActive(false);
                    spawner.SpawnNextGround(this);
                    //Destroy(gameObject);
                }

                break;

            case GroundTileSpawner.AXIS.ZPositive:
                if (transform.position.z > spawner.destoryZone)
                {
                    gameObject.SetActive(false);
                    spawner.SpawnNextGround(this);
                    //Destroy(gameObject);
                }
                break;

            case GroundTileSpawner.AXIS.ZNegative:
                if (transform.position.z < -spawner.destoryZone)
                {
                    gameObject.SetActive(false);
                    spawner.SpawnNextGround(this);
                    //Destroy(gameObject);
                }
                break;
        }

    }
}
