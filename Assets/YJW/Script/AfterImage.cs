using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        //gameObject.GetComponent<Renderer>().sortingOrder = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(ps != null)
        {
            ParticleSystem.MainModule main = ps.main;

            if(main.startRotation.mode == ParticleSystemCurveMode.Constant)
            {
              //  main.startRotation = -transform.eulerAngles.z * Mathf.Deg2Rad;
            }
        }
    }
}
