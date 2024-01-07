using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5;
    public float minDistance = 2;
    [SerializeField] protected int meleeType = 0;
    [SerializeField] protected int weaponType = 0;
    [SerializeField] private AudioClip swingSFX;

    protected Animator animator;

    protected bool dead = false;

    [SerializeField] protected float attackAnimationLength;
    protected float finishTime;

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
        if (Time.time < this.finishTime && !dead)
        {
            return;
        }
        float step = moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!dead)
        {
            transform.LookAt(player);
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
            animator.SetFloat("Speed_f", 0);
            animator.SetInteger("WeaponType_int", this.weaponType);
            animator.SetInteger("MeleeType_int", this.meleeType);
            this.finishTime = Time.time + this.attackAnimationLength;
            AudioSource.PlayClipAtPoint(this.swingSFX, this.transform.position);
        }
    }

    public void Die()
    {
        this.dead = true;
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
