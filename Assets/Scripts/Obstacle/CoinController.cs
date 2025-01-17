using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed  * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("+ 1");
        }
        else if(other.gameObject.tag == "Barrier")
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z);
        }
        else if(other.gameObject.tag == "Coin")
        {
            Destroy(gameObject);
        }
    }
}
