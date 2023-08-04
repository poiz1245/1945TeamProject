using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

public class destoyPosSJ : MonoBehaviour
{
    public GameObject smallstar;

    bool check = true;
    // Start is called before the first frame update
    void Start()
    {
        check = true;
        StartCoroutine(CreatBullet());
        Invoke("Stop", 1.5f);
    }

    IEnumerator CreatBullet()
    {
        int count = 9;
        float intervalAngle = 180 / count;
        float weightAngle = 0;
        int i = 0;
        while (check)
        {
            for (i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(smallstar, transform.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                clone.GetComponent<SmallStarSj>().Move(new Vector2(x, y));
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Stop()
    {
        check = false;
        StopCoroutine(CreatBullet());
    }
}
