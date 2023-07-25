using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSJ : MonoBehaviour
{
    public float Speed = 1f;
    public GameObject bullet;
    public Transform gunPos;
    public int AttackPower = 10;
    public int Hp = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        transform.Translate(moveHorizontal, moveVertical , 0);

        if(Input.GetKeyDown(KeyCode.Space))
            Instantiate(bullet, gunPos.position, Quaternion.identity);
    }
}
