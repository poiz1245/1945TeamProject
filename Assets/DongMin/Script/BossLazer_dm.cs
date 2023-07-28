using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazer_dm : MonoBehaviour
{
    [SerializeField]
    Animator lazerBodyAnim;
    [SerializeField]
    Animator lazerHeadAnim;
    public bool isLazer = true;
    float curTime = 0;
    float countTime = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime >= countTime && isLazer)
        {
            curTime = 0;
            isLazer = false;
            LazerOff();
            //gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            //Destroy(collision.gameObject);
        }
    }

    public void LazerOff()
    {
        lazerBodyAnim.SetTrigger("End");
        lazerHeadAnim.SetTrigger("End");

        Invoke("SetActiveOff", 0.3f);
    }

    void SetActiveOff()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        isLazer = true;
        curTime = 0;
    }
}
