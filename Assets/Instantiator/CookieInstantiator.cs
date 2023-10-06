using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieInstantiator : MonoBehaviour
{
    public GameObject cookie;

    void Start()
    {
        for (int i = 0; i < 75; i++)
            spawnCookie();
    }

    public void spawnCookie()
    {
        Instantiate(cookie, new Vector3(Random.Range(-18.0f,18.0f), Random.Range(-18.0f, 18.0f), 0), Quaternion.identity);
    }
}
