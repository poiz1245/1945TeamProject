using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RotationSJ : MonoBehaviour
{

    public float rotationSpeed = 5f;

    
    GameObject target;
    private void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector2 direction = new Vector2(transform.position.x - target.transform.position.x,
                                       transform.position.y - target.transform.position.y);
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }
}
