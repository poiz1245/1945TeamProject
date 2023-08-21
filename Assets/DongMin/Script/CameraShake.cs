using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    AudioSource audioSource;

    bool switchLR = true;
    public float speed = 10;
    public float maxPosX = 0.03f;
    public bool shakeOnOff = false;

    bool fullScreenOnOff = false;
    bool onOff = true;

    private void Awake()
    {
        //Screen.SetResolution(600, 1920, true);
        
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //스크린 전체화면 창모드 변경
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fullScreenOnOff = !fullScreenOnOff;
            onOff = true;
        }
        if (onOff)
        {
            Screen.SetResolution(450, 800, fullScreenOnOff);
            onOff = false;
        }

        if (shakeOnOff)
        {
            if (switchLR)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -maxPosX, maxPosX),
                transform.position.y, transform.position.z);

            //transform.position.x = 3;
            if (transform.position.x >= maxPosX || transform.position.x <= -maxPosX)
                switchLR = !switchLR;
        }
    }


    public void ShakeSwitch()
    {
        shakeOnOff = true;
        transform.position = new Vector3(0, 0, transform.position.z);
    }

    public void ShakeSwitchOff()
    {
        shakeOnOff = false;
        transform.position = new Vector3(0, 0, transform.position.z);
    }

    public void AudioPlay()
    {
        audioSource.volume = 0;
    }
}
