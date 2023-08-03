using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCraft : MonoBehaviour
{
    Rigidbody2D rigid;

    public float count = 0;
    public float gravityScale = 0.3f;
    public float startTime = 4;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if(count > startTime) 
        {
            rigid.gravityScale = gravityScale;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
