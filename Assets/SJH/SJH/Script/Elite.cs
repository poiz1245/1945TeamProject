using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Elite : MonoBehaviour
{
    public Transform SpawnPos1;
    public Transform SpawnPos2;
    public Transform SpawnPos3;
    public Transform SpawnPos4;
    public Transform gunPos;
    public Transform gun2Pos;
    public Transform gun3Pos;
    public Transform BossPos;



    public GameObject Rintercept;
    public GameObject Lintercept;
    public GameObject Rintercept2;
    public GameObject Lintercept2;
    public GameObject Lazor;
    public GameObject bullet;
    public GameObject StarBullet;
    public float Speed = 5f;
    public float Hp = 1000;
    Vector3 playerPos;
    GameObject beam;
    bool check;

    // Start is called before the first frame update
    private void Awake()
    {
        beam = GameObject.Find("EffBeam").transform.Find("gliter").gameObject;
    }
    void Start()
    {
        Instantiate(StarBullet, gunPos.position, Quaternion.identity);
        
        check = true;
        beam.SetActive(true);

        StartCoroutine(Spawn());
        StartCoroutine(CreatBullet());

        Invoke("Stop", 1f);
        Invoke("BeamStop", 2f);
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

    void BeamStop()
    {
        beam.SetActive(false);
        Invoke("BeamCreat", 2f);
    }
    void BeamCreat()
    {
        beam.SetActive(true);
        Invoke("BeamStop", 2f);
    }
    private void Update()
    {
        transform.Translate(Vector2.right * Speed* Time.deltaTime );
        if(Hp <= 0)
        {
            Destroy(gameObject);
            ScoreManager.instance.monsterkill++;
            ScoreManager.instance.Bonus++;
        }
    }


    void OnDestroy()
    {
        ScoreManager.instance.UpdateScore();
    }

    IEnumerator Spawn()
    {
        while (check)
        {
            yield return new WaitForSeconds(0.05f);
           
            GameObject clone1 = Instantiate(Rintercept, SpawnPos1.position, Quaternion.identity);
            GameObject clone2 = Instantiate(Lintercept, SpawnPos2.position, Quaternion.identity);
            GameObject clone3 = Instantiate(Rintercept2, SpawnPos3.position, Quaternion.identity);
            GameObject clone4 = Instantiate(Lintercept2, SpawnPos4.position, Quaternion.identity);
            

        }

    }

    IEnumerator CreatBullet()
    {
        int count = 9;
        float intervalAngle = 360 / count;
        float weightAngle = 0;

        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject clone = Instantiate(bullet, gun2Pos.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                clone.GetComponent<ArcBulletSJ>().Move(new Vector2(x, -y-3));
            }

            yield return new WaitForSeconds(3f);

            for (int i = 0; i < count; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject clone2 = Instantiate(bullet, gun3Pos.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                clone2.GetComponent<ArcBulletSJ>().Move(new Vector2(x, -y - 3));
            }

            weightAngle += 1;

            yield return new WaitForSeconds(5);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SideWall"))
            Speed *= -1;
        if (collision.gameObject.CompareTag("PlayerBullet"))
            Hp -= GameManagerSJ.Instance.player.AttackPower;
        if(collision.gameObject.CompareTag("HomingMissle"))
            Hp -= GameManagerSJ.Instance.player.AttackPower*2;
    }

}
