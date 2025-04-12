using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (GameManager.Instance.Player.playerParameters.IsImmortal)
            {
                ParticleSystemController.Instance.explosion.Play();
                gameObject.SetActive(false);
                // ItemController.Instance.OutOfTimeToUseItem();
            }
            else
            {
                GameManager.Instance.Player.Die();
            }
        }
    }
}
