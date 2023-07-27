using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Lazor : MonoBehaviour
{
    Transform PosA;
    public float Speed=5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PosA = GameObject.FindGameObjectWithTag("Elite").transform.Find("gun").transform;

        transform.position = new Vector2(PosA.position.x,transform.position.y);
        transform.Translate(new Vector2(transform.position.x, -1f * Speed * Time.deltaTime));
    }
}
