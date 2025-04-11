using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private PlayerController playerController;
    private PowerUpPulo powerUpJump;
    private PowerUpSpeed powerUpSpeed;
    private PowerUpInvensibility powerUpInvensibility;

    private int powerUpDuration = 30;
    private int invenbilityDuration = 10;

    public float jumpForceUpgrade;
    public float jumpForceNormal;

    public float speedUpgrade;
    public float speedNormal;

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
        StartCoroutine(PowerUpJumpRoutine());
    }

    private IEnumerator PowerUpJumpRoutine()
    {
        Debug.Log("Power Up Pulo Ativado");

        playerController.jumpForce = jumpForceUpgrade;

        yield return new WaitForSeconds(powerUpDuration);

        playerController.jumpForce = jumpForceNormal;

        Debug.Log("CABO O KANGURU");
    }

    public void ActivateSpeed()
    {
        StartCoroutine(PowerUpSpeedRoutine());
    }

    private IEnumerator PowerUpSpeedRoutine()
    {
        Debug.Log("Power Up Speed Pego");

        playerController.speed = speedUpgrade;

        yield return new WaitForSeconds(powerUpDuration);

        playerController.speed = speedNormal;

        Debug.Log("CABO FLASH");
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
