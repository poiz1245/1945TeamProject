using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManagerSJ : MonoBehaviour
{
    public static WayPointManagerSJ instance;
    public Transform[] LeftWayPoint;
    public Transform[] RightWayPoint;
    public Transform[] LeftStopWayPoint;
    public Transform[] RightStopWayPoint;
    public Transform[] RightCornerWayPoint;
    public Transform[] LeftCornerWayPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
