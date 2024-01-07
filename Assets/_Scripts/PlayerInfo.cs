using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo
{
    
    public static string weaponType;
    
    public static int currentHealth;
    
    public static int maxHealth;
    
    public static float maxStamina;
    
    private static string startingWeaponType = "sword";
    
    private static int startingHealth = 100;
    
    private static int startingMaxHealth = 100;
    
    private static float startingMaxStamina = 100;
    
    public static void OverwriteStartingValues()
    {
        startingWeaponType = weaponType;
        startingHealth = currentHealth;
        startingMaxHealth = maxHealth;
        startingMaxStamina = maxStamina;
    }
    
    public static void LoadStartingValues()
    {
        weaponType = startingWeaponType;
        currentHealth = startingHealth;
        maxHealth = startingMaxHealth;
        maxStamina = startingMaxStamina;
    }
}
