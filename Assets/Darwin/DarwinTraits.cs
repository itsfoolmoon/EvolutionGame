/**
This code is applied on the Darwin gameObject. It is used to store the traits the Darwin should have.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarwinTraits : MonoBehaviour
{
    // The mass of the Darwin
    public float Mass { get; set; }

    // The interval (in terms of seconds) between each time the Darwin "lunges" forward.
    public float ChargeCoolDown { get; set; }

    // How strong the Darwin will "lunge" forward. In terms of force value.
    public float ChargeStrength { get; set; }

    // This is to ensure that Darwin does not get stuck trying to get an inaccessible cookie.
    // Will be reset when Darwin is done being bored or if it collides into anything.
    public byte Boredom { get; set; }
    // If boredom goes beyond this threshold, Darwin will become bored(wander around aimlessly).
    public byte BoredThreshold { get; set; }

    // The accuracy of the Darwin when it charges at a desired angle.
    public float DeltaDeviationAngle { get; set; }

    // The current energy of the Darwin. Decreases per charge and birthing child. Increases upon consuming a cookie.
    public short Energy { get; set; }
    // Amount of energy consumed per charge.
    public byte EnergyPerCharge { get; set; }
    //The energy required for the Darwin to be able to breed.
    public byte BreedEnergy { get; set; }

    // Chance for mass value of the Darwin to be inherited for its own child
    public float MassHeredity { get; set; }

    public float ChargeCoolDownHeredity { get; set; }
    public float ChargeStrengthHeredity { get; set; }

    // public float BoredomHeredity { get; set; }
    public float BoredThresholdHeredity { get; set; }

    public float DeltaDeviationAngleHeredity { get; set; }

    // public double EnergyHeredity { get; set; }
    public float EnergyPerChargeHeredity { get; set; }
    public float BreedEnergyHeredity { get; set; }

    public DarwinTraits()
    {
        Mass = 0;
        ChargeCoolDown = 0;
        ChargeStrength = 0;
        Boredom = 0;
        BoredThreshold = 0;
        DeltaDeviationAngle = 0;
        Energy = 0;
        EnergyPerCharge = 0;
        BreedEnergy = 0;

        MassHeredity = 0;
        ChargeCoolDownHeredity = 0;
        ChargeStrengthHeredity = 0;
        DeltaDeviationAngleHeredity = 0;
        // EnergyHeredity = 0;
        EnergyPerChargeHeredity = 0;
        BreedEnergyHeredity = 0;
    }

    public DarwinTraits(float mass, float chargeCoolDown, float chargeStrength, float deltaDeviationAngle, short energy, byte energyPerCharge, byte breedEnergy,
    float massHeredity, float chargeCoolDownHeredity, float chargeStrengthHeredity, float deltaDeviationAngleHeredity, float energyPerChargeHeredity, float breedEnergyHeredity)
    {
        Mass = mass;
        ChargeCoolDown = chargeCoolDown;
        ChargeStrength = chargeStrength;
        DeltaDeviationAngle = deltaDeviationAngle;
        Energy = energy;
        EnergyPerCharge = energyPerCharge;
        BreedEnergy = breedEnergy;

        MassHeredity = massHeredity;
        ChargeCoolDownHeredity = chargeCoolDownHeredity;
        ChargeStrengthHeredity = chargeStrengthHeredity;
        DeltaDeviationAngleHeredity = deltaDeviationAngleHeredity;
        // energyHeredity = energyHeredity;
        EnergyPerChargeHeredity = energyPerChargeHeredity;
        BreedEnergyHeredity = breedEnergyHeredity;
    }

    /* miscellaneous */
    public bool canBreed() { return Energy >= BreedEnergy; }

    public bool isLiving() { return Energy > 0; }

    public bool isBored() { return Boredom >= BoredThreshold; }
}
