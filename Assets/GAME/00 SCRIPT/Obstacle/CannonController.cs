using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : ObstacleBase
{
    private bool isActive = false;

    void Update()
    {
        if (!isActive)
            return;
        
        transform.Translate(Vector3.back * 20 * Time.deltaTime, Space.World);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.PlayerTag))
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
            isActive = false;
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
