using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer_dm : MonoBehaviour
{
    [SerializeField]
    Animator lazerBodyAnim;
    [SerializeField]
    Animator lazerHeadAnim;
    [SerializeField]
    GameObject exprosionPrefab;
    public Transform playerFireTransform = null;
    public bool isLazer = true;
    int attack = 40;
    float curTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerFireTransform.position;

        if (!isLazer)
        {
            lazerBodyAnim.SetTrigger("End");
            lazerHeadAnim.SetTrigger("End");
            Destroy(gameObject, 0.3f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {

            curTime += Time.deltaTime;

            if (curTime >= 0.1f)
            {
                //Debug.Log("Æ½¹ßµ¿µÊ");
                curTime = 0;
                if (collision.GetComponent<Monster_dm>() != null)
                    collision.GetComponent<Monster_dm>().Damage(attack);
                else if(collision.GetComponent<Boss_dm>() != null)
                    collision.GetComponent<Boss_dm>().Damage(attack);
                else if (collision.GetComponent<BossArm_dm>() != null)
                    collision.GetComponent<BossArm_dm>().Damage(attack);
                else if (collision.GetComponent<Octopus_dm>() != null)
                    collision.GetComponent<Octopus_dm>().Damage(attack);

                Debug.Log("¼³¸¶?");    
            }

        }
    }
}
