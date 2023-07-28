using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomFire : MonoBehaviour
{
   
    public GameObject Boom2;
    

   
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoolTimeTransBoom());
       
       
    }

 

    IEnumerator CoolTimeTransBoom()
    {
       
        yield return new WaitForSeconds(1f);
      
        var copyboom2 = Instantiate(Boom2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        //Destroy(copyboom2, 1f);
        Destroy(transform.gameObject);
       
    }
}
