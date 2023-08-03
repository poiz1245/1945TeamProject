using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBackground_dm : MonoBehaviour
{
    [SerializeField]
    GameObject background1;
    [SerializeField]
    GameObject background2;

    [SerializeField]
    float speed = 10f;

    float yLength;

    // Start is called before the first frame update
    void Start()
    {
        //background1.transform.position = new Vector2(0,)
        yLength = background1.GetComponent<BoxCollider2D>().bounds.size.y;
        Debug.Log(yLength);

        background1.transform.position = Vector2.zero;
        background2.transform.position = new Vector2(0, yLength);
    }

    // Update is called once per frame
    void Update()
    {
        background1.transform.Translate(0, -speed * Time.deltaTime, 0);
        background2.transform.Translate(0, -speed * Time.deltaTime, 0);

        if (background1.transform.position.y <= -20)
        {
            background1.transform.position = new Vector2(0, background2.transform.localPosition.y + yLength);
        }
        if (background2.transform.position.y <= -20)
        {
            background2.transform.position = new Vector2(0, background1.transform.localPosition.y + yLength);
        }
    }
}
