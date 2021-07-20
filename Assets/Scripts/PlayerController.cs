using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;

    private Vector2 standingColliderOffset = new Vector2(0f, 0.98f);
    private Vector2 standingColliderSize = new Vector2(0.45f, 2.05f);
    private Vector2 crouchingColliderOffset = new Vector2(-0.15f, 0.6f);
    private Vector2 crouchingColliderSize = new Vector2(0.75f,1.3f);

    private void Update()
    {
        HandleHorizontalInput();
        HandleVerticalInput();
        HandleOtherInput();
    }

    public void HandleHorizontalInput()
    {
        float speed = Input.GetAxisRaw("Horizontal");

        if (speed < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1 * Mathf.Abs(scale.x);
            transform.localScale = scale;

            speed = Mathf.Abs(speed);
        }
        else if (speed > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        playerAnimator.SetFloat("speed", speed);
    }

    public void HandleVerticalInput()
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
        } else if(Input.GetKeyDown(KeyCode.Space))
        {
            // Jump
            playerAnimator.SetTrigger("Jump");
        }
    }

}
