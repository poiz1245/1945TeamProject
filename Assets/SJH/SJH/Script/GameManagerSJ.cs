using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSJ : MonoBehaviour
{
    public static GameManagerSJ Instance;
    public PoolManagerSJ pool;
    public PlayerSJ player;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
