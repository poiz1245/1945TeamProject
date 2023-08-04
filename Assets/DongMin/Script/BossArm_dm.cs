using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArm_dm : MonoBehaviour
{
    enum LeftRight
    {
        Left,
        Right
    }

    [SerializeField]
    LeftRight leftRight = LeftRight.Left;

    [SerializeField]
    GameObject BossBody;
    Boss_dm boss_dm;
    [SerializeField]
    GameObject destroyArm;

    [SerializeField]
    GameObject[] WarningArea;
    [SerializeField]
    GameObject BossAttack;

    [SerializeField]
    GameObject exprosionPrefab;

    public int hp = 500;
    int maxHp;
    float Delay = 0.5f;
    public GameObject bullet;
    public Transform ms;

    Coroutine AttackCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        boss_dm = BossBody.GetComponent<Boss_dm>();

        for (int i = 0; i < WarningArea.Length; i++)
        {
            WarningArea[i].SetActive(false);
        }
        BossAttack.SetActive(false);

        //한번 호출
        //Invoke("CreateBullet", 0.1f);

        maxHp = hp;
        if (leftRight == LeftRight.Right)
            BossUI_dm.instance.StartSet(BossUI_dm.HP.rightArm, maxHp);
        else if (leftRight == LeftRight.Left)
            BossUI_dm.instance.StartSet(BossUI_dm.HP.leftArm, maxHp);

        StartCoroutine("BossArmPattern");
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms.position, Quaternion.identity);
        Invoke("CreateBullet", Delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public void Damage(int attack)
    {
        hp -= attack;
        if (leftRight == LeftRight.Right)
            BossUI_dm.instance.Damage(BossUI_dm.HP.rightArm, hp);
        else if (leftRight == LeftRight.Left)
            BossUI_dm.instance.Damage(BossUI_dm.HP.leftArm, hp);

        if (hp <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    IEnumerator BossArmPattern()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            //yield return new WaitForSeconds(15f);

            if (leftRight == LeftRight.Right)
                AttackCoroutine = StartCoroutine(boss_dm.AttackWarning(WarningArea, BossAttack));

            yield return new WaitForSeconds(12f);

            if (leftRight == LeftRight.Left)
                AttackCoroutine = StartCoroutine(boss_dm.AttackWarning(WarningArea, BossAttack));

            yield return new WaitForSeconds(15f);
        }


    }

    private void OnDisable()
    {

        if (AttackCoroutine != null)
        {
            StopCoroutine(AttackCoroutine);
        }
        StopCoroutine("BossArmPattern");

        BossBody.GetComponent<Boss_dm>().destroyArmCount++;

        destroyArm.SetActive(true);

        Destroy(Instantiate(exprosionPrefab, transform.position, Quaternion.identity), 0.4f);
    }
}
