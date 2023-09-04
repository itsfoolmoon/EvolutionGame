/**
This code is applied to the trigger collider attached to the frog itself.
It is used when an object(either a food or another frog)
enters the area near the frog. It is also responsible for keeping
track of all of the objects that are within the range.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int numCookies = 0;
    public int numDarwins = 0;
    public List<GameObject> cookiesInArea = new List<GameObject>();
    public List<GameObject> darwinsInArea = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D other) // Add objects to list when they get in range
    {
        if (other.gameObject.CompareTag("Cookie")) // Cookie in range
        {
            cookiesInArea.Add(other.gameObject);
            //umCookies++;
            //Debug.Log("TriggerArea - Cookie " + numCookies);
        }
        else if (other.gameObject.CompareTag("Darwin")) // Frog in range
        {
            darwinsInArea.Add(other.gameObject);
            //numDarwins++;
            //Debug.Log("TriggerArea - Darwin " + numDarwins);
        }
    }

    void OnTriggerExit2D(Collider2D other) // Remove objects from list when they get out of range
    {
        if(cookiesInArea.IndexOf(other.gameObject) >= 0) // Cookie left range
        {
            cookiesInArea.Remove(other.gameObject);
            //numCookies--;
            //Debug.Log(numCookies+"Cookie(s)");
        }
        else if (darwinsInArea.IndexOf(other.gameObject) >= 0) // Frog left range
        {
            darwinsInArea.Remove(other.gameObject);
            //numDarwins--;
            //Debug.Log(numDarwins+"Darwin(s)");
        }
    }
}
