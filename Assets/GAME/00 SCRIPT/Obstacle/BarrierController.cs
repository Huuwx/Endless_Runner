using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (GameManager.Instance.Player.playerParameters.IsImmortal)
            {
                ParticleSystemController.Instance.explosion.Play();
                gameObject.SetActive(false);
                ItemController.Instance.OutOfTimeToUseMagicShield();
            }
            else
            {
                GameManager.Instance.Player.Die();
            }
        }
    }
}
