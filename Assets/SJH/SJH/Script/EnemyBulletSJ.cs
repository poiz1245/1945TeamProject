using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSJ : MonoBehaviour
{
    public float Speed = 3f;

    public GameObject target;
    Vector2 dir;
    Vector2 dirNo;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            dir = target.transform.position - transform.position;
            dirNo = dir.normalized;
        }
        else
        {
            dir = Vector2.down;
            dirNo = dir.normalized;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dirNo*Speed*Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
