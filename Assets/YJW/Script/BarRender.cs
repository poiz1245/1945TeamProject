using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarRender : MonoBehaviour
{
    Image image;
   // public SpriteRenderer ren = null;
    public Sprite[] sprites = null;
    public int stack = 0;
    GameObject player;
    public float energyval = 0;
    
    void Start()
    {
       // ren = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        StackCompare();
        StackCheckImage();
        StackCheck();


    }

    public void StackCompare()
    {
        energyval = player.gameObject.GetComponent<PlayerSJ>().energyValue;


    }

    private void StackCheckImage()
    {
       

        if (stack == 0)
        {
            image.sprite = sprites[0];
        }
        else if (stack == 1)
        {
            image.sprite = sprites[1];
        }
        else if (stack == 2)
        {
            image.sprite = sprites[2];
        }
        else if (stack == 3)
        {
            image.sprite = sprites[3];
        }
    }

    public void SetStack(int stack2)
    {
        stack2 = this.stack;
    }

    private void StackCheck()
    {
        if (0<= energyval && energyval < 0.19f)
        {
            stack = 0;
        }
        else if (0.19f <= energyval && energyval <0.47f)
        {
            stack = 1;
        }
        else if (0.47f <= energyval && energyval < 1f)
        {
            stack = 2;
        }
        else if(energyval ==1f)
        {
            stack = 3;
        }
    }
}
