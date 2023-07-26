using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    //아이템 가속 속도 
    public float ItemVelocity = 20f;
    Rigidbody2D rig = null;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(ItemVelocity, ItemVelocity, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
