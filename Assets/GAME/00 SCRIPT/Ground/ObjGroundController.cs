using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGroundController : MonoBehaviour
{
    List<ItemIndex> items = new List<ItemIndex>();
    List<CoinController> coins = new List<CoinController>();
    List<ObstacleBase> obstacles = new List<ObstacleBase>();
    
    Dictionary<GameObject, List<GameObject>> _pool = new Dictionary<GameObject, List<GameObject>>();

    private void Start()
    {
        ItemIndex[] i = this.GetComponentsInChildren<ItemIndex>();
        foreach (ItemIndex item in i)
        {
            item.Init();
            items.Add(item);
            // if (_pool.ContainsKey(item.gameObject))
            // {
            //     _pool[item.gameObject].Add(item.gameObject);
            // }
            // else
            // {
            //     _pool.Add(item.gameObject, new List<GameObject>());
            //     _pool[item.gameObject].Add(item.gameObject);
            // }
        }
        
        CoinController[] co = this.GetComponentsInChildren<CoinController>();
        foreach (CoinController coin in co)
        {
            coin.Init();
            coins.Add(coin);
            // if (_pool.ContainsKey(coin.gameObject))
            // {
            //     _pool[coin.gameObject].Add(coin.gameObject);
            // }
            // else
            // {
            //     _pool.Add(coin.gameObject, new List<GameObject>());
            //     _pool[coin.gameObject].Add(coin.gameObject);
            // }
        }
        
        ObstacleBase[] ob = this.GetComponentsInChildren<ObstacleBase>();
        foreach (ObstacleBase obj in ob)
        {
            obstacles.Add(obj);
            // if (_pool.ContainsKey(obj.gameObject))
            // {
            //     //items.Add(item);
            //     _pool[obj.gameObject].Add(obj.gameObject);
            // }
            // else
            // {
            //     _pool.Add(obj.gameObject, new List<GameObject>());
            //     _pool[obj.gameObject].Add(obj.gameObject);
            // }
        }
    }

    private void OnEnable()
    {
        foreach (ItemIndex item in items)
        {
            item.Activate();
        }

        foreach (CoinController coin in coins)
        {
            coin.Activate();
        }

        foreach (ObstacleBase obstacle in obstacles)
        {
            obstacle.ResetActive();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(CONSTANT.PlayerTag))
            return;
        
        if (obstacles != null)
        {
            foreach (var controller in obstacles)
            {
                controller.Active();
            }
        }
    }
}
