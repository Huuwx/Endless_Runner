using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] ItemBase _item;
    
    public void SetItem(ItemBase item)
    {
        _item = item;
    }
    
    public void ItemUseTime()
    {
        _item.ResetCounterTime();
        _item.gameObject.SetActive(true);
    }
}
