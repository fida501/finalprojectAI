using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public Transform[] patrolPoints;   // Patrol points for the enemy to move between
    public float patrolSpeed = 2f;     // Speed while patrolling
    public float chaseSpeed = 4f;      // Speed while chasing the player
    public float attackCooldown = 0.5f; // Attack cooldown time
    public Animator animator;

    private Transform[] players;       // Array to store both players
    private Transform closestPlayer;   // Store the closest player
    private int currentPatrolIndex;
    private float waitTime = 2f;       // Time to wait at each patrol point
    private float waitTimer;
    private float attackTimer;         // Timer to handle attack cooldown
    private bool playerInRange = false;
    private bool playerInAttackRange = false;

    private State currentState;
    private Rigidbody rb;

    private enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    private void Start()
    {
        // Find both players in the scene
        // players = GameObject.FindGameObjectsWithTag("T5_Player");
        players = GameObject.FindGameObjectsWithTag("T5_Player").Select(player => player.transform).ToArray();
        
        rb = GetComponent<Rigidbody>(); // Get the 2D Rigidbody component
        currentState = State.Idle;
        waitTimer = waitTime;
        attackTimer = 0f;
        currentPatrolIndex = 0;
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime; // Update attack timer

        // Find the closest player if they are in range
        closestPlayer = GetClosestPlayer();

        // Check if the enemy should chase or attack
        if (closestPlayer != null)
        {
            if (playerInRange && !playerInAttackRange)
            {
                currentState = State.Chase;
            }
        }

        // Handle the enemy state machine
        switch (currentState)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Patrol:
                HandlePatrol();
                break;
            case State.Chase:
                HandleChase();
                break;
            case State.Attack:
                HandleAttack();
                break;
        }
    }

    private Transform GetClosestPlayer()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform player in players)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = player;
            }
        }

        return closest;
    }

    private void HandleIdle()
    {
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsAttack", false);

        waitTimer -= Time.deltaTime;

        if (waitTimer <= 0)
        {
            currentState = State.Patrol;
            waitTimer = waitTime;
        }
    }

    private void HandlePatrol()
    {
        animator.SetBool("IsWalk", true);
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        // Move the enemy towards the target patrol point using Rigidbody2D for 2D movement
        rb.velocity = new Vector2(direction.x * patrolSpeed, rb.velocity.y);

        // Flip the enemy sprite based on direction
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        // Check if the enemy has reached the patrol point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            currentState = State.Idle;
        }
    }

    private void HandleChase()
    {
        if (closestPlayer == null)
        {
            currentState = State.Patrol;
            return;
        }

        animator.SetBool("IsWalk", true);
        Vector3 direction = (closestPlayer.position - transform.position).normalized;

        // Move the enemy towards the closest player
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        // Flip the sprite based on direction
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        // Check if the enemy is within attack range
        if (playerInAttackRange)
        {
            currentState = State.Attack;
        }
    }

    private void HandleAttack()
    {
        if (!playerInAttackRange)
        {
            currentState = State.Chase;
            animator.SetBool("IsAttack", false);
            return;
        }

        if (attackTimer <= 0)
        {
            // Stop the enemy's movement by setting velocity to zero
            rb.velocity = Vector2.zero;

            // Trigger the attack animation and reset cooldown timer
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsAttack", true);

            attackTimer = attackCooldown;
            StartCoroutine(ResetAttackAnimation());
        }
    }

    private IEnumerator ResetAttackAnimation()
    {
        // Reset "Attacking" animation bool after a short delay
        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("IsAttack", false);
    }

    private void Flip()
    {
        // Flip the sprite horizontally
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("T5_Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("T5_Player"))
        {
            playerInRange = false;
            playerInAttackRange = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("T5_Player"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            distance = distance - 1.2f; // Adjust distance to center of enemy

            // Check for the closest player within attack range
            if (other.transform == closestPlayer)
            {
                playerInAttackRange = distance <= 1f; // Adjust attack range
            }
        }
    }
}
