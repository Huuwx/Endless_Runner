using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeTrapController : ObstacleBase
{
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Activate()
    {
        base.Activate();
        animator.SetBool(CONSTANT.Activate, false);
    }
}
