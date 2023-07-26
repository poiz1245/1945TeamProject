using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSJ : MonoBehaviour
{
    public float Speed = 1f;

    Vector2 vec2 = Vector2.down;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(vec2 * Speed* Time.deltaTime);
    }
    public void OnMove(Vector2 vec)
    {
        vec2 = vec;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }


}
