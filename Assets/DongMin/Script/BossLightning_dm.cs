using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLightning_dm : MonoBehaviour
{
    float startRotation = 45f;
    float maxRot = 360f;
    float curRot;

    float countTime = 8f;
    float curTime = 0;

    [SerializeField]
    GameObject bossBody;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, startRotation));

        //Debug.Log(transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        curRot = (maxRot * (curTime / countTime)) + startRotation;
        //Debug.Log("z rot : "+curRot);
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
