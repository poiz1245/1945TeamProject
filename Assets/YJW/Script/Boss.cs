using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Boss : Monster
{
  


    public GameObject BossP1;
    public GameObject BossP2;
    public GameObject BossP3;

    public void ItemDrop()
    {

    }
        private void Awake()
    {
        StartCoroutine(Think());

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Think() //패턴 코루틴 함수임
    {
        yield return new WaitForSeconds(0.1f);

        int rendAction = Random.Range(0, 3);
        switch (rendAction)
        {
            case 0:

            case 1:
                StartCoroutine(Bosspattern1());


                break;

            case 2:

            case 3:
                StartCoroutine(Bosspattern2());

                break;
           



        }

        IEnumerator Bosspattern1()
        {
           
            BossP1.GetComponent<BossPattern1>().Shot();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Think());
        }

        IEnumerator Bosspattern2()
        {
            
            BossP2.GetComponent<BossPattern2>().Shot();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Think());
        }




        // Update is called once per frame
        void Update()
        {
            
        }




    }
}
