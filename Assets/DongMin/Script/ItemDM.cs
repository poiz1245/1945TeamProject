using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDM : MonoBehaviour
{
    public float Speed = 3f;

    int movex;
    int movey;

    int dirnox = 1;
    int dirnoy = 1;

    float limitX = 2.65f;
    float limitY = 4.83f;

    Rigidbody2D rb;
    void Start()
    {
        float randomval = Random.value;
        movex = (randomval < 0.5f) ? -1 : 1;
        movey = (randomval < 0.5f) ? -1 : 1;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.one * 100);
    }

    void Update()
    {
        //transform.Translate(new Vector2(movex* dirnox*Speed*Time.deltaTime, movey* dirnoy * Speed * Time.deltaTime));

        //transform.Translate(moveX, moveY, 0);
/*
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -limitX, limitX),
            Mathf.Clamp(transform.position.y, -limitY, limitY));*/


        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -limitX, limitX),
            Mathf.Clamp(transform.position.y, -limitY, limitY));

        if (transform.position.x >= limitX || transform.position.x <= -limitX)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }

        if (transform.position.y >= limitY || transform.position.y <= -limitY)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SideWall"))
        {
            // if (transform.position.x >= 2 || transform.position.x <= -2)
            dirnox = dirnox * -1;
        }
        if (collision.gameObject.CompareTag("UpDownWall"))
        {
            // if (transform.position.y <= -2 || transform.position.y >= 2)
            dirnoy = dirnoy * -1;
        }
        if (collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
