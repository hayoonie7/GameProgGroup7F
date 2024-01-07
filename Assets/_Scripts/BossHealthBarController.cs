using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{
    public Slider healthSlider;
    private EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        this.enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = ((float)enemyHealth.currentHealth / (float)enemyHealth.startingHealth) * healthSlider.maxValue;
    }
}
