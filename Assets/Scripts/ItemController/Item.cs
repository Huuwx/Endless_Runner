using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected float useTimeMax = 5;
    public float useTimeCounter;

    protected virtual void Awake()
    {
        useTimeCounter = 0;
    }

    public virtual void ResetUseTime()
    {
        useTimeCounter = useTimeMax;
    }

    public virtual void ClearUseTime()
    {
        useTimeCounter = 0;
    }
}
