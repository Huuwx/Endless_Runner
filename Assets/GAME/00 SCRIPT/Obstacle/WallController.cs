using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : ObstacleBase
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(CONSTANT.PlayerTag))
            return;
        
        GameManager.Instance.Player.Die();
    }
}
