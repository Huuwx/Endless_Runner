using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : BarrierController
{
    public bool isActive = false;

    void Update()
    {
        if (isActive)
        {
            transform.Translate(Vector3.back * 20 * Time.deltaTime, Space.World);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerParameters.Instance.immortal)
            {
                ParticleSystemController.Instance.explosion.Play();
                Destroy(gameObject);
                ItemController.Instance.OutOfTimeToUseMagicShield();
            }
            else
            {
                PlayerController.Instance.Die();
                isActive = false;
            }
        }
    }
}
