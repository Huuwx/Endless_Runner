using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCoinController : MonoBehaviour
{
    public Transform target;
    public GameObject PointCheck;
    public Vector3 sizePointCheck;

    [SerializeField] private float useTimeMax;
    public float useTimeCounter;

    private void Awake()
    {
        useTimeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (useTimeCounter > 0)
        {
            useTimeCounter -= Time.deltaTime;
            CheckCoin();
        }
        else
        {
            ItemController.Instance.OutOfTimeToUseMagnet();
        }
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

    public void ResetUseTime()
    {
        useTimeCounter = useTimeMax;
    }
}
