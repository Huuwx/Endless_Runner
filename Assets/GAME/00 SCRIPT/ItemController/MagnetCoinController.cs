using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCoinController : ItemBase
{
    [SerializeField] Transform target;
    [SerializeField] GameObject PointCheck;
    [SerializeField] Vector3 sizePointCheck;

    // Update is called once per frame
    void Update()
    {
        ItemEffect();
    }

    public void CheckCoin()
    {
        Collider[] colliders = Physics.OverlapBox(PointCheck.transform.position, sizePointCheck);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Coin")
            {
                collider.gameObject.transform.position = Vector3.MoveTowards(collider.gameObject.transform.position, target.position, 30 * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(PointCheck.transform.position, sizePointCheck);
    }

    protected override void ItemEffect()
    {
        if (useTimeCounter > 0)
        {
            useTimeCounter -= Time.deltaTime;
            CheckCoin();
        }
        else
        {
            ClearUseTime();
        }
    }
}
