using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public float movementSpeed;
    public float jumpForce;
    public bool isTouchingGround;

    private Rigidbody2D rigidbodyPlayer;

    private Vector2 standingColliderOffset = new Vector2(0f, 0.98f);
    private Vector2 standingColliderSize = new Vector2(0.45f, 2.05f);
    private Vector2 crouchingColliderOffset = new Vector2(-0.15f, 0.6f);
    private Vector2 crouchingColliderSize = new Vector2(0.75f,1.3f);

    private void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
        if (horizontal < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1 * Mathf.Abs(scale.x);
            transform.localScale = scale;
            horizontal = Mathf.Abs(horizontal);
        }
        else if (horizontal > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
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
        }  else if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
        {
            // Jump
            playerAnimator.SetTrigger("Jump");
            rigidbodyPlayer.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
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

}
