using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BossUI_dm;

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
    float uiMoveSpeed = 5f;

    [SerializeField]
    int setMaxCount = 4;
    int setCount = 0;

    float bodyMaxHp = 0;
    float leftMaxHp = 0;
    float rightMaxHp = 0;
    float octoMaxHp = 0;

    [SerializeField]
    GameObject octopusWarningText;

    [SerializeField]
    GameObject nextStageBtn;
    [SerializeField]
    GameObject clearText;

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

        octopusWarningText.SetActive(false);
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

    public void Damage(HP hpSelect, float hp)
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

    public void SetActiveFalseSlider(HP hpSelect)
    {
        switch (hpSelect)
        {
            case HP.body:
                bodySlider.gameObject.SetActive(false);
                break;
            case HP.leftArm:
                leftArmSlider.gameObject.SetActive(false);
                break;
            case HP.rightArm:
                rightArmSlider.gameObject.SetActive(false);
                break;
            case HP.octopus:
                octopusSlider.gameObject.SetActive(false);
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

    public void StartSet_ver2()
    {
        bodyMaxHp = 4000;
        bodySlider.value = 0;

        octoMaxHp = 3000;
        octopusSlider.value = 0;

        StartCoroutine(StartAct_ver2());
    }

    public void StartSet_ver3(HP hpSelect, int maxHp)
    {
        bodyMaxHp = maxHp;
        bodySlider.value = 0;

        StartCoroutine(StartAct_ver2());
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

                //if (rb.velocity.y >= -7f && !startSlider)
                if (bossUI.GetComponent<RectTransform>().position.y <= 4.87f && !startSlider)
                {
                    Debug.Log("UI 실행");
                    rb.velocity = Vector2.zero;
                    startSlider = true;
                    StartCoroutine(StartSliderSet(leftArmSlider));
                    StartCoroutine(StartSliderSet(rightArmSlider));
                    
                    break;
                }

                //if (rb.velocity.y >= -0.1f)
                //{
                //    rb.velocity = Vector2.zero;
                //    break;
                //}
            }

            yield return null;
        }

    }

    IEnumerator StartAct_ver2()
    {
        Rigidbody2D rb = bossUI.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * 4, ForceMode2D.Impulse);

        float curTime = 0;
        bool startSlider = false;

        while (true)
        {
            curTime += Time.deltaTime;

            if (curTime >= 0.1f)
            {
                curTime = 0;
                rb.velocity = rb.velocity * 0.8f;

                //if (rb.velocity.y >= -3f && !startSlider)
                if (bossUI.GetComponent<RectTransform>().position.y <= 4.87f && !startSlider)
                {
                    startSlider = true;
                    StartCoroutine(StartSliderSet(bodySlider));
                    rb.velocity = Vector2.zero;
                    break;
                }

                //if (rb.velocity.y >= -0.1f)
                //{
                //    rb.velocity = Vector2.zero;
                //    break;
                //}
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

    public void CorStartSliderSet(HP hpSelect)
    {
        switch (hpSelect)
        {
            case HP.body:
                StartCoroutine(StartSliderSet(bodySlider));
                break;
            case HP.leftArm:
                StartCoroutine(StartSliderSet(leftArmSlider));
                break;
            case HP.rightArm:
                StartCoroutine(StartSliderSet(rightArmSlider));
                break;
            case HP.octopus:
                StartCoroutine(StartSliderSet(octopusSlider));
                break;
        }
    }

    public void OctopusWarning()
    {
        octopusWarningText.SetActive(true);

        StartCoroutine(OctopusWarningBlink());
    }

    IEnumerator OctopusWarningBlink()
    {
        float delaySec = 0.13f;

        for(int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(delaySec);
            octopusWarningText.GetComponent<Text>().color = Color.white;
            yield return new WaitForSeconds(delaySec);
            octopusWarningText.GetComponent<Text>().color = Color.red;
        }
        yield return new WaitForSeconds(delaySec);

        octopusWarningText.SetActive(false);
    }

    public void StageClear()
    {
        //nextStageBtn.SetActive(true);
        ScoreManager.instance.UpdateScore();
        //clearText.SetActive(true);
    }

    public void NextStage()
    {
        //SceneManager.LoadScene("")
        Debug.Log("다음스테이지로");
    }
}
