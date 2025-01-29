using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : BarrierController
{
    private Rigidbody rb;

    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            Vector3 forwardMove = transform.right * -10 * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
        }
    }
}
