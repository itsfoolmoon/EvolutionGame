using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;

    private void Start()
    {
        // Listen for changes to the slider value
        slider.onValueChanged.AddListener(UpdateValueText);
        UpdateValueText(slider.value);
    }

    private void UpdateValueText(float value)
    {
        // Update the text to match the slider value
        valueText.text = value.ToString();
    }
}
