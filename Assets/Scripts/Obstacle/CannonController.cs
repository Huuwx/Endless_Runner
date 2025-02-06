using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : BarrierController
{
    public bool isActive = false;

    void FixedUpdate()
    {
        if (isActive)
        {
            transform.Translate(Vector3.back * 20 * Time.deltaTime, Space.World);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.Instance.Die();
            collision.rigidbody.AddForce(new Vector3(0, 1, -1) * 5, ForceMode.Impulse);
            isActive = false;
        }
    }
}
