using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public float movementSpeed;
    public float jumpForce;
    public bool isTouchingGround;
    public ScoreController scoreController;
    public int health = 3;
    public int maxHealth = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private bool isdead = false;


    private Rigidbody2D rigidbodyPlayer;
    public Camera mainCamera;
    public CanvasRenderer deathUIPanel;

    private Vector2 standingColliderOffset = new Vector2(0f, 0.98f);
    private Vector2 standingColliderSize = new Vector2(0.45f, 2.05f);
    private Vector2 crouchingColliderOffset = new Vector2(-0.15f, 0.6f);
    private Vector2 crouchingColliderSize = new Vector2(0.75f,1.3f);

    public Transform startPosition;

    private void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    private void Update()
    {
        HandleHealthUI();
        if (isdead)
        {
            return;
        }
        HandleHorizontalInput();
        HandleVerticalInput();
        HandleOtherInput();
    }

    public void HandleHorizontalInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        PlayHorizontalAnimation(horizontal);
        MovePlayerHorizontally(horizontal);
    }

    public void PlayHorizontalAnimation(float horizontal)
    {
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1 * Mathf.Abs(scale.x);
            horizontal = Mathf.Abs(horizontal);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        transform.localScale = scale;
        playerAnimator.SetFloat("speed", horizontal);
    }

    public void MovePlayerHorizontally(float horizontal)
    {
        Vector3 position = transform.position;
        position.x = position.x + (horizontal * movementSpeed * Time.deltaTime);
        transform.position = position;
    }

    public void HandleVerticalInput()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        MovePlayerVertically();
    }

    public void MovePlayerVertically()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround && !playerAnimator.GetBool("Crouch"))
        {
            // Jump
            playerAnimator.SetTrigger("Jump");
            rigidbodyPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void HandleOtherInput()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            // Crouch
            playerAnimator.SetBool("Crouch",true);
            GetComponent<BoxCollider2D>().offset = crouchingColliderOffset;
            GetComponent<BoxCollider2D>().size = crouchingColliderSize;
        } else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            //Stand
            playerAnimator.SetBool("Crouch", false);
            GetComponent<BoxCollider2D>().offset = standingColliderOffset;
            GetComponent<BoxCollider2D>().size = standingColliderSize;
        }   
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.transform.tag == "platform")
        {
            isTouchingGround = true;
        } 
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "platform")
        {
            isTouchingGround = false;
        }
    }

    public void GetKey()
    {
        scoreController.AddScore(10);
    }

    public void DecreaseHealth()
    {
        health--;
        if(health <= 0)
        {
            PlayDeathAnimation();
            PlayerDeath();
            isdead = true;
        } else
        {
            transform.position = startPosition.position;
        }
    }

    public void PlayerDeath()
    {
        mainCamera.transform.parent = null;
        deathUIPanel.gameObject.SetActive(true);
        rigidbodyPlayer.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void PlayDeathAnimation()
    {
        playerAnimator.SetTrigger("Die");
        rigidbodyPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void HandleHealthUI()
    {
        for(int i=0; i<hearts.Length; i++)
        {
            if (i<health)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i<maxHealth)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

}
