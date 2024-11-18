using UnityEngine;

public class Demon : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;


    public Transform player;

    public float speedMove;
    public int health;

    public float rangeAttack;

    public bool isAttack, isDeath;
    public BoxCollider2D hitBox;

    public int damage;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDeath == false)
        {
            Attack();
        }
    }
    void Attack()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, player.position) > rangeAttack)
        {
            if (isAttack == false)
            {
                //Move
                anim.SetBool("aatack", false);
                rb.linearVelocity = direction * speedMove;

                if(transform.position.x > player.position.x)
                {
                    transform.localScale = new Vector3(6, 6, 6);
                }
                if (transform.position.x < player.position.x)
                {
                    transform.localScale = new Vector3(-6, 6, 6);
                }
            }
        }
        else
        {
            //Attack
            rb.linearVelocity = Vector2.zero;
            isAttack = true;
            anim.SetBool("aatack", true);
        }
    }
    public void EventAttack()
    {
        isAttack = false;
    }
    public void TrueHit()
    {
        hitBox.enabled = true;
    }
    public void FalseHit()
    {
        hitBox.enabled = false;
    }
    public void Damage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                isDeath = false;
                anim.SetTrigger("death");

                StartCoroutine(FindObjectOfType<PlayerController>().LoadGame());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Death(damage);
        }
    }
}
