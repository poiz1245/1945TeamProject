using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSJ : MonoBehaviour
{
    public float Speed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Speed* Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
            Destroy(gameObject);
    }


}