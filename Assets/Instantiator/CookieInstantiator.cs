using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieInstantiator : MonoBehaviour
{
    public GameObject cookie;

    void Start()
    {
        for(int i = 0; i < 75 ; i++)
            Instantiate(cookie, new Vector3(Random.Range(-18.0f,18.0f), Random.Range(-18.0f, 18.0f), 0), Quaternion.identity);
    }

    public void spawnCookie()
    {
        //loat timeStamp = Time.time + 5.0f;

        //hile(true)
        //
        //   if(timeStamp <= Time.time)
        //   {
        //       Instantiate(cookie, new Vector3(Random.Range(-18.0f,18.0f), Random.Range(-18.0f, 18.0f), 0), Quaternion.identity);
        //       break;
        //   }
        //

        Instantiate(cookie, new Vector3(Random.Range(-18.0f,18.0f), Random.Range(-18.0f, 18.0f), 0), Quaternion.identity);
    }
}
