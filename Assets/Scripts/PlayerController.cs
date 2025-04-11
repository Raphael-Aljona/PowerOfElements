using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Publicos & Váriaveis

    public float speed = 5f;
    public float sensitivity = 2f;

    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.4f;

    public bool invensibility = false;

    public float maxHealth = 3;
    public float currentHealth;
    public Scrollbar ScrollHealthBar;
    public bool isDead = false;

    //public GameObject SpeedEffect;
    //public GameObject JumpEffect;
    //public GameObject HealthEffect;

    // Privados & Váriaveis

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    //private int SpeedLayer = 9;
    //private int HealthLayer = 10;
    //private int JumpLayer = 11;

    private float originalSpeed;
    private float originalJumpForce;

    //private Coroutine speedRoutine;
    //private Coroutine jumpRoutine;

    private CharacterController controller;
    private Transform myCamera;
    private float yVelocity;
    private float rotationX = 0f;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalSpeed = speed;
        originalJumpForce = jumpForce;

        // Vida Player
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (transform.position.y <= -5)
        {
            SceneManager.LoadScene("DeathScene");
        }

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        myCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 inputDir = (right * horizontal + forward * vertical).normalized;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded)
        {
            if (inputDir.magnitude > 0.1f)
                moveDirection = inputDir;
            else
                moveDirection = Vector3.zero;

            if (yVelocity < 0)
                yVelocity = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
                if (moveDirection == Vector3.zero)
                    moveDirection = Vector3.zero;
            }
        }
        else
        {
            if (inputDir.magnitude > 0.1f)
                moveDirection = inputDir;
        }

        yVelocity += gravity * Time.deltaTime;

        Vector3 moveXZ = moveDirection * speed;
        Vector3 finalMove = new Vector3(moveXZ.x, yVelocity, moveXZ.z);
        controller.Move(finalMove * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }

       
        if (isDead)
        {
            SceneManager.LoadScene("DeathScene");

            Debug.Log("MORREU");
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        AtualizeHealthBar();

    }

    public void AtualizeHealthBar()
    {
        ScrollHealthBar.size = currentHealth / maxHealth;
    }
}

