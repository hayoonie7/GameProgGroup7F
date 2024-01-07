using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehavior : EnemyBehavior
{
    [SerializeField] private AudioClip shootSFX;
    private float arrowReleaseFinishTime;
    private bool releasedArrow = false;
    private bool resetAnim;
    
    
    [SerializeField] private GameObject arrowInHand;
    [SerializeField] private GameObject arrowProjectilePrefab;
    [SerializeField] private float arrowSpeed = 10f;
    
    [SerializeField] private float arrowReleaseTime;

    //public int damageAmount = 1;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) return;
        if (!dead)
        {
            transform.LookAt(player);
        }
        if (Time.time < this.arrowReleaseFinishTime)
        {
            return;
        }
        this.arrowInHand.SetActive(false);
        if (!this.releasedArrow && Time.time < this.finishTime && !dead)
        {
            Rigidbody rb = Instantiate(this.arrowProjectilePrefab, this.arrowInHand.transform.position, this.arrowInHand.transform.rotation).GetComponent<Rigidbody>();
            rb.gameObject.transform.up = -this.transform.forward;
            rb.AddForce((this.transform.forward) * this.arrowSpeed, ForceMode.Impulse);
            this.releasedArrow = true;
            Destroy(rb.gameObject, 5f);
        }
        if (Time.time < this.finishTime && !dead)
        {
            return;
        }
        if (Time.time < this.finishTime)
        {
            return;
        }
        float step = moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);
        
        if (this.resetAnim)
        {
            this.animator.SetBool("Static_b", true);
            this.animator.SetInteger("WeaponType_int", 0);
            this.animator.SetInteger("MeleeType_int", 0);
            this.releasedArrow = true;

            this.animator.SetBool("Shoot_b", false);
            this.resetAnim = false;
            return;
        }

        if (distance > minDistance && !dead)
        {
            animator.SetFloat("Speed_f", moveSpeed);
            animator.SetInteger("WeaponType_int", 0);
            animator.SetInteger("MeleeType_int", 0);
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
        else if (dead)
        {
            animator.SetFloat("Speed_f", 0);
            animator.SetInteger("WeaponType_int", 0);
            animator.SetInteger("MeleeType_int", 0);
        }
        else
        {
            this.animator.SetBool("Static_b", true);
            this.animator.SetFloat("Speed_f", 0);
            this.animator.SetInteger("WeaponType_int", 11);
            this.animator.SetInteger("MeleeType_int", 0);
            this.animator.SetBool("Shoot_b", true);
            this.resetAnim = true;
            this.finishTime = Time.time + this.attackAnimationLength;
            this.arrowReleaseFinishTime = Time.time + this.arrowReleaseTime;
            this.releasedArrow = false;
            this.arrowInHand.SetActive(true);
            AudioSource.PlayClipAtPoint(this.shootSFX, this.transform.position);
        }
    }

    /*
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("PlayerWeapon") && this.player.GetComponent<Animator>().GetInteger("WeaponType_int") > 0) {
            dead = true;
            this.animator.SetBool("Death_b", true);
            Destroy(gameObject,2);
        }
    }
    */

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            //var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            //enemyHealth.TakeDamage(damageAmount);
        }
    }
}
