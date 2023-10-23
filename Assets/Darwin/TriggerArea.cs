/**
This code is applied to the trigger collider which is attached as a child to a Darwin.
It is used when an object(either a food or another Darwin)
enters the area near the current Darwin. It is also responsible for keeping
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

    /*
     * This function is responsible for adding objects, either a cookie or another Darwin to the
     * Lists cookiesInArea or darwinsInArea.
     * 
     * @param other: The Collider2D of the object that just entered the trigger area of the Darwin.
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cookie")) // The gameObject that just entered was a cookie
        {
            cookiesInArea.Add(other.gameObject);
            //umCookies++;
            //Debug.Log("TriggerArea - Cookie " + numCookies);
        }
        else if (other.gameObject.CompareTag("Darwin")) // The Collider2D that just entered was a Darwin
        {
            darwinsInArea.Add(other.gameObject);
            //numDarwins++;
            //Debug.Log("TriggerArea - Darwin " + numDarwins);
        }
    }

    /*
     * This function is responsible for removing objects, either a cookie or another Darwin,
     * from the Lists if they have exited the triggerArea.
     * 
     * @param other: The Collider2D of the object that just exited the trigger area of the Darwin.
     */
    void OnTriggerExit2D(Collider2D other)
    {
        if(cookiesInArea.IndexOf(other.gameObject) >= 0) // The gameObject that just exited was a cookie
        {
            cookiesInArea.Remove(other.gameObject);
            //numCookies--;
            //Debug.Log(numCookies+"Cookie(s)");
        }
        else if (darwinsInArea.IndexOf(other.gameObject) >= 0) // The gameObject that just exited was a Darwin
        {
            darwinsInArea.Remove(other.gameObject);
            //numDarwins--;
            //Debug.Log(numDarwins+"Darwin(s)");
        }
    }
}
