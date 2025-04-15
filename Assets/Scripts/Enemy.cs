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
    public PlayerController playerController;

    // Efeito de fumaça
    public GameObject attackSmokeEffect;
    public Transform smokeSpawnPoint;
    void Start()
    {
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
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

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

        if (currentHealth <= 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        if (playerController != null)
        {
            playerController.TakeDamage(1);
            playerController.Knockback(transform.position, 50f);
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