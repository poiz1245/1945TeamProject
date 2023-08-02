using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI_dm : MonoBehaviour
{
    public static BossUI_dm instance;

    public enum HP
    {
        body,
        leftArm,
        rightArm,
        octopus
    };

    [SerializeField]
    Slider bodySlider;
    [SerializeField]
    Slider leftArmSlider;
    [SerializeField]
    Slider rightArmSlider;
    [SerializeField]
    Slider octopusSlider;

    [SerializeField]
    GameObject bossUI;
    float uiMoveSpeed = 120f;

    [SerializeField]
    int setMaxCount = 3;
    int setCount = 0;

    float bodyMaxHp = 0;
    float leftMaxHp = 0;
    float rightMaxHp = 0;
    float octoMaxHp = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //boss = GameObject.Find("")


        //slider.maxValue = GameObject.Find("Boss_dm").GetComponent<Boss_dm>().hp;
        //slider.maxValue = GetComponentInParent<Monster_dm>().hp;
        //slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //UI 슬라이더에 hp 반영
    public void Damage(HP hpSelect, int hp)
    {
        switch (hpSelect)
        {
            case HP.body:
                bodySlider.value = hp / bodyMaxHp;
                break;
            case HP.leftArm:
                leftArmSlider.value = hp / leftMaxHp;
                break;
            case HP.rightArm:
                rightArmSlider.value = hp / rightMaxHp;
                break;
            case HP.octopus:
                octopusSlider.value = hp / octoMaxHp;
                break;
        }
    }

    public void StartSet(HP hpSelect, int maxHp)
    {
        switch (hpSelect)
        {
            case HP.body:
                bodyMaxHp = maxHp;
                bodySlider.value = 0;
                break;
            case HP.leftArm:
                leftMaxHp = maxHp;
                leftArmSlider.value = 0;
                break;
            case HP.rightArm:
                rightMaxHp = maxHp;
                rightArmSlider.value = 0;
                break;
            case HP.octopus:
                octoMaxHp = maxHp;
                octopusSlider.value = 0;
                break;
        }

        setCount++;

        if (setCount >= setMaxCount)
        {
            StartCoroutine(StartAct());
        }
    }

    //public void StartActive()
    //{
    //    StartCoroutine(StartAct());
    //}

    IEnumerator StartAct()
    {
        Rigidbody2D rb = bossUI.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * uiMoveSpeed, ForceMode2D.Impulse);

        float curTime = 0;
        bool startSlider = false;

        while (true)
        {
            curTime += Time.deltaTime;

            if (curTime >= 0.1f)
            {
                curTime = 0;
                rb.velocity = rb.velocity * 0.8f;

                if (rb.velocity.y >= -7f && !startSlider)
                {
                    startSlider = true;
                    StartCoroutine(StartSliderSet(leftArmSlider));
                    StartCoroutine(StartSliderSet(rightArmSlider));
                }

                if (rb.velocity.y >= -0.1f)
                {
                    rb.velocity = Vector2.zero;
                    break;
                }
            }

            yield return null;
        }

    }

    IEnumerator StartSliderSet(Slider slider)
    {
        float setTime = 1.0f;
        float curTime = 0;

        while (true)
        {
            curTime += Time.deltaTime;
            slider.value = curTime / setTime;

            if (curTime >= setTime)
                break;

            yield return null;
        }


    }
}
