using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float curTime = 0;
    bool switchLR = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        if (switchLR)
        {
            transform.Translate(0.001f, 0, 0);
               
        }
        else
        {
            transform.Translate(0.001f, 0, 0);
        }

        if (curTime >= 1f)
        {
            curTime = 0;
            switchLR = false;
        }

    }
}
