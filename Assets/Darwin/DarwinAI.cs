/**
This code is applied to the gameObject Darwin.
It is responsible for handling the behavior of the Darwin.
AKA the AI of the Darwin
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarwinAI : MonoBehaviour
{
    // Rigidbody2D of the current Darwin
    private Rigidbody2D rigidBody;

    // DarwinTraits(a certain script) of the current Darwin
    private DarwinTraits traits;

    private LineRenderer lineRenderer;

    private SpriteRenderer spriteRenderer;

    // Measures the current time. It is used to determine if enough time has passed for the Darwin to "lunge" again
    private float timeStamp = 0.0f;

    void Start()
    {
        // Rigidbody2D of the current Darwin
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

        // DarwinTraits(a certain script) of the current Darwin
        traits = gameObject.GetComponent<DarwinTraits>();

        rigidBody.mass = traits.Mass;

        lineRenderer = GetComponent<LineRenderer>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!traits.isLiving())
            Destroy(gameObject);

        // The angle in which the Darwin will "lunge" towards
        float angle = Random.Range(0.0f, 360.0f);

        GameObject closestObject = null;

        if (!traits.isBored())
            closestObject = getClosestObject();

        // This if-statement will be true if enough time has passed for the Darwin to be able to "lunge" again
        if (timeStamp <= Time.time)
        {
            // Set the angle to the direction of the closest gameobject if it exists within the trigger area
            if (closestObject != null)
                angle = getAngle(closestObject);

            // "Lunge" towards the desired angle with some inaccuracy(DeltaDeviationAngle) at a certain strength
            charge(angle + Random.Range(-traits.DeltaDeviationAngle, traits.DeltaDeviationAngle), traits.ChargeStrength);

            // Put the "lunge" on cooldown
            timeStamp = Time.time + traits.ChargeCoolDown;

            // Decrease energy since Darwin just "lunged!"
            traits.Energy -= 3;

            // Darwin is done being bored!
            if (traits.Boredom >= traits.BoredThreshold * 2)
                traits.Boredom = 0;

            if (traits.isBored())
                spriteRenderer.color = Color.blue;
            else
                spriteRenderer.color = Color.yellow;

            // Increase boredom. This is to ensure that Darwin does not get stuck trying to get an inaccessible cookie
            // Will be reset when Darwin is done being bored or if it collides into anything other than a wall.
            traits.Boredom++;
        }
    }

    /*
     * This function is responsible for "lunging" towards the desired angle at a certain strength
     * 
     * @param angle: The angle to "lunge" towards
     * @param strength: The strength at which to "lunge" at
     */
    void charge(float angle, float strength)
    {
        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        rigidBody.AddForce(dir * strength);
    }

    /*
     * This function is responsible for finding the angle to the closest gameObject
     * 
     * @param other: The closest gameObject
     */
    float getAngle(GameObject other)
    {
        Vector3 dir = other.transform.position - transform.position;
        dir = other.transform.InverseTransformDirection(dir);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

    /*
     * This function is responsible for finding the closest gameObject, either a cookie or another Darwin from the
     * Lists cookiesInArea or darwinsInArea.
     * 
     * @param darwins: The list of Darwins within the trigger area of the Darwin.
     * @param cookies: The list of cookies within the trigger area of the Darwin.
     */
    GameObject getClosestObject()
    {
        // This is used to store the closest object from the current Darwin. Can be either a cookie or another Darwin
        GameObject closestObject = null;

        GameObject[] darwins = GameObject.FindGameObjectsWithTag("Darwin");
        GameObject[] cookies = GameObject.FindGameObjectsWithTag("Cookie");


        // The distance from the closest object. Set as 10.0f which is the radius of the triggerArea.
        float dist = 10.0f;

        if (traits.canBreed()) // if Darwin has enough energy(food), look for mate to breed with
        {
            foreach (GameObject darwin in darwins)
            {
                // There can be cases where the other Darwin that was closest to the current Darwin dies :(
                // We also have a darwin == gameObject since we do not want to count the current Darwin as a potential mate
                if (darwin == null || darwin == gameObject)
                    continue;

                DarwinTraits otherDarwinTraits = darwin.GetComponent<DarwinTraits>();

                // See if the closest Darwin has enough energy to breed. 
                if (Vector3.Distance(darwin.transform.position, transform.position) < dist && Vector3.Distance(darwin.transform.position, transform.position) < 0 && otherDarwinTraits.canBreed())
                {
                    closestObject = darwin;
                    dist = Vector3.Distance(closestObject.transform.position, transform.position);
                }
            }
        }
        else
        {
            foreach (GameObject cookie in cookies)
            {
                // There can be cases where another Darwin eats the cookie that was closest to the current Darwin
                if (cookie == null)
                    continue;
                // If current cookie that is in the list is closer than the previous closest, set the new closest cookie as this.
                if (Vector3.Distance(cookie.transform.position, transform.position) < dist)
                {
                    closestObject = cookie;
                    dist = Vector3.Distance(closestObject.transform.position, transform.position);
                }
            }
        }

        // Draw a visible line between the current Darwin and the closest game object
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, closestObject.transform.position);


        return closestObject;
    }
}
