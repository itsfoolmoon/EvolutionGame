using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void SetDarwinPopulation (float size)
    {
        SettingsManager.Instance.darwinPopulation = (int) size;
    }
}
