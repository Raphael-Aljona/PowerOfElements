using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PowerUpManager>().ActivateSpeed();

            Destroy(gameObject);
        }
    }
}
