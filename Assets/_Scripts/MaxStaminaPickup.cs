using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxStaminaPickup : MonoBehaviour
{
    public float amountGain = 10f;
    public AudioClip maxStaminaSfx;
    public Slider staminaSlider;

    private RectTransform _staminaTransform;
    private float _currentMaxStamina;
    private Vector2 _originalStaminaSize;
    private int _originalStamina;
    private float _maxPossibleStamina = 300;
    // Start is called before the first frame update
    void Start()
    {
        staminaSlider = FindObjectOfType<StaminaManager>().staminaBar;
        _currentMaxStamina = PlayerInfo.maxStamina;
        _staminaTransform = staminaSlider.GetComponent<RectTransform>();
        _originalStaminaSize = _staminaTransform.sizeDelta;
        _originalStamina = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInfo.maxStamina += amountGain;
            PlayerInfo.maxStamina = 
                Mathf.Clamp(PlayerInfo.maxStamina, _currentMaxStamina, _maxPossibleStamina);
            _currentMaxStamina = PlayerInfo.maxStamina;
            float newXPos = _staminaTransform.sizeDelta.x 
                            + amountGain / _originalStamina * _originalStaminaSize.x;
            float xValue = Mathf.Clamp(newXPos, _originalStaminaSize.x, _originalStaminaSize.x * 3);
            _staminaTransform.sizeDelta = new Vector2(xValue, _staminaTransform.sizeDelta.y);
            staminaSlider.maxValue = PlayerInfo.maxStamina;
            staminaSlider.value = PlayerInfo.maxStamina;
            AudioSource.PlayClipAtPoint(maxStaminaSfx, transform.position);
            Destroy(gameObject);
        }
    }
}
