// TODO: Use C# style getters and setters

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarwinTraits : MonoBehaviour
{
    public float mass;

    public float chargeCoolDown;
    public float chargeStrength;

    // This is to ensure that Darwin does not get stuck trying to get an inaccessible cookie.
    // Will be reset when Darwin is done being bored or if it collides into anything.
    public int boredom { get; set; }
    // If boredom goes beyond this threshold, Darwin will become bored(wander around aimlessly).
    public int boredThreshold { get; set; }

    public float deltaDeviationAngle;

    public int energy;
    public int energyPerCharge;
    public int breedEnergy;


    public float massHeredity;
    
    public float chargeCoolDownHeredity;
    public float chargeStrengthHeredity;

    public float deltaDeviationAngleHeredity;

    public float energyHeredity;
    public float energyPerChargeHeredity;
    public float breedEnergyHeredity;

    public DarwinTraits()
    {
        mass = 0;
        chargeCoolDown = 0;
        chargeStrength = 0;
        boredom = 0;
        boredThreshold = 5;
        deltaDeviationAngle = 0;
        energy = 0;
        energyPerCharge = 0;
        breedEnergy = 0;

        massHeredity = 0;
        chargeCoolDownHeredity = 0;
        chargeStrengthHeredity = 0;
        deltaDeviationAngleHeredity = 0;
        energyHeredity = 0;
        energyPerChargeHeredity = 0;
        breedEnergyHeredity = 0;
    }

    public DarwinTraits(float mass, float chargeCoolDown, float chargeStrength, float deltaDeviationAngle, int energy, int energyPerCharge, int breedEnergy,
    float massHeredity, float chargeCoolDownHeredity, float chargeStrengthHeredity, float deltaDeviationAngleHeredity, int energyHeredity, int energyPerChargeHeredity, int breedEnergyHeredity)
    {
        this.mass = mass;
        this.chargeCoolDown = chargeCoolDown;
        this.chargeStrength = chargeStrength;
        this.deltaDeviationAngle = deltaDeviationAngle;
        this.energy = energy;
        this.energyPerCharge = energyPerCharge;
        this.breedEnergy = breedEnergy;

        this.massHeredity = massHeredity;
        this.chargeCoolDownHeredity = chargeCoolDownHeredity;
        this.chargeStrengthHeredity = chargeStrengthHeredity;
        this.deltaDeviationAngleHeredity = deltaDeviationAngleHeredity;
        this.energyHeredity = energyHeredity;
        this.energyPerChargeHeredity = energyPerChargeHeredity;
        this.breedEnergyHeredity = breedEnergyHeredity;
    }

    /* getters */
    public float getMass() { return mass; }
    public float getChargeCoolDown() { return chargeCoolDown; }
    public float getChargeStrength() { return chargeStrength; }
    public float getDeltaDeviationAngle() { return deltaDeviationAngle; }
    public int getEnergy() { return energy; }
    public int getBreedEnergy() { return breedEnergy; }

    public float getMassHeredity() { return massHeredity; }
    public float getChargeCoolDownHeredity() { return chargeCoolDownHeredity; }
    public float getChargeStrengthHeredity() { return chargeStrengthHeredity; }
    public float getDeltaDeviationAngleHeredity() { return deltaDeviationAngleHeredity; }
    public float getEnergyHeredity() { return energyHeredity; }
    public float getBreedEnergyHeredity() { return breedEnergyHeredity; }

    /* setters */
    public void setMass(float mass) { this.mass = mass; }
    public void setChargeCoolDown(float chargeCoolDown) { this.chargeCoolDown = chargeCoolDown; }
    public void setChargeStrength(float chargeStrength) { this.chargeStrength = chargeStrength; }
    public void setDeltaDeviationAngle(float deltaDeviationAngle) { this.deltaDeviationAngle = deltaDeviationAngle; }
    public void setEnergy(int energy) { this.energy = energy; }
    public void setBreedEnergy(int breedEnergy) { this.breedEnergy = breedEnergy; }

    public void setMassHeredity(float massHeredity) { this.massHeredity = massHeredity; }
    public void setChargeCoolDownHeredity(float chargeCoolDownHeredity) { this.chargeCoolDownHeredity = chargeCoolDownHeredity; }
    public void setChargeStrengthHeredity(float chargeStrengthHeredity) { this.chargeStrengthHeredity = chargeStrengthHeredity; }
    public void setDeltaDeviationAngleHeredity(float deltaDeviationAngleHeredity) { this.deltaDeviationAngleHeredity = deltaDeviationAngleHeredity; }
    public void setEnergyHeredity(float energyHeredity) { this.energyHeredity = energyHeredity; }
    public void setBreedEnergyHeredity(float breedEnergyHeredity) { this.breedEnergyHeredity = breedEnergyHeredity; }

    /* miscellaneous */
    public bool canBreed()
    {
        if(energy >= breedEnergy)
            return true;
        return false;
    }

    public bool isLiving()
    {
        if(energy > 0)
            return true;
        return false;
    }

    public bool isBored()
    {
        if (boredom >= boredThreshold)
            return true;
        return false;
    }
}
