using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarwinAI : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private DarwinTraits traits;
    private TriggerArea triggerArea;
    private float timeStamp = 0.0f;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        traits = gameObject.GetComponent<DarwinTraits>();
        triggerArea = gameObject.GetComponentInChildren<TriggerArea>();

        rigidBody.mass = traits.getMass();
    }

    void Update()
    {
        if(!traits.isLiving())
            Destroy(gameObject);

        float angle;
        
        if(timeStamp <= Time.time)
        {
            GameObject thing = getClosestObject(triggerArea.darwinsInArea, triggerArea.cookiesInArea);

            if (thing == null)
                angle = Random.Range(0.0f, 360.0f);
            else
                angle = getAngle(thing);

            charge(angle + Random.Range(-traits.getDeltaDeviationAngle(), traits.getDeltaDeviationAngle()), traits.getChargeStrength());
            timeStamp = Time.time + traits.getChargeCoolDown();
        }
    }

    void charge(float angle, float strength)
    {
        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        rigidBody.AddForce(dir * strength);
        traits.setEnergy(traits.getEnergy() - 3);
    }

    float getAngle(GameObject other)
    {
        Vector3 dir = other.transform.position - transform.position;
        dir = other.transform.InverseTransformDirection(dir);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

    GameObject getClosestObject(List<GameObject> darwins, List<GameObject> cookies)
    {
        GameObject closestObject = null;
        float dist = 10.0f;

        if(darwins.Count == 1) // if frog is still hungry, search for more food
        {
            dist = float.MaxValue;
            foreach (GameObject cookie in cookies)
            {
                if(cookie == null)
                    continue;
                if (Vector3.Distance(cookie.transform.position, transform.position) < dist)
                {
                    closestObject = cookie;
                    dist = Vector3.Distance(closestObject.transform.position, transform.position);
                }
            }
        }
        else if (traits.canBreed()) // if frog has enough energy(food), look for mate
        {
            foreach (GameObject darwin in darwins)
            {
                if(darwin == null || darwin == gameObject)
                    continue;

                DarwinTraits otherDarwinTraits = darwin.GetComponent<DarwinTraits>();
                if (Vector3.Distance(darwin.transform.position, transform.position) < dist && otherDarwinTraits.canBreed())
                {
                    closestObject = darwin;
                    dist = Vector3.Distance(closestObject.transform.position, transform.position);
                }
            }
        }
        
        return closestObject;
    }
}
