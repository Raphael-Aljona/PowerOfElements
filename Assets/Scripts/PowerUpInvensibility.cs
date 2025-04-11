using UnityEngine;

public class PowerUpInvensibility : MonoBehaviour
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
            other.GetComponent<PowerUpManager>().ActivateInvensibility();

            Destroy(gameObject);
        }
    }
}
