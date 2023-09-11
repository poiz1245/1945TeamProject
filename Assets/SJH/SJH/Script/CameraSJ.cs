using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSJ : MonoBehaviour
{
    public static CameraSJ instance;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AudioPlay()
    {
        audioSource.volume = 0;
    }
}
