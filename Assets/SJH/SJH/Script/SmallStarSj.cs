using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallStarSj : MonoBehaviour
{
    public float Speed = 5;
    Vector2 vec2 = Vector2.down;


    void Update()
    {
       
        transform.Translate(vec2 * Speed * Time.deltaTime);
    }


    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
