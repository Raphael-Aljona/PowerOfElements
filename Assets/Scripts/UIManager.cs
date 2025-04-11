using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        playerHealthBar.maxValue = playerController.maxHealth;

        playerHealthBar.value = playerHealthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdatePlayerHealthBar(int amount)
    {
        playerHealthBar.value = amount;
    }

}
