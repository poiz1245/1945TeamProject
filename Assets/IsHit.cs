using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHit : MonoBehaviour
{ 
    SpriteRenderer colorren;
    bool ishit = false;
    Boss hit;

    void Start()
    {
        colorren = GetComponent<SpriteRenderer>();
        hit = GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit.isHit)
        {
            StartCoroutine(Hitcool());
        }
    }

    //
    //
    IEnumerator Hitcool()
    {
        if (!ishit)
        {
            ishit = true;
            colorren.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            colorren.color = Color.white;
            ishit = false;
        }
    }
}
