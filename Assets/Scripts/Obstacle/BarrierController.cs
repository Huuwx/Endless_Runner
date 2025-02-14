using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (PlayerController.Instance.GetImmortal())
            {
                ParticleSystemController.Instance.explosion.Play();
                Destroy(gameObject);
                ItemController.Instance.OutOfTimeToUseMagicShield();
            }
            else
            {
                PlayerController.Instance.Die();
                collision.rigidbody.AddForce(new Vector3(0, 1, -1) * 5, ForceMode.Impulse);
                collision.rigidbody.excludeLayers |= (1 << LayerMask.NameToLayer("Barrier"));
            }
        }
    }
}
