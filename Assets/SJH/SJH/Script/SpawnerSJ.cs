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

    bool swi6;
    bool swi5;
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
        Invoke("StopArcSpawn", 5);
    }

    void StopArcSpawn()
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
        swi5 = true;
        swi4 = false;
      
        StartCoroutine(LeftCornerSpawn());
        StopCoroutine (LeftEnemy3Spawn());

        Invoke("LeftCornerStop", 7);
        //Invoke("StopArcSpawn", 5);
    }
    void Enemy2SpawnCreat()
    {
        swi3 = true;
        StartCoroutine(Enemy2Spawn());
        Invoke("StopEnemy2Spawn", 15);
    }

    void LeftCornerStop()
    {
        swi6 = true;
        swi5 = false;
        StopCoroutine(LeftCornerSpawn());
        StartCoroutine(RightCornerSpawn());
        
        Invoke("RightCornerStop", 5);
    }
    void RightCornerStop()
    {
        swi = true;
        swi2 = true;
        swi6 = false;
        StopCoroutine(RightCornerSpawn());
        StartCoroutine(LeftArcSpawn());
        StartCoroutine(RightArcSpawn());

        Invoke("StopArcSpawn", 7);
    }
    IEnumerator LeftArcSpawn()
    {
        yield return new WaitForSeconds(3f);
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
        yield return new WaitForSeconds(3f);
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
    IEnumerator LeftCornerSpawn()
    {
        yield return new WaitForSeconds(2f);
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
        yield return new WaitForSeconds(2.5f);
        while (swi6)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 spawnSpot = RightCornerSpawnPoint.position;
            GameObject monster = GameManagerSJ.Instance.pool.Get(6);
            monster.transform.position = spawnSpot;
        }
    }

    void RighttEnemy3Spawn()
    {
        Vector2 spawnSpot = RightStopSpawnPoint.position;
        GameObject monster = GameManagerSJ.Instance.pool.Get(4);
        monster.transform.position = spawnSpot;
    }
}
