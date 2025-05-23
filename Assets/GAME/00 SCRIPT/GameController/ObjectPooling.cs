using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    List<GameObject> ground = new List<GameObject>();
    
    Dictionary<GameObject, List<GameObject>> _pool = new Dictionary<GameObject, List<GameObject>>();

    public GameObject GetGround()
    {
        List<GameObject> inactiveGrounds = new List<GameObject>();
        foreach (GameObject g in ground)
        {
            if (g.activeSelf)
                continue;
            
            inactiveGrounds.Add(g);
        }
        if (inactiveGrounds.Count == 0)
            return null;
        
        int groundIndex = Random.Range(0, inactiveGrounds.Count);
        return inactiveGrounds[groundIndex];
    }

    public void AddGround(GameObject g)
    {
        ground.Add(g);
    }

    public GameObject GetObject(GameObject prefab, Transform parent = null)
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
        
        GameObject g2 = Instantiate(prefab, this.transform.position, Quaternion.identity, parent);
        listObj.Add(g2);

        return g2;
    }

    public void AddObject(GameObject prefab, Transform parent = null)
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
        
        listObj.Add(prefab);
    }

    public T Getcomp<T>(T prefab) where T : MonoBehaviour
    {
        return this.GetObject(prefab.gameObject).GetComponent<T>();
    }
}
