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
}
