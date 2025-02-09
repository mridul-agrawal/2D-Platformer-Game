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
        SoundManager.Instance.PlayBackgroundMusic(Sounds.BackgroundMusic1);
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
        if(Input.GetKeyDown(KeyCode.F))
        {
            playerAnimator.SetTrigger("meleeAttack");
            SoundManager.Instance.PlaySoundEffects(Sounds.PlayerStaffAttack);
        }


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
        SoundManager.Instance.PlaySoundEffects(Sounds.KeySounds);
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
            SoundManager.Instance.PlaySoundEffects(Sounds.PlayerDamage);
            transform.position = startPosition.position;
        }
    }

    public void PlayerDeath()
    {
        mainCamera.transform.parent = null;
        deathUIPanel.gameObject.SetActive(true);
        rigidbodyPlayer.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void PlayDeathAnimation()
    {
        if (isdead) return;
        playerAnimator.SetTrigger("Die");
        rigidbodyPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void HandleHealthUI()
    {
        for(int i=0; i<hearts.Length; i++)
        {
            hearts[i].sprite = i < health ? fullHeart : emptyHeart;
            hearts[i].enabled = i < maxHealth ? true : false;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void PlayMovementSound()
    {
        if(isTouchingGround) SoundManager.Instance.PlaySoundEffects(Sounds.PlayerMove);
    }
    public void PlayJumpSound()
    {
        SoundManager.Instance.PlaySoundEffects(Sounds.PlayerJump);
    }
    public void PlayLandSound()
    {
        if (isTouchingGround) SoundManager.Instance.PlaySoundEffects(Sounds.PlayerLand);
    }
    public void PlayDeathSound()
    {
        SoundManager.Instance.PlaySoundEffects(Sounds.PlayerDeath);
    }
}


