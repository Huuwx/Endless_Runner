using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<ItemBase> _listItems = new List<ItemBase>();
    [SerializeField] ItemController _item;

    public void ChangeItem(int index)
    {
        _item.SetItem(_listItems[index]);
    }

    private void OnDrawGizmosSelected()
    {
        if (_listItems.Count >= this.transform.childCount)
            return;
        
        for (int i = _listItems.Count; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<ItemBase>() != null)
            {
                _listItems.Add(this.transform.GetChild(i).gameObject.GetComponent<ItemBase>());
            }
        }
    }
}
