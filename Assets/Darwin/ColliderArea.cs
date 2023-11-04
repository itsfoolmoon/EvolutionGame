/**
This code is applied on the 2D collider attached to
a Darwin. It is used when the Darwin collides
with either a food or another Darwin. Anything involving
collisions will happen here.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArea : MonoBehaviour
{
    public GameObject darwin; // The child object to create when it breeds
    public GameObject cookieInstantiator; // Gameobject responsible for spawning cookies upon consumption by Darwin

    /*
     * Used to calculate the value of the new traits the child will inherit from the parent.
     * This determines which traits the child will inherit most from. Either most from parent 1 or parent 2.
     *  
     * Rough formula:
     * Chance to inherit most from parent 1 = darwinHeredity
     * Chance to inherit most from parent 2 = otherHeredity
     * Chance to mutate and have own trait  = 1 - (darwinHeredity + otherHeredity)
     *  
     * @param darwinHeredity: Chance to inherit most from parent 1
     * @param otherHeredity:  Chance to inherit most from parent 2
     * @param darwinTrait: Trait to inherit from parent 1 (i.e. mass, boredomThreshold, deltaDeviationAngle, etc.)
     * @param otherTrait:  Trait to inherit from parent 2 (i.e. mass, boredomThreshold, deltaDeviationAngle, etc.)
     */
    float heredity(float darwinHeredity, float otherHeredity, float darwinTrait, float otherTrait)
    {
        float random = Random.Range(0.0f, 1.0f); // Used to choose if inherited trait will mostly be from parent 1 or 2

        // Inherit most of parent 1's traits
        if(random <= darwinHeredity)
            return ((darwinHeredity + otherHeredity) * darwinTrait + otherHeredity * otherTrait) / (darwinHeredity + otherHeredity * 2);
        else if(random <= darwinHeredity + otherHeredity) // Inherit most of parent 2's traits
            return ((darwinHeredity + otherHeredity) * otherTrait + darwinHeredity * darwinTrait) / (darwinHeredity * 2 + otherHeredity);

        // Mutation. Create own trait.
        return -1;
    }

    /*
     * Used to check collision with cookie.
     * Upon collision, reset the Darwin's boredom to 0, increase the Darwin's energy, delete cookie, and spawn a new one somewhere else.
     * 
     * @param other: The Collider2D of the cookie the Darwin collided with.
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        DarwinTraits darwinTraits = gameObject.GetComponent<DarwinTraits>();
        CookieInstantiator cookieScript = cookieInstantiator.GetComponent<CookieInstantiator>();

        if (other.gameObject.CompareTag("Cookie"))
        {
            // Hooray! I got a cookie, task successful, reset boredom.
            darwinTraits.Boredom = 0;
            //Debug.Log("Ate a Cookie!");
            //Destroy(other.gameObject);
            other.gameObject.transform.position = new Vector3(Random.Range(-18.0f, 18.0f), Random.Range(-18.0f, 18.0f), 0);
            // Increase the Darwin's energy
            darwinTraits.Energy += 5;
            //cookieScript.spawnCookie();
        }
    }

    /*
     * Used to check collision with another Darwin.
     * Upon collision, reset the Darwin's boredom to 0.
     * If current Darwin and other Darwin are able to breed, generate traits for child to inherit and spawn it.
     * 
     * @param other: The Collider2D of the other Darwin the current Darwin collided with.
     */
    void OnCollisionEnter2D(Collision2D other)
    {
        DarwinTraits otherTraits = other.gameObject.GetComponent<DarwinTraits>(); // parent 1's list of traits
        DarwinTraits darwinTraits = gameObject.GetComponent<DarwinTraits>(); // parent 2's list of traits

        if (other.gameObject.CompareTag("Darwin"))
        {
            // Hooray! I bumped into another Darwin, task successful, reset boredom.
            darwinTraits.Boredom = 0;
            // Are criterias met for both of us to breed?
            if (otherTraits.canBreed() && darwinTraits.canBreed())
            {
                GameObject newObject; // The gameobject of the new child that will be birthed.

                // This if statement is a bit arbitrary. To prevent birthing of two children when there should be one,
                // the parent with a higher mass births the child.
                if (otherTraits.Mass > darwinTraits.Mass)
                    newObject = Instantiate(darwin, other.transform.position, Quaternion.identity);
                else
                    newObject = Instantiate(darwin, transform.position, Quaternion.identity);

                DarwinTraits traits = newObject.GetComponent<DarwinTraits>(); // An empty trait for the child to inherit from its parents



                // The mass value the child will inherit from its parents
                float traitStat = heredity(darwinTraits.MassHeredity, otherTraits.MassHeredity, darwinTraits.Mass, otherTraits.Mass);

                // Mutation. Create own trait.
                if (traitStat == -1)
                    traits.Mass = Random.Range(0.05f, 0.5f);
                else // No mutation. Inherit traits from parents
                    traits.Mass = traitStat;

                // Create chance for mass value of the child to be inherited for its own child
                traits.MassHeredity = Random.Range(0.0f, 0.5f);



                // ChargeCoolDown
                traitStat = heredity(darwinTraits.ChargeCoolDownHeredity, otherTraits.ChargeCoolDownHeredity, darwinTraits.ChargeCoolDown, otherTraits.ChargeCoolDown);

                if(traitStat == -1)
                    traits.ChargeCoolDown = Random.Range(1.0f, 5.0f);
                else
                    traits.ChargeCoolDown = traitStat;

                traits.ChargeCoolDownHeredity = Random.Range(0.0f, 0.5f);



                // ChargeStrength
                traitStat = heredity(darwinTraits.ChargeStrengthHeredity, otherTraits.ChargeStrengthHeredity, darwinTraits.ChargeStrength, otherTraits.ChargeStrength);

                if(traitStat == -1)
                    traits.ChargeStrength = Random.Range(100.0f, 200.0f);
                else
                    traits.ChargeStrength = traitStat;

                traits.ChargeStrengthHeredity = Random.Range(0.0f, 0.5f);



                // BoredThreshold
                traitStat = heredity(darwinTraits.BoredThresholdHeredity, otherTraits.BoredThresholdHeredity, darwinTraits.BoredThreshold, otherTraits.BoredThreshold);

                if (traitStat == -1)
                    traits.BoredThreshold = (byte) Random.Range(3, 6);
                else
                    traits.BoredThreshold = (byte) traitStat;

                traits.BoredThresholdHeredity = Random.Range(0.0f, 0.5f);



                // DeltaDeviationAngle
                traitStat = heredity(darwinTraits.DeltaDeviationAngleHeredity, otherTraits.DeltaDeviationAngleHeredity, darwinTraits.DeltaDeviationAngle, otherTraits.DeltaDeviationAngle);

                if(traitStat == -1)
                    traits.DeltaDeviationAngle = Random.Range(0.0f, 15.0f);
                else
                    traits.DeltaDeviationAngle = traitStat;

                traits.DeltaDeviationAngleHeredity = Random.Range(0.0f, 0.5f);



                // EnergyPerCharge
                traitStat = heredity(darwinTraits.EnergyPerChargeHeredity, otherTraits.EnergyPerChargeHeredity, darwinTraits.EnergyPerCharge, otherTraits.EnergyPerCharge);

                if (traitStat == -1)
                    traits.EnergyPerCharge = (byte) Random.Range(1, 6);
                else
                    traits.EnergyPerCharge = (byte) traitStat;

                traits.EnergyPerChargeHeredity = Random.Range(0.0f, 0.5f);



                // BreedEnergy
                traitStat = heredity(darwinTraits.BreedEnergyHeredity, otherTraits.BreedEnergyHeredity, darwinTraits.BreedEnergy, otherTraits.BreedEnergy);

                if (traitStat == -1)
                    traits.BreedEnergy = (byte) Random.Range(40, 101);
                else
                    traits.BreedEnergy = (byte) traitStat;

                traits.BreedEnergyHeredity = Random.Range(0.0f, 0.5f);



                // Steal half of energy from both parents
                traits.Energy = (short) (darwinTraits.Energy / 2 + otherTraits.Energy / 2);

                // Deplete parents' energy by half
                otherTraits.Energy = (short) (otherTraits.Energy / 2);
                darwinTraits.Energy = (short) (darwinTraits.Energy / 2);
                
            }
        }
    }
}
