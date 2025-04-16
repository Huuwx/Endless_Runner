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
        if (this.gameObject.activeInHierarchy)
        {
            GameObject[] coinObjects = GameObject.FindGameObjectsWithTag(CONSTANT.CoinTag);
            if (useTimeCounter > 0)
            {
                useTimeCounter -= Time.deltaTime;
                foreach (GameObject coinObject in coinObjects)
                {
                    CoinController coinController = coinObject.GetComponent<CoinController>();
                    coinController.animator.SetBool(CONSTANT.X2, true);
                    coinController.SetPoint(2);
                }
            }
            else
            {
                foreach (GameObject coinObject in coinObjects)
                {
                    CoinController coinController = coinObject.GetComponent<CoinController>();
                    coinController.animator.SetBool(CONSTANT.X2, false);
                    coinController.SetPoint(1);
                }

                ClearUseTime();
            }
        }
    }
}
