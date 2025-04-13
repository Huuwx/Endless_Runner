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
                return;
            
            GameManager.Instance.Player.Die();
            
        }
    }
}
