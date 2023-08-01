using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject shadow;
    // Start is called before the first frame update
    void Start()
    {
        shadow.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
