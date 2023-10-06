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
        else if(random <= darwinHeredity + otherHeredity)
            return ((darwinHeredity + otherHeredity) * otherTrait + darwinHeredity * darwinTrait) / (darwinHeredity * 2 + otherHeredity);

        return -1;
    }

    void OnTriggerEnter2D(Collider2D other) // Trigger(used for cookies)
    {
        DarwinTraits darwinTraits = gameObject.GetComponent<DarwinTraits>();
        CookieInstantiator cookieScript = cookieInstantiator.GetComponent<CookieInstantiator>();

        darwinTraits.Boredom = 0;

        if (other.gameObject.CompareTag("Cookie"))
        {
            //Debug.Log("Ate a Cookie!");
            Destroy(other.gameObject);
            darwinTraits.Energy += 5;
            cookieScript.spawnCookie();
        }
    }

    void OnCollisionEnter2D(Collision2D other) // Collision(used for other frogs)
    {
        DarwinTraits otherTraits = other.gameObject.GetComponent<DarwinTraits>(); // parent 1
        DarwinTraits darwinTraits = gameObject.GetComponent<DarwinTraits>(); // parent 2

        darwinTraits.Boredom = 0;

        if (other.gameObject.CompareTag("Darwin"))
        {
            if (otherTraits.canBreed() && darwinTraits.canBreed())
            {
                if (otherTraits.Mass > darwinTraits.Mass)
                {
                    GameObject newObject = Instantiate(darwin, other.transform.position, Quaternion.identity);
                    DarwinTraits traits = newObject.GetComponent<DarwinTraits>(); // what traits the offspring will inherit

                    // Mass
                    float traitStat = heredity(darwinTraits.MassHeredity, otherTraits.MassHeredity, darwinTraits.Mass, otherTraits.Mass);

                    if(traitStat == -1)
                        traits.Mass = Random.Range(0.05f, 0.5f);
                    else
                        traits.Mass = traitStat;

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

                    // Energy
                    traits.Energy = (short) (darwinTraits.Energy / 2 + otherTraits.Energy / 2);

                    // Deplete parents' energy
                    otherTraits.Energy = (short) (otherTraits.Energy / 2);
                    darwinTraits.Energy = (short) (darwinTraits.Energy / 2);
                }
            }
        }
    }
}
