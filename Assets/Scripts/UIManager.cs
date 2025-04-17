using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;

    private PlayerController playerController;

    public int totalCrystals = 0;
    public TextMeshProUGUI crystalText; // Use Text se for UI normal
    public BlockedWay blockedWay;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        playerHealthBar.maxValue = playerController.maxHealth;

        playerHealthBar.value = playerHealthBar.maxValue;

        UpdateCrystalUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalCrystals >= 5)
        {
            SceneManager.LoadScene("Victory");

            Debug.Log("ganhou!!");
        }

        if (totalCrystals == 4)
        {
            blockedWay.LiberarCaminho();
        }
    }
    
    public void UpdatePlayerHealthBar(int amount)
    {
        playerHealthBar.value = amount;
    }

    public void AddCrystal()
    {
        totalCrystals++;
        UpdateCrystalUI();
    }
    void UpdateCrystalUI()
    {
        crystalText.text = "" + totalCrystals;
    }

}
