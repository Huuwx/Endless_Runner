using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    List<ItemIndex> items = new List<ItemIndex>();
    List<CoinController> coins = new List<CoinController>();

    private void Start()
    {
        ItemIndex[] i = this.GetComponentsInChildren<ItemIndex>();
        foreach (ItemIndex item in i)
        {
            items.Add(item);
        }

        CoinController[] co = this.GetComponentsInChildren<CoinController>();
        foreach (CoinController coin in co)
        {
            coins.Add(coin);
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
    }
}
