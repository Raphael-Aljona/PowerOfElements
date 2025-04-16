using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private PlayerController playerController;
    private PowerUpPulo powerUpJump;
    private PowerUpSpeed powerUpSpeed;
    private PowerUpInvensibility powerUpInvensibility;

    private int invenbilityDuration = 10;

    public float jumpForceUpgrade;

    public float speedUpgrade;

    public bool invensibilityUpgrade;
    public bool invenbilityNormal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        powerUpJump = GetComponent<PowerUpPulo>();
        playerController = GetComponent<PlayerController>();
        powerUpSpeed = GetComponent<PowerUpSpeed>();
        powerUpInvensibility = GetComponent<PowerUpInvensibility>();
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void ActivateJump()
    {
        Debug.Log("Power Up Pulo Ativado");

        playerController.jumpForce = jumpForceUpgrade;
    }
    public void ActivateSpeed()
    {
        Debug.Log("Power Up Speed Pego");

        playerController.speed = speedUpgrade;
    }

    public void ActivateInvensibility()
    {
        StartCoroutine(PowerUpInvensibilityRoutine());
    }

    private IEnumerator PowerUpInvensibilityRoutine()
    {
        Debug.Log("Power Up Speed Pego");

        playerController.invensibility = invensibilityUpgrade;

        yield return new WaitForSeconds(invenbilityDuration);

        playerController.invensibility = invenbilityNormal;

        Debug.Log("ACABO");
    }

}
