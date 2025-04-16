using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    private PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JogarDeNovo()
    {
        SceneManager.LoadScene("PowerOfElements");

    }

    public void JogarDeNovo2() {
        SceneManager.LoadScene("PowerOfElements");
    }
}
