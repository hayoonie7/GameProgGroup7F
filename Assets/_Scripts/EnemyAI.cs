using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public bool showGizmos = true;
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }
    
    public AudioClip attackSFX;
    
    public GameObject deathEffect;

    public FSMStates currentState;
    public float enemySpeed = 3f;
    public GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator anim;

    public float chaseDistance = 10;
    public float distanceToPlayer;

    int currentDestinationIndex = 0;

    public GameObject player;

    public float attackDistance = 5;

    public GameObject[] spellProjectiles;

    public GameObject shootPoint;

    public float shootRate;

    public float elapsedTime = 0;

    EnemyHealth enemyHealth;
    int health;

    public GameObject deadVFX;
    Transform deadTransform;

    bool isDead = false;
    
    NavMeshAgent agent;
    
    public Transform enemyEyes;
    
    public float fieldOfView = 45f;

    // Start is called before the first frame update
    void Start()
    {
        //this.wanderPoints = GameObject.FindGameObjectsWithTag("wanderpoint");
        this.anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        health = enemyHealth.currentHealth;
        agent = GetComponent<NavMeshAgent>();
        FindNextPoint();
        this.Initialize();
        //anim.SetInteger("animState", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        print(nextDestination);

        health = enemyHealth.currentHealth;

        switch(this.currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }

        elapsedTime += Time.deltaTime;

        if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }

    private void Initialize()
    {
        this.currentState = FSMStates.Patrol;
    }

    private void UpdatePatrolState()
    {
        print("Patrolling");
        
        agent.stoppingDistance = 4;

        anim.SetFloat("Speed_f", 0.5f);
        agent.speed = enemySpeed * 0.5f;
        //print(Vector3.Distance(transform.position, nextDestination));
        if (Vector3.Distance(transform.position, nextDestination) <= agent.stoppingDistance)
        {
            FindNextPoint();
        }
        if (distanceToPlayer <= chaseDistance && IsPlayerInClearFOV())
        {
            print("start chasing!");
            currentState = FSMStates.Chase;
        }
        FaceTarget(nextDestination);
        
        agent.SetDestination(nextDestination);
        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    private void UpdateChaseState()
    {
        anim.SetFloat("Speed_f", 0.8f);
        agent.stoppingDistance = 1;
        agent.speed = enemySpeed * 0.8f;

        nextDestination = player.transform.position;
        
        //agent.stoppingDistance = attackDistance;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
            elapsedTime = shootRate + 1;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);
        
        agent.SetDestination(nextDestination);
        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    private void UpdateAttackState()
    {
        print("attack");
        if (Vector3.Distance(this.transform.position, nextDestination) >= agent.stoppingDistance)
        {
            FaceTarget(nextDestination);
        }
        
        
        if (elapsedTime < shootRate) return;
        FaceTarget(player.transform.position);

        nextDestination = player.transform.position;
        agent.SetDestination(nextDestination);
        //agent.stoppingDistance = attackDistance;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        } 
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
            elapsedTime = 0;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
            elapsedTime = 0;
        }

        if (currentState == FSMStates.Attack)
        {
            EnemyAttack();
        }
        anim.SetFloat("Speed_f", 1);
    }

    private void UpdateDeadState()
    {
        //anim.SetInteger("animState", 4);
        print("Enemy is dead");
        deadTransform = transform;
        agent.SetDestination(this.transform.position);
    }

    private void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
        
        //agent.SetDestination(nextDestination);
    }

    private void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
    
    private void EnemyAttack()
    {
        if (isDead) return;
        if (elapsedTime < shootRate) return;
        
        elapsedTime = 0;
        //agent.speed = 0;
        nextDestination = player.transform.position;
        Invoke("DoAttack", 0.5f);
    }

    private void DoAttack()
    {
        agent.speed = enemySpeed * 3f;
        AudioSource.PlayClipAtPoint(this.attackSFX, this.transform.position);
        AudioSource.PlayClipAtPoint(this.attackSFX, this.transform.position);
        AudioSource.PlayClipAtPoint(this.attackSFX, this.transform.position);
        AudioSource.PlayClipAtPoint(this.attackSFX, this.transform.position);
        AudioSource.PlayClipAtPoint(this.attackSFX, this.transform.position);
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        
        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        
        Vector3 leftRayPoint = enemyEyes.position + Quaternion.AngleAxis(fieldOfView * 0.5f, enemyEyes.up) * enemyEyes.forward * chaseDistance;
        Vector3 rightRayPoint = enemyEyes.position + Quaternion.AngleAxis(fieldOfView * -0.5f, enemyEyes.up) * enemyEyes.forward * chaseDistance;
        
        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);
        if (player == null) return;
        var playerPos = player.transform.position;
        playerPos.y = enemyEyes.position.y;
        Debug.DrawLine(enemyEyes.position, playerPos, Color.blue);
    }
    
    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;
        directionToPlayer.y = enemyEyes.position.y;
        if (Vector3.Angle(directionToPlayer.normalized, enemyEyes.forward) <= fieldOfView * 0.5f)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                print(hit.collider.gameObject.tag);
                if (hit.collider.CompareTag("Player"))
                {
                    print("Player in sight!");
                    return true;
                }
            }
        }
        return false;
    }
}
