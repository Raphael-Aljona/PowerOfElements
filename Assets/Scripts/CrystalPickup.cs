using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    private UIManager UIManager;

    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (UIManager != null)
            {
                UIManager.AddCrystal();
            }

            Destroy(gameObject); // Some da cena
        }
    }
}