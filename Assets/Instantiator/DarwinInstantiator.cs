using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarwinInstantiator : MonoBehaviour
{
    public GameObject darwin;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            DarwinTraits traits = darwin.GetComponent<DarwinTraits>();

            //prefab.GetComponent<DarwinTraits>().setMass(Random.Range(0.05f, 1.0f));
            //prefab.GetComponent<DarwinTraits>().setChargeCoolDown(Random.Range(1.0f, 5.0f));
            //prefab.GetComponent<DarwinTraits>().setChargeStrength(Random.Range(100.0f, 200.0f));
            //prefab.GetComponent<DarwinTraits>().setDeltaDeviationAngle(Random.Range(0.0f, 60.0f));
            //prefab.GetComponent<DarwinTraits>().setBreedEnergy(10);

            traits.setMass(Random.Range(0.05f, 0.5f));
            traits.setChargeCoolDown(Random.Range(1.0f, 5.0f));
            traits.setChargeStrength(Random.Range(100.0f, 200.0f));
            traits.setDeltaDeviationAngle(Random.Range(0.0f, 15.0f));
            traits.setBreedEnergy(Random.Range(40, 100));
            traits.setEnergy(Random.Range(10, traits.getBreedEnergy()));

            traits.setMassHeredity(Random.Range(0.0f, 0.5f));
            traits.setChargeCoolDownHeredity(Random.Range(0.0f, 0.5f));
            traits.setChargeStrengthHeredity(Random.Range(0.0f, 0.5f));
            traits.setDeltaDeviationAngleHeredity(Random.Range(0.0f, 0.5f));
            traits.setBreedEnergyHeredity(Random.Range(0.0f, 0.5f));
            
            Instantiate(darwin, new Vector3(Random.Range(-10.0f,10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity);
        }
    }
}
