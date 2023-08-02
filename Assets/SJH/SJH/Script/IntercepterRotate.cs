using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercepterRotate : MonoBehaviour
{
    //인터셉터 로테이션 

    public float rotationSpeed = 5f;


    GameObject target;
    private void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(target)
        {
            Vector2 direction = new Vector2(transform.position.x - target.transform.position.x,
                                       transform.position.y - target.transform.position.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotationSpeed * Time.deltaTime);
            transform.rotation = rotation;
        }
       
    }
}
