using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieInstantiator : MonoBehaviour
{
    public GameObject cookie;

    // The maximum amount of cookies to spawn
    public int maxCookies = 75;

    void Start()
    {
        // Spawn maxCookies amount of cookies
        for (int i = 0; i < maxCookies; i++)
        {
            StartCoroutine(SpawnCookie());
        }
    }

    public IEnumerator SpawnCookie()
    {
        Debug.Log("SpawnCookie started");
        // Get the number of Darwins present on the map
        GameObject[] darwins = GameObject.FindGameObjectsWithTag("Darwin");
        // Find Empty Space one the map
        Vector3 spawnPosition = FindEmptySpace();

        // Wait for (number of Darwins) seconds
        yield return new WaitForSeconds(darwins.Length);

        Debug.Log("Waited for " + darwins.Length + " seconds");
        // Spawn a new cookie at an empty space
        Instantiate(cookie, spawnPosition, Quaternion.identity);
        Debug.Log("Cookie spawned");
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
