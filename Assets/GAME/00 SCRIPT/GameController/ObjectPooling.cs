using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling instance;
    public static ObjectPooling Instance { get { return instance; } }
    
    Dictionary<GameObject, List<GameObject>> _pool = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        List<GameObject> listObj = new List<GameObject>();
        if (_pool.ContainsKey(prefab))
        {
            listObj = _pool[prefab];
        }
        else
        {
            _pool.Add(prefab, listObj);
        }

        foreach (var g in  listObj)
        {
            if(g.activeSelf)
                continue;

            return g;
        }
        
        GameObject g2 = Instantiate(prefab, this.transform.position, Quaternion.identity);
        listObj.Add(g2);

        return g2;
    }

    public T Getcomp<T>(T prefab) where T : MonoBehaviour
    {
        return this.GetObject(prefab.gameObject).GetComponent<T>();
    }
}
