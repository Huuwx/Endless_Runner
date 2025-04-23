using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2CoinItemController : ItemBase
{
    // Update is called once per frame
    void Update()
    {
        ItemEffect();
    }

    protected override void ItemEffect()
    {
        if (!this.gameObject.activeInHierarchy)
            return;
        
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag(CONSTANT.CoinTag);
        if (useTimeCounter > 0)
        {
            useTimeCounter -= Time.deltaTime;
            foreach (GameObject coinObject in coinObjects)
            {
                CoinController coinController = coinObject.GetComponent<CoinController>();
                coinController.x2Activate();
                coinController.SetPoint(2);
            }
        }
        else
        {
            foreach (GameObject coinObject in coinObjects)
            {
                CoinController coinController = coinObject.GetComponent<CoinController>();
                coinController.x2Deactivate();
                coinController.SetPoint(1);
            }

            ClearUseTime();
        }
    }
}
