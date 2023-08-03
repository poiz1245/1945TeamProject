using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    bool switchLR = true;
    public float speed = 10;
    public float maxPosX = 0.03f;
    bool shakeOnOff = false;

    private void Awake()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
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
        shakeOnOff = !shakeOnOff;
        transform.position = new Vector3(0, 0, transform.position.z);
    }

    public void ShakeSwitchOff()
    {
        shakeOnOff = false;
        transform.position = new Vector3(0, 0, transform.position.z);
    }
}
