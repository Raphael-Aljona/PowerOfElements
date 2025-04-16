using UnityEngine;

public class HealthRecovery : MonoBehaviour
{
    public float HealAmount = 1f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            Debug.Log("É o player!");

            if (player.currentHealth < player.maxHealth)
            {
                player.currentHealth += HealAmount;
                player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);
                player.AtualizeHealthBar();

                Destroy(gameObject);
            }
        }
        
    }
}
