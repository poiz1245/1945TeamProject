using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI_dm : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = GameObject.Find("Boss_dm").GetComponent<Boss_dm>().hp;
        //slider.maxValue = GetComponentInParent<Monster_dm>().hp;
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int hp)
    {
        slider.value = hp;
    }
}
