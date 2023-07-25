using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmHp : MonoBehaviour
{
    public float HP = 70;
    public SpriteRenderer renderer = null;
    public Sprite[] sprites = null;


    public GameObject effect;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int attack)
    {
        HP -= attack;
       
        if (HP < 50)
        {
            Debug.Log("데미지 받았음");
            Debug.Log(HP);
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

}
