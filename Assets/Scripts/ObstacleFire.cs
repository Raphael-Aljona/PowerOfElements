using UnityEngine;
using System.Collections;

public class FireObstacle : MonoBehaviour
{
    public float initialDamage = 1f;
    public float delayedDamage = 0.5f;
    public float delayTime = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(initialDamage);
                Debug.Log("Dano inicial aplicado");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                StartCoroutine(DelayedBurn(player));
                Debug.Log("Dano contínuo começando...");
            }
        }
    }

    private IEnumerator DelayedBurn(PlayerController player)
    {
        yield return new WaitForSeconds(delayTime);

        if (player != null)
        {
            player.TakeDamage(delayedDamage);
            Debug.Log("Dano contínuo aplicado");
        }
    }
}