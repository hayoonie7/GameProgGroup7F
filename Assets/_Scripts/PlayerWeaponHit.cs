using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHit : MonoBehaviour
{
    public int damageAmount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LevelManager.isGameOver) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth) enemyHealth.TakeDamage(damageAmount);
        }
    }
}
