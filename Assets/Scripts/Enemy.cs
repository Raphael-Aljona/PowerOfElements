using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float stopDistance = 1.5f;
    public float attackCooldown = 2f;
    private float attackTimer = 0f;

    public float currentHealth;
    public float maxHealth = 10f;

    private bool isDead = false;

    private NavMeshAgent agent;
    private Animator animator;
    public PlayerController playerController;

    public GameObject attackSmokeEffect;
    public Transform smokeSpawnPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerController = playerObj.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("Walking", true);
        }
        else
        {
            agent.ResetPath();
            animator.SetBool("Walking", false);

            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                Attack();
                attackTimer = 0f;
            }
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Attack()
    {
        if (playerController != null)
        {
            playerController.TakeDamage(1);
            playerController.Knockback(transform.position, 50f);
            animator.SetTrigger("Attack");

            if (attackSmokeEffect != null)
            {
                Instantiate(attackSmokeEffect, smokeSpawnPoint != null ? smokeSpawnPoint.position : transform.position, Quaternion.identity);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        agent.isStopped = true;
        GetComponent<Collider>().enabled = false; // evita colisões pós-morte
        Destroy(gameObject, 2f); // tempo para a animação tocar
    }
}
