using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSJ : MonoBehaviour
{
    public float Speed = 3f;

    int movex;
    int movey;

    int dirnox = 1;
    int dirnoy = 1; 
    void Start()
    {
        float randomval = Random.value;
        movex = (randomval < 0.5f) ? -1 : 1;
        movey = (randomval < 0.5f) ? -1 : 1;
    }

    void Update()
    {
        transform.Translate(new Vector2(movex* dirnox*Speed*Time.deltaTime, movey* dirnoy * Speed * Time.deltaTime));
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
