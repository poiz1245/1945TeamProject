using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class LegMove2_dm : MonoBehaviour
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
    float maxAngle = 60f;
    float angle = 60f;

    Queue<Vector3>[] pastChaseLegPartRot;

    // Start is called before the first frame update
    void Start()
    {
        pastChaseLegPartRot = new Queue<Vector3>[legParts.Count - 1];
        for (int i = 0; i < pastChaseLegPartRot.Length; i++)
        {
            pastChaseLegPartRot[i] = new Queue<Vector3>();
        }

        for (int i = legParts.Count - 1; i > 0; i--)
        {
            StartCoroutine(LegRotChase(legParts[i - 1], legParts[i], i - 1));
        }
        
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


    //List<Vector3> pastChaseLegPartRot = new List<Vector3>();
    IEnumerator LegRotChase(GameObject legPart, GameObject chaseLegPart, int pastChaseLegPartRotIndex)
    {
        StartCoroutine(LegRotMove(legPart, pastChaseLegPartRotIndex));

        while (true)
        {
            pastChaseLegPartRot[pastChaseLegPartRotIndex].Enqueue(chaseLegPart.transform.rotation.eulerAngles);

            yield return null;
        }
    }

    IEnumerator LegRotMove(GameObject legPart, int pastChaseLegPartRotIndex)
    {
        yield return new WaitForSeconds(0.2f);

        while (true)
        {
            legPart.transform.rotation = Quaternion.Euler(pastChaseLegPartRot[pastChaseLegPartRotIndex].Dequeue());


            yield return null;
        }
    }
}
