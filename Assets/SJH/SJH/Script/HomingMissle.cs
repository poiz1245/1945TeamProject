using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingMissle : MonoBehaviour
{
    public float Speed = 10;
    public float ScanRange;
    public float rotationSpeed = 5f;

    public LayerMask targetLayer;
    public RaycastHit2D[] targets;

    public Transform nearestTarget;
    Vector2 dir;
    Vector2 dirno;

    bool isScanning = true;

    void Start()
    {
        //nearestTarget = GetNearest();
        //target = GameObject.FindWithTag("Enemy");
    }

    void Update()
    {
        targets = Physics2D.CircleCastAll(transform.position, ScanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();

        dir = nearestTarget.transform.position - transform.position;

        dirno = dir.normalized;
        
        if (nearestTarget != null)
        {
            transform.Translate(dir.normalized*Speed*Time.deltaTime);
            Vector2 direction = new Vector2(transform.position.x - nearestTarget.transform.position.x,
                                     transform.position.y - nearestTarget.transform.position.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotationSpeed * Time.deltaTime);
            transform.rotation = rotation;
        }
        else
        {
            transform.Translate(dirno * Speed * Time.deltaTime);
        }

       
    }

    public Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 mypos = transform.position;
            Vector3 targetpos = target.transform.position;
            float curdiff = Vector3.Distance(mypos, targetpos);

            if (curdiff < diff)
            {
                diff = curdiff;
                result = target.transform;
            }
        }
        return result;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }

}
