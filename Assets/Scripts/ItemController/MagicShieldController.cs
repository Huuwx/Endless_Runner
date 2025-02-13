using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShieldController : Item
{
    private void Update()
    {
        if(useTimeCounter > 0)
        {
            useTimeCounter -= Time.deltaTime;
        }
        else
        {
            ItemController.Instance.OutOfTimeToUseMagicShield();
        }
    }
}
