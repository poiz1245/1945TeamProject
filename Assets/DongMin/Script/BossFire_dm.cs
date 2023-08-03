using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire_dm : MonoBehaviour
{
    float rotationMaxZ = 49.52f;
    float rotationMinZ = -49.52f;
    float differenceRot;
    float curRot;

    float countTime = 4f;
    float curTime = 0;

    [SerializeField]
    GameObject bossBody;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationMaxZ));

        differenceRot = rotationMaxZ - rotationMinZ;

        //Debug.Log(transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        curRot = (differenceRot * (1.0f - curTime / countTime)) - rotationMaxZ;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRot));

        if (curTime >= countTime)
        {
            curTime = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        curTime = 0;
    }

    private void OnDisable()
    {
        if (bossBody.activeSelf)
        {
            bossBody.GetComponent<Boss_dm>().corBossBulletStart();
        }
    }
}
