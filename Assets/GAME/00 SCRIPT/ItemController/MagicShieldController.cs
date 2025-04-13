using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShieldController : ItemBase
{
    private void Update()
    {
        ItemEffect();
    }

    protected override void ItemEffect()
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (useTimeCounter > 0)
            {
                useTimeCounter -= Time.deltaTime;
                GameManager.Instance.Player.playerParameters.State = PlayerState.Immortal;
            }
            else
            {
                ClearUseTime();
            }
        }
    }

    public override void ClearUseTime()
    {
        base.ClearUseTime();
        GameManager.Instance.Player.playerParameters.State = PlayerState.Normal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrier")
        {
            ParticleSystemController.Instance.explosion.Play();
            ClearUseTime();
            other.gameObject.SetActive(false);
        }
    }
}
