using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg_dm : MonoBehaviour
{
    GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position;
    }
}
