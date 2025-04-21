using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(CONSTANT.PlayerTag))
        {
            if (GameManager.Instance.Player.playerParameters.IsImmortal)
            {
                GameManager.Instance.ParticleController.explosion.Play();
                GameManager.Instance.ItemManager.ChangeItem(CONSTANT.ShieldItemIndex);
                GameManager.Instance.ItemController.ClearUseTime();
                this.gameObject.SetActive(false);
                return;
            }

            GameManager.Instance.Player.Die();
            
        }
    }
}
