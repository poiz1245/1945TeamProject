using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerBulletSJ : MonoBehaviour
{
    public float Speed = 1f;

    Vector2 vec2 = Vector2.down;
    public float enegy = 1;

    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

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
        if (collision.CompareTag("Enemy") || collision.CompareTag("Elite") || collision.CompareTag("InterCepter"))
        {
            
            player.GetComponent<PlayerSJ>().GazyPower(enegy);
            Destroy(gameObject);
            
        }
    }


}
