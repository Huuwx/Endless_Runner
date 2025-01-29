using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapController : BarrierController
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
