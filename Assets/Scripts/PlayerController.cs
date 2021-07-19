using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;

    private void Update()
    {
        HandleHorizontalInput();
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

}
