using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWeaponDuringAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider myCollider;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) return;
        if (animator.GetInteger("WeaponType_int") > 0 && animator.GetInteger("MeleeType_int") > 0)
        {
            this.myCollider.enabled = true;
        }
        else
        {
            this.myCollider.enabled = false;
        }
    }
}
