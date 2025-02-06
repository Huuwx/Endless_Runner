using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2CoinItemController : MonoBehaviour
{
    [SerializeField] private float useTimeMax = 5;
    public float useTimeCounter;

    private void Awake()
    {
        useTimeCounter = 0;
    }

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
                coinController.point = 2;
            }
        }
        else
        {
            foreach (GameObject coinObject in coinObjects)
            {
                CoinController coinController = coinObject.GetComponent<CoinController>();
                coinController.animator.SetBool("X2", false);
                coinController.point = 1;
            }
        }
    }

    public void ResetUseTime()
    {
        useTimeCounter = useTimeMax;
    }
}
