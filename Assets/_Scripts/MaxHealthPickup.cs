using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaxHealthPickup : MonoBehaviour
{
    public int amountGain = 10;
    public GameObject player;
    public AudioClip maxHealthSfx;
    private PlayerHealth _playerHealth;
    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHealth.ChangeCurrentMaxHealth(amountGain);
            AudioSource.PlayClipAtPoint(maxHealthSfx, transform.position);
            Destroy(gameObject);
        }
    }
    
}
