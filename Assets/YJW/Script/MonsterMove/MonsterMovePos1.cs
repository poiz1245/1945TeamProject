using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovePos1 : MonoBehaviour
{
    GameObject ms;
    public float Speed = 3;
    void Start()
    {
        ms = GameObject.Find("Paze1Pos1");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ms.transform.position, Speed * Time.deltaTime);
    }
}
