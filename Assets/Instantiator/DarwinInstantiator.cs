﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarwinInstantiator : MonoBehaviour
{
    public GameObject darwin;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newObject = Instantiate(darwin, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity);
            DarwinTraits traits = newObject.GetComponent<DarwinTraits>();

            traits.Mass = Random.Range(0.05f, 0.5f);
            traits.ChargeCoolDown = Random.Range(1.0f, 5.0f);
            traits.ChargeStrength = Random.Range(100.0f, 200.0f);
            traits.BoredThreshold = (byte) Random.Range(3, 6);
            traits.DeltaDeviationAngle = Random.Range(0.0f, 15.0f);
            traits.EnergyPerCharge = (byte) Random.Range(1.0f, 6.0f);
            traits.BreedEnergy = (byte) Random.Range(40.0f, 101.0f);
            traits.Energy = (short) Random.Range(15.0f, 51.0f);

            traits.MassHeredity = Random.Range(0.0f, 0.5f);
            traits.ChargeCoolDownHeredity = Random.Range(0.0f, 0.5f);
            traits.ChargeStrengthHeredity = Random.Range(0.0f, 0.5f);
            traits.BoredThresholdHeredity = Random.Range(0.0f, 0.5f);
            traits.EnergyPerChargeHeredity = Random.Range(0.0f, 0.5f);
            traits.DeltaDeviationAngleHeredity = Random.Range(0.0f, 0.5f);
            traits.BreedEnergyHeredity = Random.Range(0.0f, 0.5f);
        }
    }
}
