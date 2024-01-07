using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public GameObject sword;
    public GameObject swordPrefab;
    public GameObject bow;

    public GameObject bowPrefab;
    // Start is called before the first frame update
    void Start()
    {
        this.SetWeapon(PlayerInfo.weaponType);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) return;
    }

    // Set the player's current weapon
    public void SetWeapon(string weaponName)
    {
        PlayerInfo.weaponType = weaponName;
        switch (weaponName)
        {
            case "bow":
                bow.SetActive(true);
                //GameObject newSword = 
                  //  Instantiate(swordPrefab, transform.position, transform.rotation) as GameObject;
                sword.SetActive(false);
                GetComponent<PlayerMeleeAttack>().enabled = false;
                GetComponent<PlayerBowAttack>().enabled = true;
                break;
            
            case "sword":
                sword.SetActive(true);
                //GameObject newBow = 
                  //  Instantiate(bowPrefab, transform.position, transform.rotation) as GameObject;
                bow.SetActive(false);
                GetComponent<PlayerMeleeAttack>().enabled = true;
                GetComponent<PlayerBowAttack>().enabled = false;
                break;
        }
    }

    // Set the damageType of the current weapon
    public void SetDamageType(string damageType)
    {
        switch (damageType)
        {
            case "":
                // Child of weapon type set enabled
                break;
            
            case "sword":
                // Child of weapon type set enabled
                break;
        }
    }
    


}
