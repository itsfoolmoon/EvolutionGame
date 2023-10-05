/**
This code is applied on the 2D collider attached to
the frog itself. It is used when the frog collides
with either a food or another frog. Anything involving
collisions will happen here.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArea : MonoBehaviour
{
    public GameObject darwin; //Used to create clone
    public GameObject cookieInstantiator;

    float heredity(float darwinHeredity, float otherHeredity, float darwinTrait, float otherTrait)
    {
        float random = Random.Range(0.0f, 1.0f);

        if(random <= darwinHeredity)
            return ((darwinHeredity + otherHeredity) * darwinTrait + otherHeredity * otherTrait) / (darwinHeredity + otherHeredity * 2);
        if(random > darwinHeredity && random <= darwinHeredity + otherHeredity)
            return ((darwinHeredity + otherHeredity) * otherTrait + darwinHeredity * darwinTrait) / (darwinHeredity * 2 + otherHeredity);

        return -1;
    }

    void OnTriggerEnter2D(Collider2D other) // Trigger(used for cookies)
    {
        DarwinTraits darwinTraits = gameObject.GetComponent<DarwinTraits>();
        CookieInstantiator cookieScript = cookieInstantiator.GetComponent<CookieInstantiator>();

        darwinTraits.boredom = 0;

        if (other.gameObject.CompareTag("Cookie"))
        {
            //Debug.Log("Ate a Cookie!");
            Destroy(other.gameObject);
            darwinTraits.setEnergy(darwinTraits.getEnergy() + 5);
            cookieScript.spawnCookie();
        }
    }

    void OnCollisionEnter2D(Collision2D other) // Collision(used for other frogs)
    {
        DarwinTraits otherTraits = other.gameObject.GetComponent<DarwinTraits>();
        DarwinTraits darwinTraits = gameObject.GetComponent<DarwinTraits>();

        darwinTraits.boredom = 0;

        if (other.gameObject.CompareTag("Darwin"))
        {
            if (otherTraits.canBreed() && darwinTraits.canBreed())
            {
                if (otherTraits.getMass() > darwinTraits.getMass())
                {
                    DarwinTraits traits = darwin.GetComponent<DarwinTraits>();

                    float traitStat = heredity(darwinTraits.getMassHeredity(), otherTraits.getMassHeredity(), darwinTraits.getMass(), otherTraits.getMassHeredity());

                    if(traitStat == -1)
                        traits.setMass(Random.Range(0.05f, 0.5f));
                    else
                        traits.setMass(traitStat);

                    traits.setMassHeredity(Random.Range(0.0f, 0.5f));


                    traitStat = heredity(darwinTraits.getChargeCoolDownHeredity(), otherTraits.getChargeCoolDownHeredity(), darwinTraits.getChargeCoolDown(), otherTraits.getChargeCoolDown());

                    if(traitStat == -1)
                        traits.setChargeCoolDown(Random.Range(1.0f, 5.0f));
                    else
                        traits.setChargeCoolDown(traitStat);

                    traits.setChargeCoolDownHeredity(Random.Range(0.0f, 0.5f));


                    traitStat = heredity(darwinTraits.getChargeStrengthHeredity(), otherTraits.getChargeStrengthHeredity(), darwinTraits.getChargeStrength(), otherTraits.getChargeStrength());

                    if(traitStat == -1)
                        traits.setChargeStrength(Random.Range(100.0f, 200.0f));
                    else
                        traits.setChargeStrength(traitStat);

                    traits.setChargeStrengthHeredity(Random.Range(0.0f, 0.5f));


                    traitStat = heredity(darwinTraits.getDeltaDeviationAngleHeredity(), otherTraits.getDeltaDeviationAngleHeredity(), darwinTraits.getDeltaDeviationAngle(), otherTraits.getDeltaDeviationAngle());

                    if(traitStat == -1)
                        traits.setDeltaDeviationAngle(Random.Range(0.0f, 15.0f));
                    else
                        traits.setDeltaDeviationAngle(traitStat);

                    traits.setDeltaDeviationAngleHeredity(Random.Range(0.0f, 0.5f));

                    traits.setBreedEnergy(Random.Range(40, 100));

                    Instantiate(darwin, other.transform.position, other.transform.rotation);
                    otherTraits.setEnergy(otherTraits.getEnergy() / 2);
                    darwinTraits.setEnergy(darwinTraits.getEnergy() / 2);
                }
            }
        }
    }
}
