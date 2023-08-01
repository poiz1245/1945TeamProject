using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMove_dm : MonoBehaviour
{
    [SerializeField]
    List<GameObject> legParts;
    [SerializeField]
    List<GameObject> linkPos;
    [SerializeField]
    GameObject hand;

    float speed = 200f;
    float cSpeed = 40f;
    public bool switchLR = true;
    float maxAngle = 90f;
    float angle = 90f;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < linkPos.Count; i++)
        //{
        //    linkPos[i] = legParts[i].transform.Find("linkPos").gameObject;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //문어 다리 마디 고정
        for (int i = 1; i < legParts.Count; i++)
        {
            legParts[i].transform.position = linkPos[i - 1].transform.position;
        }
        hand.transform.position = linkPos[legParts.Count - 1].transform.position;


        /*
        ////if (switchLR)
        ////{
        ////    speed = -speed;
        ////}
        //for (int i = 0; i < 5; i++)
        //{
        //    legParts[i].transform.Rotate(0, 0, speed * (i + 1) * Time.deltaTime);
        //}
        //for (int i = 5; i < legParts.Count; i++)
        //{
        //    legParts[i].transform.Rotate(0, 0, -speed * (i - 4) * Time.deltaTime);
        //}

        ////legParts[0].transform.rotation = Quaternion.Euler(
        ////    legParts[0].transform.rotation.eulerAngles.x,
        ////    legParts[0].transform.rotation.eulerAngles.y,
        ////    Mathf.Clamp(legParts[0].transform.rotation.eulerAngles.z, -45f, 45f));

        ////if (legParts[0].transform.rotation.eulerAngles.z >= 45f || legParts[0].transform.rotation.eulerAngles.z <= 315f)
        ////{
        ////    speed = -speed;
        ////}

        //if (switchLR)
        //    speed = cSpeed;
        //else
        //    speed = -cSpeed;


        ////legParts[0].transform.Rotate(0, 0, 5*Time.deltaTime);
        */

        legParts[0].transform.localRotation = Quaternion.Euler(0, 0, angle);
        legParts[1].transform.localRotation = Quaternion.Euler(0, 0, angle/2f);
        legParts[2].transform.localRotation = Quaternion.Euler(0, 0, 0);
        legParts[3].transform.localRotation = Quaternion.Euler(0, 0, -angle/2f);
        legParts[4].transform.localRotation = Quaternion.Euler(0, 0, -angle);
        legParts[5].transform.localRotation = Quaternion.Euler(0, 0, -angle);
        legParts[6].transform.localRotation = Quaternion.Euler(0, 0, -angle/2f);
        legParts[7].transform.localRotation = Quaternion.Euler(0, 0, 0);
        legParts[8].transform.localRotation = Quaternion.Euler(0, 0, angle/2f);
        legParts[9].transform.localRotation = Quaternion.Euler(0, 0, angle);

        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        if (Mathf.Abs(angle) >= maxAngle)
            switchLR = !switchLR;

        if (switchLR)
        {
            angle -= speed * Time.deltaTime;
        }
        else
        {
            angle += speed * Time.deltaTime;
        }


    }
}
