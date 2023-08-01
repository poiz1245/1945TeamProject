using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFlareSJ : MonoBehaviour
{
    public GameObject destroyPos;
    public GameObject smallstar;

    public float Speed = 1f;
    public int count = 0;

    void Start()
    {
        StartCoroutine(VolumeUp());
        StartCoroutine(Move());
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
    IEnumerator VolumeUp()
    {

        float scaleSpeed = 0.02f;
        while (gameObject.transform.localScale.x < 0.8f)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.localScale = new Vector3(0.1f + scaleSpeed,
                0.1f + scaleSpeed, 0.1f + scaleSpeed);
            scaleSpeed += 0.02f;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UpDownWall") || collision.CompareTag("SideWall"))
        {
            Debug.Log("aaa");
            Instantiate(destroyPos, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }

    }
}
