using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangMusic : MonoBehaviour
{
    
    AudioSource source;

    public AudioClip boss1;
    public AudioClip boss2;
   
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
