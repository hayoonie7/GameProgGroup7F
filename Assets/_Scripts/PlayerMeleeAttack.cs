using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private float animationLength;
    private Animator animator;
    private float finishTime;
    private PlayerController playerController;
    [SerializeField] private AudioClip swingSFX;

    private bool resetAnim;
    private bool hasMeleeWeapon = true;

    // Start is called before the first frame update
    void Start()
    {
        this.playerController = GetComponent<PlayerController>();
        
        this.animator = GetComponent<Animator>();
        this.finishTime = 0;

        this.animator.SetBool("Static_b", true);
        this.animator.SetInteger("WeaponType_int", 0);
        this.animator.SetInteger("MeleeType_int", 1);

        this.resetAnim = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) return;
        if (Time.time < this.finishTime)
        {
            return;
        }
        else
        {
            if (this.resetAnim)
            {
                this.playerController.enabled = true;
                this.animator.SetBool("Static_b", true);
                this.animator.SetInteger("WeaponType_int", 0);
                this.animator.SetInteger("MeleeType_int", 1);
                this.resetAnim = false;
            }
        }
        if (Input.GetButtonDown("Fire1") && Time.time > this.finishTime && this.hasMeleeWeapon)
        {
            this.playerController.enabled = false;
            var mousePos = Helpers.GetMousePosition3D(new Plane(Vector3.up, this.transform.position));
            this.transform.LookAt(mousePos);
            this.animator.SetBool("Static_b", true);
            this.animator.SetFloat("Speed_f", 0);
            this.animator.SetInteger("WeaponType_int", 12);
            this.animator.SetInteger("MeleeType_int", 1);
            this.resetAnim = true;
            this.finishTime = Time.time + this.animationLength;
            AudioSource.PlayClipAtPoint(this.swingSFX, this.transform.position);
        }
    }
}
