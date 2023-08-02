using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSJ : MonoBehaviour
{

    float startPos = -4;
    float endPos = 4;

    public Transform LeftSpawnPoint;
    public Transform RightSpawnPoint;
    public Transform RightStopSpawnPoint;
    public Transform LeftStopSpawnPoint;
    public Transform LeftCornerSpawnPoint;
    public Transform RightCornerSpawnPoint;
    public Transform BossSpawnPoint;
    public GameObject TextBossWarning;
    public GameObject Boss;


    bool swi6;
    bool swi5;
    bool swi4;
    bool swi3;
    bool swi2;
    bool swi;

    void Start()
    {
        Invoke("StartEnemySpawn", 8);
    }

    void StartEnemySpawn()
    {
        swi = true;
        swi2 = true;
        StartCoroutine(Enemy1Spawn());
        StartCoroutine(Enemy2Spawn());
        Invoke("StartArcSpawn", 7);
        Invoke("StopEnemy1Spawn", 7);
    }
    void StartArcSpawn()
    {
        swi3 = true;
        swi4 = true;
        StartCoroutine(LeftArcSpawn());
        StartCoroutine(RightArcSpawn());
        Invoke("StopArcSpawn", 12);
    }
    void StopEnemy1Spawn()
    {
        swi = false;
        StopCoroutine(Enemy1Spawn());
        Invoke("CreatEnemy1", 7);
    }

    void StopArcSpawn()
    {
        swi5 = true;
        swi3 = false;
        swi4 = false;
        StopCoroutine(LeftArcSpawn());
        StopCoroutine(RightArcSpawn());
        StartCoroutine(LeftCornerSpawn());
        Invoke("StopLeftCornerSpawn", 10);
    }

    void StopLeftCornerSpawn()
    {
        swi6 = true;
        swi5 = false;
        StopCoroutine(LeftCornerSpawn());
        StartCoroutine(RightCornerSpawn());
        Invoke("StopRightCorner", 10);
    }
    void StopRightCorner()
    {
        swi6 = false;
        StopCoroutine(RightCornerSpawn());

        swi = false;
        StopCoroutine(Enemy1Spawn());

        swi2 = false;
        StopCoroutine(Enemy2Spawn());


        Invoke("TextStart", 4);
        Invoke("TextStop", 7);
        Invoke("BossSpawn", 7);

    }

    void CreatEnemy1()
    {
        swi = true;
        StartCoroutine(Enemy1Spawn());
    }

    void SoptEnemy1Creat()
    {
        swi = false;
        StopCoroutine(Enemy1Spawn());
    }

    void TextStart()
    {
        TextBossWarning.SetActive(true);
    }
    void TextStop()
    {
        TextBossWarning.SetActive(false);
    }
    void BossSpawn()
    {
        Instantiate(Boss, BossSpawnPoint.position, Quaternion.Euler(0, 0, 180));
    }
    IEnumerator Enemy2Spawn()
    {
        while (swi2)
        {
            yield return new WaitForSeconds(5);
            float X = Random.Range(startPos, endPos);
            Vector2 spawnSpot = new Vector2(X, transform.position.y);
            GameObject monster = GameManagerSJ.Instance.pool.Get(2);
            monster.transform.position = spawnSpot;
        }
    }

    IEnumerator Enemy1Spawn()
    {
        while (swi)
        {
            yield return new WaitForSeconds(1f);
            float X = Random.Range(startPos, endPos);
            Vector2 spawnSpot = new Vector2(X, transform.position.y);
            GameObject monster = GameManagerSJ.Instance.pool.Get(3);
            monster.transform.position = spawnSpot;
            yield return new WaitForSeconds(1f);
            Enemy1Spawn2();
        }
    }
    void Enemy1Spawn2()
    {
        float X = Random.Range(startPos, endPos);
        Vector2 spawnSpot = new Vector2(X, transform.position.y);
        GameObject monster = GameManagerSJ.Instance.pool.Get(4);
        monster.transform.position = spawnSpot;

    }

    IEnumerator LeftArcSpawn()
    {
        yield return new WaitForSeconds(7);
        while (swi3)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 spawnSpot = LeftSpawnPoint.position;
            GameObject monster = GameManagerSJ.Instance.pool.Get(0);
            monster.transform.position = spawnSpot;
        }
    }

    IEnumerator RightArcSpawn()
    {
        yield return new WaitForSeconds(10);
        while (swi4)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 spawnSpot = RightSpawnPoint.position;
            GameObject monster = GameManagerSJ.Instance.pool.Get(1);
            monster.transform.position = spawnSpot;
        }
    }

    IEnumerator LeftCornerSpawn()
    {
        yield return new WaitForSeconds(5);
        while (swi5)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 spawnSpot = LeftCornerSpawnPoint.position;
            GameObject monster = GameManagerSJ.Instance.pool.Get(5);
            monster.transform.position = spawnSpot;
        }
    }

    IEnumerator RightCornerSpawn()
    {
        yield return new WaitForSeconds(5);
        while (swi6)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 spawnSpot = RightCornerSpawnPoint.position;
            GameObject monster = GameManagerSJ.Instance.pool.Get(6);
            monster.transform.position = spawnSpot;
        }
    }


}
