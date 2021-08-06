using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public int movingRight = 1;
    public GameObject groundDetector;
    public float rayDistance;
    private Animator enemyAnimator;
    public bool attacking;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (attacking) return;

        transform.Translate(movingRight * Vector2.right * speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(groundDetector.transform.position, Vector2.down, rayDistance);
    //    Debug.Log(hit.transform.name);
        if(!hit)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            movingRight = movingRight * -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<PlayerController>() != null)
        {
            collision.transform.GetComponent<PlayerController>().DecreaseHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Staff")
        {
            //trigger Particle Effect.
            SoundManager.Instance.PlaySoundEffects(Sounds.EnemyDeath);
            Destroy(gameObject);
        }
        if (collision.transform.GetComponent<PlayerController>() != null)
        {
            enemyAnimator.SetTrigger("attack");
            SoundManager.Instance.PlaySoundEffects(Sounds.ChomperAttack);
            attacking = true;
        }
    }

    public void SetAttacking()
    {
        attacking = false;
    }

}
