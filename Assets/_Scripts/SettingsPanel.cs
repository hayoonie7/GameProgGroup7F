using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volume", 1);
    }

    void Update()
    {
        AudioListener.volume = slider.value;
    }

    public void OnSliderValueChanged()
    {
        PlayerPrefs.SetFloat("volume", slider.value);
    }
}
