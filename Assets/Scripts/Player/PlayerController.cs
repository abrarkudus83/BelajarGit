using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private PlayerData playerData;

    private float currentHP;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    // public GameManager gameManager;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData != null)
        {
            currentHP = playerData.maxHP;
        }
        else
        {
            Debug.LogError("PlayerData belum di-assign!");
        }
    }

    void Update()
    {
        if (playerInput == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * playerData.moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(10f * Time.deltaTime); 
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameOver();
            // gameManager.GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("MainMenu");
        // currentState = GameState.GameOver;
        Time.timeScale = 1f; 
        Debug.Log("Player Mati");
        Time.timeScale = 0f;
        gameObject.SetActive(false);
    }
}
