using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class HelperShot : MonoBehaviour
{
    public Transform pos = null; //미사일 발사
    public GameObject helperBullet;
    bool isshot = false;

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (!isshot)
        {
            StartCoroutine(Shot());
            Instantiate(helperBullet, pos.position, Quaternion.identity);
        }

    }

    IEnumerator Shot()
    {
        isshot = true;
        yield return new WaitForSeconds(0.2f);
        isshot = false;

    }
}
