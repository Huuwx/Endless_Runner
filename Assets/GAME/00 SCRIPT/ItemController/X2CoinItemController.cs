using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2CoinItemController : Item
{
    // Update is called once per frame
    void Update()
    {
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag("Coin");
        if (useTimeCounter > 0)
        {
            useTimeCounter -= Time.deltaTime;
            foreach (GameObject coinObject in coinObjects)
            {
                CoinController coinController = coinObject.GetComponent<CoinController>();
                coinController.animator.SetBool("X2", true);
                coinController.SetPoint(2);
            }
        }
        else
        {
            foreach (GameObject coinObject in coinObjects)
            {
                CoinController coinController = coinObject.GetComponent<CoinController>();
                coinController.animator.SetBool("X2", false);
                coinController.SetPoint(1);
            }
        }
    }
}
