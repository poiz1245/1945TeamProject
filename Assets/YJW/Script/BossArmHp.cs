using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmHp : MonoBehaviour
{
    public float HP = 70;
   
    public SpriteRenderer renderer = null;
    public Sprite[] sprites = null;

    bool isHit = false; 


    public GameObject effect;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float attack)
    {
        HP -= attack;
      
        StartCoroutine(CoolHit());
       
        if (HP < 300)
        {
            
            renderer.sprite = sprites[1];
            if (HP <= 0)
            {
                HP = 0;

                Destroy(gameObject);


                Instantiate(effect, transform.position, Quaternion.identity);

                //    Destroy(effect, 0.5f);


            }
        }
    }

    IEnumerator CoolHit()
    {
        var hit = transform.GetComponent<SpriteRenderer>();
        isHit = true;
        hit.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        hit.color = Color.white;
        isHit = false;
    }

}
