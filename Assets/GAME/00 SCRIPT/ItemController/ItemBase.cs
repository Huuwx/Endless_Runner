using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] protected float useTimeMax = 5;
    protected float useTimeCounter;

    protected virtual void Awake()
    {
        useTimeCounter = 0;
    }

    private void OnEnable()
    {
        useTimeCounter = useTimeMax;
    }

    public virtual void ClearUseTime()
    {
        useTimeCounter = 0;
        this.gameObject.SetActive(false);
    }
    
    protected abstract void ItemEffect();
}
