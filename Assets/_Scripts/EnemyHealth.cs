using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 5;
    //public AudioClip deadSFX;
    public AudioClip hitSFX;
    public int currentHealth;


    void Start()
    {
        currentHealth = startingHealth;
     }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (LevelManager.isGameOver) return;
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            AudioSource.PlayClipAtPoint(hitSFX, this.transform.position);

        }
        if (currentHealth <= 0)
        {
           EnemyDies();
        }
        Debug.Log("Current health: " + currentHealth);
    }

    void EnemyDies()
    {
        if (LevelManager.isGameOver) return;
        Debug.Log("Enemy dies");
        //AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        //Destroy(gameObject);
        var eb = this.GetComponent<EnemyBehavior>();
        if (eb)
        {
            eb.Die();
            Destroy(gameObject, 2);
        }
        else
        {
            
        }
        this.GetComponent<Animator>().SetBool("Death_b", true);
        FindObjectOfType<LevelManager>().AddScore(1);
    }

 


}
