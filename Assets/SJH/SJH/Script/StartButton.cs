using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private void Awake()
    {
        
    }
    public void GameStart()
    {
        SceneManager.LoadScene("DongMinScene 1");
    }
}
