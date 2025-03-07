using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : BarrierController
{
    private bool isActive = false;

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
            if (PlayerParameters.Instance.GetImmortal())
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

    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetIsActive(bool value)
    {
        isActive = value;
    }
}
