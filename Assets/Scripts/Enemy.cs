using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float stopDistance = 1.5f;
    public float attackCooldown = 2f; // Tempo entre ataques
    private float attackTimer = 0f;

    public float currentHealth;
    public float maxHealth = 10f;

    private bool isDead = false;

    private NavMeshAgent agent;
    private Animator animator;
    public PlayerController playerController;

    // Efeito de fumaça
    public GameObject attackSmokeEffect;
    public Transform smokeSpawnPoint;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                playerController = playerObj.GetComponent<PlayerController>();
            }
        }
    }

    void Update()
    {

        float distance = Vector3.Distance(transform.position, player.position);
        if (player != null)
        {
            if (distance > stopDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();

                attackTimer += Time.deltaTime;

                if (attackTimer >= attackCooldown)
                {
                    Attack();
                    attackTimer = 0f;
                }
            }
        }

        if (distance >= stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            animator.SetTrigger("Death");
            Destroy(gameObject);
        }


    }

    void Attack()
    {
        if (playerController != null)
        {
            playerController.TakeDamage(1);
            playerController.Knockback(transform.position, 50f);
            animator.SetTrigger("Attack");
            Debug.Log("Inimigo atacou!");

            //Efeito de fumaça
            if (attackSmokeEffect != null)
            {
                Instantiate(attackSmokeEffect, smokeSpawnPoint != null ? smokeSpawnPoint.position : transform.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogWarning("playerController está nulo!");
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}