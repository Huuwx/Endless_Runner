using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTriggerItem : ItemIndex
{
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    public override void Activate()
    {
        base.Activate();
        this.transform.rotation = Quaternion.identity;
        rb.excludeLayers &= ~(1 << LayerMask.NameToLayer("Player"));
    }
}
