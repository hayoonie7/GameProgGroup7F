using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip hitSFX;
    public Slider healthSlider;
    public GameObject background;
    public GameObject fillArea;
    public int maxPossibleHealth = 300;
    private int _currentMaxHealth;
    private int _currentHealth;
    private Vector2 _originalBoxSize;
    private int _originalMaxHealth;
    private RectTransform _healthSliderTransform;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //_currentHealth = startingHealth;
        _currentHealth = PlayerInfo.currentHealth;
        _currentMaxHealth = PlayerInfo.maxHealth;
        print(_currentHealth);
        print(_currentMaxHealth);
        healthSlider.maxValue = PlayerInfo.maxHealth;
        healthSlider.value = _currentHealth;
        _originalBoxSize = healthSlider.GetComponent<RectTransform>().sizeDelta;
        _originalMaxHealth = 100;
        _healthSliderTransform = healthSlider.GetComponent<RectTransform>();
        float newXPos = _healthSliderTransform.sizeDelta.x 
                         + (float)(_currentMaxHealth - _originalMaxHealth) / _originalMaxHealth * _originalBoxSize.x;
         float xValue = Mathf.Clamp(newXPos, _originalBoxSize.x, _originalBoxSize.x * 3);
         _healthSliderTransform.sizeDelta = new Vector2(xValue, _healthSliderTransform.sizeDelta.y);
        _currentHealth = PlayerInfo.currentHealth;
        healthSlider.value = _currentHealth;
    }
    
    void Update()
    {
        if (LevelManager.isGameOver) return;
        PlayerInfo.currentHealth = _currentHealth;
        PlayerInfo.maxHealth = _currentMaxHealth;
    }

    public void TakeDamage(int damageAmount) {
        if (LevelManager.isGameOver) return;
        if (_currentHealth > 0) {
            _currentHealth -= damageAmount;
            healthSlider.value = _currentHealth;
            AudioSource.PlayClipAtPoint(hitSFX, this.transform.position);
        }  
        
        if (_currentHealth <= 0) {
            PlayerDies();
        }

        //Debug.Log(currentHealth);
    }

    void PlayerDies() {
        if (LevelManager.isGameOver) return;
        Debug.Log("Player is dead");
        var animator = GetComponent<Animator>();
        animator.SetFloat("Speed_f", 0);
        animator.SetBool("Death_b", true);
        animator.SetInteger("DeathType_int", 1);
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerMeleeAttack>().enabled = false;
        GetComponent<PlayerBowAttack>().enabled = false;
        FindObjectOfType<LevelManager>().LevelLost();
        //AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        //transform.Rotate(-90, 0, 0, Space.Self);
    }

     public void TakeHealth(int healthAmount) {
        if (LevelManager.isGameOver) return;
        if (_currentHealth < _currentMaxHealth) {
            _currentHealth += healthAmount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _currentMaxHealth);
            healthSlider.value = Mathf.Clamp(_currentHealth, 0, _currentMaxHealth);
        }  
        
    }

     public void ChangeCurrentMaxHealth(int amount)
     {
         _currentMaxHealth += amount;
         _currentMaxHealth = Mathf.Clamp(_currentMaxHealth, _originalMaxHealth, maxPossibleHealth);
         float newXPos = _healthSliderTransform.sizeDelta.x 
                         + (float)amount / _originalMaxHealth * _originalBoxSize.x;
         float xValue = Mathf.Clamp(newXPos, _originalBoxSize.x, _originalBoxSize.x * 3);
         _healthSliderTransform.sizeDelta = new Vector2(xValue, _healthSliderTransform.sizeDelta.y);
         healthSlider.maxValue = _currentMaxHealth;
         _currentHealth = _currentMaxHealth;
         healthSlider.value = _currentHealth;
     }
}
