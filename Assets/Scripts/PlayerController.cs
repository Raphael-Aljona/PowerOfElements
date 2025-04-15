using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Publicos

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

    public GameObject fireball;
    public Transform fireTarget;
    public float fireBallRate = 1f;
    private float fireTimer;

    // Privados 

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private float originalSpeed;
    private float originalJumpForce;

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

        // Ataque do Player -> Bola de Fogo

        //Define o tempo inicial até o player puder atirar
        fireTimer += Time.deltaTime;

        // Se botao esquerdo foi apertado e o fireTimer é maior que o fireballrate
        if (Input.GetMouseButtonDown(0) && fireTimer >= fireBallRate)
        {
            Fire();

            //Reseta o tempo
            fireTimer = 0f;
        }

    }

    public void TakeDamage(float damage)
    {
        if (!isDead && !invensibility)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            AtualizeHealthBar();
        }
       

    }

    public void AtualizeHealthBar()
    {
        ScrollHealthBar.size = currentHealth / maxHealth;
    }

    void Fire()
    {
        Instantiate(fireball, fireTarget.position, fireTarget.rotation);
    }

    // Adiciona um knockback ao player
    public void Knockback(Vector3 sourcePosition, float force)
    {
        StopCoroutine("ApplyKnockback"); // caso esteja levando outro empurrão
        StartCoroutine(ApplyKnockback(sourcePosition, force, 0.2f)); // 0.2 segundos de empurrão
    }

    IEnumerator ApplyKnockback(Vector3 sourcePosition, float force, float duration)
    {
        Vector3 direction = (transform.position - sourcePosition).normalized;
        direction.y = 0;

        float timer = 0f;

        while (timer < duration)
        {
            Vector3 knockback = direction * force;
            controller.Move(knockback * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }
    }
}

