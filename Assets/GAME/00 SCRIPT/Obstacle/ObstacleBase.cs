using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour
{
    public virtual void Active(){}
    
    public virtual void ResetActive()
    {
        this.gameObject.SetActive(true);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(CONSTANT.PlayerTag))
        {
            if (GameManager.Instance.Player.playerParameters.IsImmortal)
            {
                GameManager.Instance.ParticleController.explosion.Play();
                GameManager.Instance.ItemManager.ChangeItem(CONSTANT.ShieldItemIndex);
                GameManager.Instance.ItemAdapter.ClearUseTime();
                this.gameObject.SetActive(false);
                return;
            }

            GameManager.Instance.Player.Die();
            
        }
    }
}
