using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public bool disappearOnPickup = true;
    public KeyCode pickupKey;

    public PlayerBehavior playerBehavior;

    private bool doPickup = false;

    public string weaponName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) return;
        if (Input.GetKeyDown(pickupKey) && doPickup)
        {
            playerBehavior.SetWeapon(weaponName);
            // playerBehavior.SetDamageType("");
            doPickup = false;
            if (disappearOnPickup) Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (LevelManager.isGameOver) return;
        if (other.CompareTag("Player"))
        { 
            doPickup = true;
            if (this.playerBehavior == null)
            {
                this.playerBehavior = other.GetComponent<PlayerBehavior>();
            }
        }
        else
        {
            doPickup = false;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (LevelManager.isGameOver) return;
        doPickup = false;
    }
}
