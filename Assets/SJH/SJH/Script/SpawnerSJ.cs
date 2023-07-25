using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSJ : MonoBehaviour
{

    float startPos = -4;
    float endPos = 4;
    int stage;

    public Transform LeftSpawnPoint;
    public Transform RightSpawnPoint;
    public Transform RightStopSpawnPoint;
    public Transform LeftStopSpawnPoint;
    public Transform LeftCornerSpawnPoint;
    public Transform RightCornerSpawnPoint;


    bool swi4;
    bool swi3;
    bool swi2;
    bool swi;

    void Start()
    {
        swi2 = true;
        swi = true;
        swi3 = true;
        StartCoroutine(Enemy2Spawn());
        StartCoroutine(LeftArcSpawn());
        StartCoroutine(RightArcSpawn());
        Invoke("StopEnemy2Spawn", 15);
        Invoke("StopArcSpaw", 5);
    }

    void StopArcSpaw()
    {
        swi = false;
        swi2 = false;
        swi4 = true;
        StopCoroutine(LeftArcSpawn());
        StopCoroutine(RightArcSpawn());
        StartCoroutine(LeftEnemy3Spawn());
        Invoke("ArcSpawnCreat", 10);
    }
    void StopEnemy2Spawn() 
    {
        swi3 = false;
        StopCoroutine(Enemy2Spawn());
        Invoke("Enemy2SpawnCreat", 7);
    }
    void ArcSpawnCreat()
    {
        swi4 = false;
        swi= true;
        swi2= true;
        StartCoroutine(LeftArcSpawn());
        StartCoroutine(RightArcSpawn());
        StopCoroutine (LeftEnemy3Spawn());
        Invoke("StopArcSpaw", 5);
    }
    void Enemy2SpawnCreat()
    {
        swi3 = true;
        StartCoroutine(Enemy2Spawn());
        Invoke("StopEnemy2Spawn", 15);
    }
    IEnumerator LeftArcSpawn()
    {
        while (swi)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 spawnSpot = LeftSpawnPoint.position;
            GameObject monster = GameManagerSJ.Instance.pool.Get(0);
            monster.transform.position = spawnSpot; 
        }
    }
    IEnumerator RightArcSpawn()
    {
        while (swi2)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 spawnSpot = RightSpawnPoint.position;
            GameObject monster = GameManagerSJ.Instance.pool.Get(1);
            monster.transform.position = spawnSpot;
        }
    }
    IEnumerator Enemy2Spawn()
    {
        while (swi3)
        {
            yield return new WaitForSeconds(7);
            float X = Random.Range(startPos, endPos);
            Vector2 spawnSpot = new Vector2(X, transform.position.y);
            GameObject monster = GameManagerSJ.Instance.pool.Get(2);
            monster.transform.position = spawnSpot;
        }
    }

    IEnumerator LeftEnemy3Spawn()
    {
        while (swi4)
        {
            yield return new WaitForSeconds(1);
            Vector2 spawnSpot = LeftStopSpawnPoint.position;
            GameObject monster = GameManagerSJ.Instance.pool.Get(3);
            monster.transform.position = spawnSpot;
            yield return new WaitForSeconds(1);
            RighttEnemy3Spawn();
        }
    }
    void RighttEnemy3Spawn()
    {
        Vector2 spawnSpot = RightStopSpawnPoint.position;
        GameObject monster = GameManagerSJ.Instance.pool.Get(4);
        monster.transform.position = spawnSpot;
    }
}
