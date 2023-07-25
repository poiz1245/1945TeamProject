using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead_dm : MonoBehaviour
{
    [SerializeField]
    GameObject bossBullet; //보스미사일

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RightDownLaunch()
    {
        GameObject go = Instantiate(bossBullet, transform.position, Quaternion.identity);
        go.GetComponent<MonsterBullet_dm>().Move(new Vector2(1, -1));
    }

    public void DownLaunch()
    {
        GameObject go = Instantiate(bossBullet, transform.position, Quaternion.identity);
        go.GetComponent<MonsterBullet_dm>().Move(new Vector2(0, -1));
    }

    public void LeftDownLaunch()
    {
        GameObject go = Instantiate(bossBullet, transform.position, Quaternion.identity);
        go.GetComponent<MonsterBullet_dm>().Move(new Vector2(-1, -1));
    }
}
