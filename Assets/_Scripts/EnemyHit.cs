using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] public int damageAmount;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player for " + this.damageAmount.ToString() + " damage");
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(this.damageAmount);
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (LevelManager.isGameOver) return;
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player for " + this.damageAmount.ToString() + " damage");
            collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(this.damageAmount);
        }
    }
}
