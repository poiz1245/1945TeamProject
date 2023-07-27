using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Elite : MonoBehaviour
{
    public Transform SpawnPos1;
    public Transform SpawnPos2;
    public Transform SpawnPos3;
    public Transform SpawnPos4;

    public Transform BossPos;
    public Transform PosA;
    public Transform PosB;

    public GameObject Rintercept;
    public GameObject Lintercept;
    public GameObject Rintercept2;
    public GameObject Lintercept2;

    public float Speed = 5f;

    Vector3 playerPos;

    bool check;
    // Start is called before the first frame update
    void Start()
    {
        check = true;
        StartCoroutine(Spawn());
        Invoke("Stop", 1f);
    }

    void Stop()
    {
        check = false;
        StopCoroutine(Spawn());

        Invoke("Creat", 3f);
    }

    void Creat()
    {
        check = true;
        StartCoroutine(Spawn());

        Invoke("Stop", 1f);

    }

    private void Update()
    {
        transform.Translate(Vector2.right * Speed* Time.deltaTime );
    }
    IEnumerator Spawn()
    {
        while (check)
        {
            yield return new WaitForSeconds(0.05f);
            //playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            // yield return new WaitForSeconds(1);
            GameObject clone1 = Instantiate(Rintercept, SpawnPos1.position, Quaternion.identity);
            //Beziercurves(clone1.transform.position);
            // yield return new WaitForSeconds(1);
            GameObject clone2 = Instantiate(Lintercept, SpawnPos2.position, Quaternion.identity);
            GameObject clone3 = Instantiate(Rintercept2, SpawnPos3.position, Quaternion.identity);
            GameObject clone4 = Instantiate(Lintercept2, SpawnPos4.position, Quaternion.identity);


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SideWall"))
            Speed *= -1;
    }

}
