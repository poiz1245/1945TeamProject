using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg_dm : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        rb.rotation += rotation;
    }
}
