using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    //Externally Referenced Variables
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip zombieHit;
    [SerializeField] private AudioClip zombieDeath;
    public float offset;

    //Private variables
    private NavMeshAgent agent;
    private PlayerData player;
    private Rigidbody rb;
    private BoxCollider attackHitbox;
    private AudioSource audioSource;
    
    bool isAttacking = false;
    bool canAttack = false;
    bool playerInRange = false;

    //Zombie Stats
    float attackRange = 0.0f;
    float damage = 5.0f;
    [SerializeField]float speed = 10.0f;
    [SerializeField] float animationSpeed = 1.0f;
    [SerializeField] float attackDelay = 1.0f;
    float zombieHealth = 100.0f;
    

    // Start is called before the first frame update
    private void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateZombieBehaviour();
        UpdateAnimations();
    }

    //Get References to required components
    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        player = target.GetComponentInParent<PlayerData>();
        rb = GetComponent<Rigidbody>();
        attackHitbox = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    //Initialise zombie stats through Zombie Spawner
    public void IntitializeZombieStats(float attackRange, float damage, float speed, float animationSpeed , float attackDelay , float zombieHealth, Transform target)
    {
        this.attackRange = attackRange;
        this.damage = damage;
        this.speed = speed;
        this.target = target;
        this.animationSpeed = animationSpeed;
        this.attackDelay = attackDelay;
        this.zombieHealth = zombieHealth;
    }

    private void UpdateZombieBehaviour()
    {
        if (CheckForPlayer())
        {
            //attack player
            rb.velocity = new Vector3 (0,0,0);
            agent.isStopped = true;

            isAttacking = true;
            DamagePlayer();
        }
        else
        {
            agent.isStopped = false;
            MoveToTarget();
            isAttacking = false;
        }
    }

    //Checking if player is in the attackable hitbox range
    private bool CheckForPlayer()
    {
        return playerInRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Can Attack");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Cant Attack");
            playerInRange = false;
        }
    }



    //Move Zombie to target
    private void MoveToTarget()
    {
        agent.speed = speed;
        agent.SetDestination(target.position);
    }

    //Damage the player
    private void DamagePlayer()
    {
        if(canAttack)
        {
            canAttack = false;
            player.TakeDamage(damage);
            Debug.Log("Damaging Player");
            audioSource.PlayOneShot(zombieHit);
            
        }
        else
        {
            StartCoroutine(DamageDelay());
        }
    }

    //Couroutine to wait for few seconds
    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    //Update the animations using animator variables
    private void UpdateAnimations()
    {
        animator.SetBool("isAttacking", isAttacking);
        animator.speed = animationSpeed;
    }

    //public function to inflict damage
    public void TakeDamage(float damage)
    {
        zombieHealth -= damage;
        Debug.Log(zombieHealth);
        if(zombieHealth <= 0.0f)
        {
            //Kill Zombie
            ZombieDeath();
        }
    }

    //Function called on zombie death
    private void ZombieDeath()
    {
        Destroy(gameObject);
    }

    //getters and setters
    public float GetZombieDamage()
    {
        return damage;
    }
}
