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

    public override void ResetActive()
    {
        base.ResetActive();
        animator.SetBool(CONSTANT.Activate, false);
    }

    public override void Active()
    {
        animator.SetBool(CONSTANT.Activate, true);
    }
}
