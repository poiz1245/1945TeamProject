using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPColor_dm : MonoBehaviour
{
    [SerializeField]
    float lerpTime = 0.1f;
    TextMeshProUGUI textBossWarning;

    //오브젝트가 비활성화일때는 start함수 X
    private void Awake()
    {
        //캐싱
        textBossWarning = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //활성화될때
    private void OnEnable()
    {
        //코루틴 실행
        StartCoroutine("ColorLerpLoop");
    }

    IEnumerator ColorLerpLoop()
    {
        while(true)
        {
            //색상을 하얀색에서 빨간색으로
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
            //색상을 빨간색에서 하얀색으로
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
        }
    }

    IEnumerator ColorLerp(Color startColor,Color endColor)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            //lerpTime 시간동안 while() 반복문 실행
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            //Text - TextMeshPro의 폰트 색상을 startColor에서 endColor로 변경
            textBossWarning.color = Color.Lerp(startColor, endColor, percent);

            yield return null;
        }
    }
}
