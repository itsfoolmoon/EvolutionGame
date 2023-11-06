using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieInstantiator : MonoBehaviour
{
    public GameObject cookie;

    // How many cookies are being eaten per second.
    // Formula: cookiesEatenRate = cookiesEaten / (Time.time * cookieSpawnRate);
    public float cookiesEatenRate = 0.0f;

    // The frequency at which cookies spawn. Higher = faster cookie spawns
    public float cookieSpawnRate = 10.0f;

    // How long it takes for the next cookie to spawn
    // Formula: nextCookieSpawnTime = Time.time + cookiesEatenRate;
    public float nextCookieSpawnTime = 0.0f;

    // The maximum amount of cookies to spawn
    public int maxCookies = 75;

    // The number of cookies eaten throughout the simulation.
    public long CookiesEaten { get; set; }

    void Start()
    {
        CookiesEaten = 0;

        // Spawn maxCookies amount of cookies
        for (int i = 0; i < maxCookies; i++)
        {
            StartCoroutine(SpawnCookie());
            CookiesEaten = 0;
            cookiesEatenRate = 0.0f;
            nextCookieSpawnTime = 0.0f;
        }
    }

    public IEnumerator SpawnCookie()
    {
        Debug.Log("SpawnCookie started");
        GameObject[] darwins = GameObject.FindGameObjectsWithTag("Darwin");
        yield return new WaitForSeconds(darwins.Length);
        Debug.Log("Waited for " + nextCookieSpawnTime + " seconds");

        Instantiate(cookie, FindEmptySpace(), Quaternion.identity);
        Debug.Log("Cookie spawned");

        //cookiesEatenRate = CookiesEaten / (Time.time * cookieSpawnRate);
        //nextCookieSpawnTime = Time.time + cookiesEatenRate;
        //Debug.Log("Updated cookiesEatenRate and nextCookieSpawnTime");
    }

    private Vector3 FindEmptySpace()
    {
        Collider2D collided;

        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-18.0f, 18.0f), Random.Range(-18.0f, 18.0f), 0);

            // Check for collisions at the spawn position
            collided = Physics2D.OverlapCircle(spawnPosition, 0.2f);

            if (collided == null)
            {
                return spawnPosition;
            }
        }
    }
}
