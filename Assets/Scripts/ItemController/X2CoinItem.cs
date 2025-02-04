using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2CoinItem : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ItemController.Instance.ResetX2UseTime();
            Destroy(gameObject);
        }
    }
}
