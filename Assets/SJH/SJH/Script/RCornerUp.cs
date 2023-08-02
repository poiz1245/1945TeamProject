using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCornerUp : MonoBehaviour
{
    public float Speed = 5;


    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(1, 1);
        movement.Normalize();
        transform.Translate(movement * Time.deltaTime * Speed);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }
}
