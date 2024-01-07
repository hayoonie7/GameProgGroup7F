using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaRefiller : MonoBehaviour
{
    [SerializeField, Tooltip("How many stamina points per second to refill")] private float refillSpeed;
    [SerializeField] private Slider staminaSlider;
    // Start is called before the first frame update
    void Start()
    {
        this.staminaSlider.value = PlayerInfo.maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) return;
        this.staminaSlider.value = Mathf.Lerp(0, 
            this.staminaSlider.maxValue, 
            (this.staminaSlider.value + Time.deltaTime * this.refillSpeed) / this.staminaSlider.maxValue);
    }
}
