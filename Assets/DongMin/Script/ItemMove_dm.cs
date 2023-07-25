using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove_dm : MonoBehaviour
{
    float limitX = 2.556f;
    float limitY = 4.826f;
    Rigidbody2D rb;
    float speed = 100f;
    float curTime = 0;
    float limitTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //transform.Translate(Vector2.one);
        rb.AddForce(Vector2.one * speed);
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime < limitTime)
        {
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

    }
}
