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
    public bool isDead = false;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (attacking) return;
        if (isDead) return;

        transform.Translate(movingRight * Vector2.right * speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(groundDetector.transform.position, Vector2.down, rayDistance);
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
            SoundManager.Instance.PlaySoundEffects(Sounds.EnemyDeath);
            GetComponentInChildren<ParticleSystem>().Play();
            isDead = true;
            enemyAnimator.SetTrigger("die");
            Destroy(gameObject,1);
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
