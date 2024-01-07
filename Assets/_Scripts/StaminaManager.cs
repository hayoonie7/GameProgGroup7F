using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    [SerializeField] public Slider staminaBar;
    
    void Start()
    {
        var _currentMaxStamina = PlayerInfo.maxStamina;
        var _staminaTransform = staminaBar.GetComponent<RectTransform>();
        var _originalStaminaSize = _staminaTransform.sizeDelta;
        var _originalStamina = 100;
        staminaBar.maxValue = PlayerInfo.maxStamina;
        float newXPos = _staminaTransform.sizeDelta.x 
                            + (PlayerInfo.maxStamina - 100) / _originalStamina * _originalStaminaSize.x;
        float xValue = Mathf.Clamp(newXPos, _originalStaminaSize.x, _originalStaminaSize.x * 3);
        _staminaTransform.sizeDelta = new Vector2(xValue, _staminaTransform.sizeDelta.y);
        staminaBar.maxValue = PlayerInfo.maxStamina;
        staminaBar.value = PlayerInfo.maxStamina;
    }
    
    void Update()
    {
        if (LevelManager.isGameOver) return;
        PlayerInfo.maxStamina = staminaBar.maxValue;
    }
}
