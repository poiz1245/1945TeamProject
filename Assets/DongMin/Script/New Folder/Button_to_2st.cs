using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_to_2st : MonoBehaviour
{
    public string sceneName = "SJH";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStage()
    {
        Debug.Log("?");
        SceneManager.LoadScene(sceneName);
    }
}
