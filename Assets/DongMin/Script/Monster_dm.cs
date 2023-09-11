using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_dm : MonoBehaviour
{
    public int hp = 50;
    float speed = 2;
    float Delay = 1.5f;
    public Transform ms;
    public Transform ms2;
    public GameObject bullet;
    //아이템 가져오기
    public GameObject Item1 = null;
    public GameObject Item2 = null;
    public GameObject exprosion;
    //Rigidbody2D rb;

    float dis = 0;
    Vector2 startPos;
    float curTime = 0;
    float endTime = 5;
    bool end = false;

    [SerializeField]
    MonsterCanvas_dm monCanvas;

    // Start is called before the first frame update
    void Start()
    {

        //rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        //한번 호출
        Invoke("CreateBullet", 1f);
        StartCoroutine("startMove");
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);
        Invoke("CreateBullet", Delay);
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime >= endTime && !end)
        {
            //Debug.Log("?????");
            StopCoroutine("startMove");
            StartCoroutine("endMove");
            end = true;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void ItemDrop()
    {
        //아이템 생성
        int rnd = 0;
        rnd = Random.Range(0, 100);

        if (rnd >= 0 && rnd < 7)
        {

            Instantiate(Item1, transform.position, Quaternion.identity);
        }
        if (rnd >=7 && rnd < 11)
        {

            Instantiate(Item2, transform.position, Quaternion.identity);
        }
        if (rnd >= 11 && rnd < 100)
        {

            return;
        }

    }

    public void Damage(int attack)
    {
        hp -= attack;

        monCanvas.Damage(hp);

        if (hp <= 0)
        {
            ItemDrop();
            Destroy(Instantiate(exprosion, transform.position, Quaternion.identity), 0.4f);
            ScoreManager.instance.monsterkill++;
            Destroy(gameObject);
        }
    }

    IEnumerator startMove()
    {
        Vector2 targetPos = startPos + Vector2.down * 3.5f;

        while (targetPos != (Vector2)transform.position)
        {
            //Debug.Log(startPos.position + ", " + transform.position);
            transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * 2);
            //transform.Translate(Vector2.down * speed * Time.deltaTime);
            yield return null;
        }
        //Debug.Log("끝");


    }

    IEnumerator endMove()
    {
        float x;
        float y = 1;
        int leftRight = Random.Range(0, 2);
        Debug.Log(leftRight);
        if (leftRight == 0)
        {
            x = -5;
        }
        else
        {
            x = 5;
        }
        int count = 0;
        while (true)
        {
            transform.Translate(new Vector2(x, y) * Time.deltaTime);
            count++;
            if (count == 10)
            {
                x = x * 0.9f;
                y = y * 1.1f;
                count = 0;
            }


            yield return null;
        }
    }

    private void OnDestroy()
    {

    }
}
