using UnityEngine;

public class BlockedWay : MonoBehaviour
{
    public bool open = false;
    public bool liberado = false;

    public GameObject blockedWayText;

    public PlayerController controller;
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !open)
        {
            if (liberado)
            {
                Destroy(gameObject);
            }
            else
            {
                blockedWayText.SetActive(true);    
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        blockedWayText.SetActive(false);
    }

    public void LiberarCaminho()
    {
        liberado = true;
    }

}
