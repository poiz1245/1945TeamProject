using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerSJ : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;
    // Start is called before the first frame update
    void Start()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index =0; index < prefabs.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject prefab in prefabs) 
        {
            if(!prefab.activeSelf)
            {
                select = prefab;
                select.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

