using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    public float Speed = 10;
    GameObject target;
    Vector2 dir;

    void Start()
    {
        target = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        dir = target.transform.position - transform.position;
        
        if (target != null)
        {
            transform.Translate(dir.normalized*Speed*Time.deltaTime);
        }
    }


}
