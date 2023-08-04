using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.ShaderData;

public class LegMove2_dm : MonoBehaviour
{
    enum LR
    {
        left,
        right
    };

    [SerializeField]
    LR lr;

    [SerializeField]
    List<GameObject> legParts;
    [SerializeField]
    List<GameObject> linkPos;
    [SerializeField]
    GameObject hand;

    float speed = 200f;
    float cSpeed = 40f;
    public bool switchLR = true;
    public float maxAngle = 60f;
    float angle;
    public float startDelay = 1;
    public bool startIndexPosSet = true;
    int startIndex = 0;

    public bool isSkill = false;
    public bool wait = true;

    Queue<Vector3>[] pastChaseLegPartRot;


    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        angle = maxAngle;
        //Debug.Log("???");
        pastChaseLegPartRot = new Queue<Vector3>[legParts.Count - 1];
        for (int i = 0; i < pastChaseLegPartRot.Length; i++)
        {
            pastChaseLegPartRot[i] = new Queue<Vector3>();
        }

        if (startIndexPosSet)
            startIndex = 0;
        else
            startIndex = legParts.Count - 1;

        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("LegMoveStart", startDelay);

    }
    private void OnEnable()
    {
        SkillEnd();

        angle = maxAngle;
        //Debug.Log("???");
        pastChaseLegPartRot = new Queue<Vector3>[legParts.Count - 1];
        for (int i = 0; i < pastChaseLegPartRot.Length; i++)
        {
            pastChaseLegPartRot[i] = new Queue<Vector3>();
        }

        if (startIndexPosSet)
            startIndex = 0;
        else
            startIndex = legParts.Count - 1;

        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("LegMoveStart", startDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //문어 다리 마디 고정
        for (int i = 1; i < legParts.Count; i++)
        {
            legParts[i].transform.position = linkPos[i - 1].transform.position;
        }
        //hand.transform.position = linkPos[legParts.Count - 1].transform.position;

        if (!isSkill)
        {
            legParts[startIndex].transform.localRotation = Quaternion.Euler(0, 0, angle);

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
        else if(isSkill && wait)
        {
            wait = false;
            StopAllCoroutines();

            if (lr == LR.left)
            {
                legParts[0].transform.localRotation = Quaternion.Euler(0, 0, -70);
                legParts[1].transform.localRotation = Quaternion.Euler(0, 0, -70);
                legParts[2].transform.localRotation = Quaternion.Euler(0, 0, -66);
                legParts[3].transform.localRotation = Quaternion.Euler(0, 0, -56);
                legParts[4].transform.localRotation = Quaternion.Euler(0, 0, -38);
                legParts[5].transform.localRotation = Quaternion.Euler(0, 0, -21);
                legParts[6].transform.localRotation = Quaternion.Euler(0, 0, -9.6f);
                legParts[7].transform.localRotation = Quaternion.Euler(0, 0, 50);
                legParts[8].transform.localRotation = Quaternion.Euler(0, 0, 63);
                legParts[9].transform.localRotation = Quaternion.Euler(0, 0, 71);
                legParts[10].transform.localRotation = Quaternion.Euler(0, 0, 66);
            }
            else if (lr == LR.right)
            {
                legParts[0].transform.localRotation = Quaternion.Euler(0, 0, 70);
                legParts[1].transform.localRotation = Quaternion.Euler(0, 0, 70);
                legParts[2].transform.localRotation = Quaternion.Euler(0, 0, 66);
                legParts[3].transform.localRotation = Quaternion.Euler(0, 0, 56);
                legParts[4].transform.localRotation = Quaternion.Euler(0, 0, 38);
                legParts[5].transform.localRotation = Quaternion.Euler(0, 0, 21);
                legParts[6].transform.localRotation = Quaternion.Euler(0, 0, 9.6f);
                legParts[7].transform.localRotation = Quaternion.Euler(0, 0, -50);
                legParts[8].transform.localRotation = Quaternion.Euler(0, 0, -63);
                legParts[9].transform.localRotation = Quaternion.Euler(0, 0, -71);
                legParts[10].transform.localRotation = Quaternion.Euler(0, 0, -66);
            }

            //Invoke("SkillEnd", 3);


            //wait = false;

            //Vector3 dir = player.transform.position - legParts[10].transform.position;
            //Debug.Log(dir);
            //float lookAtAngle = Mathf.Atan2(
            //    player.transform.position.y - legParts[10].transform.position.y,
            //    player.transform.position.x - legParts[10].transform.position.x) * Mathf.Rad2Deg;

            //legParts[10].transform.localRotation = Quaternion.Euler(0,0, lookAtAngle+90);
            ////Invoke("SkillEnd", 4);
        }

    }

    public void SkillEnd()
    {
        isSkill = false;
        wait = true;


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
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            legPart.transform.rotation = Quaternion.Euler(pastChaseLegPartRot[pastChaseLegPartRotIndex].Dequeue());

            yield return null;
            //yield return new WaitForEndOfFrame();
        }
    }

    void LegMoveStart()
    {
        if (startIndex == 0)
        {
            for (int i = 1; i < legParts.Count; i++)
            {
                StartCoroutine(LegRotChase(legParts[i], legParts[i - 1], i - 1));
            }
        }
        else
        {
            for (int i = legParts.Count - 1; i > 0; i--)
            {
                StartCoroutine(LegRotChase(legParts[i - 1], legParts[i], i - 1));
            }
        }


        StartCoroutine(MaxAngleChange());
        
    }

    IEnumerator MaxAngleChange()
    {
        float maxMaxAngle = 110f;
        float minMaxAngle = 60f;
        float speed = 8;
        bool maxAngleSwitch = true;

        while (true)
        {
            maxAngle = Mathf.Clamp(maxAngle, minMaxAngle, maxMaxAngle);

            if (maxAngle >= maxMaxAngle)
                maxAngleSwitch = true;
            else if (maxAngle <= minMaxAngle)
                maxAngleSwitch = false;

            if (maxAngleSwitch)
            {
                maxAngle -= speed * Time.deltaTime;
            }
            else
            {
                maxAngle += speed * Time.deltaTime;
            }

            yield return null;
        }
    }
}
