using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class StarFlareSJ : MonoBehaviour
{
    public GameObject destroyPos;
    public GameObject smallstar;
    GameObject Elite;
    public float Speed = 1f;
    public int count = 0;

    void Start()
    {
        Elite = GameObject.FindWithTag("Elite");
        StartCoroutine(Move());
    }
    private void Update()
    {
        if (Elite.GetComponent<Elite>().check_smallstar == false)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Move()
    {
        yield return new WaitForSeconds(3);

        while (true)
        {
            transform.Translate(Vector2.down * Speed * Time.deltaTime);
            yield return null;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UpDownWall"))
        {
            Instantiate(destroyPos, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        //if (collision.CompareTag("Player"))
            //GameManagerSJ.Instance.player.Heart--;

    }
}


