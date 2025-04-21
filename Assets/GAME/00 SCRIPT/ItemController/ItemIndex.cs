using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemIndex : MonoBehaviour
{
    protected Vector3 startPos;
    
    public int index;

    protected virtual void Awake()
    {
        startPos = transform.localPosition;
    }

    public virtual void Activate()
    {
        transform.localPosition = startPos;
        this.gameObject.SetActive(true);
    }
}
