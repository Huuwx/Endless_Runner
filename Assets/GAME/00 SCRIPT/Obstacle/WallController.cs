using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : ObstacleBase
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.Player.Die();
        }
    }
}
