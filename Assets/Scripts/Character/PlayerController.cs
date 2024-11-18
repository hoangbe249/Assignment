using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    //move
    public float speedMove;
    private Vector2 directionMove;

    //move joystick direction button
    public Joystick _joystick;

    //animation
    private enum State { idle,run,attack1, attack2, death}
    private State state = State.idle;

    //attack
    public bool isAttack;
    public int damage;

    //damage
    public BoxCollider2D rangeAt1;
    public BoxCollider2D rangeAt2;

    public int heath;

    public bool isDeath;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        Application.targetFrameRate = 120;
    }
    private void Update()
    {
        if (!isDeath)
        {
            Move();
            ChangeAnimation();
        }
        anim.SetInteger("state", (int)state);

    }
    void Move()
    {
        if (isAttack == false)
        {
            float moveX = _joystick.Direction.x;
            float moveY = _joystick.Direction.y;

            directionMove = new Vector2(moveX, moveY);
            rb.linearVelocity = directionMove * speedMove;

            if (moveX > 0)
            {
                //move to right
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (moveX < 0)
            {
                //move to left
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    void ChangeAnimation()
    {
        if (isAttack == false)
        {
            if (directionMove != Vector2.zero)
            {
                state = State.run;
            }
            else
            {
                state = State.idle;
            }
        }
    }
    public void SkillAttack1()
    {
        isAttack = true;
        state = State.attack1;
    }
    public void TrueAttack1()
    {
        rangeAt1.enabled = true;
    }
    public void FalseAttack1()
    {
        rangeAt1.enabled = false;
    }
    public void TrueAttack2()
    {
        rangeAt2.enabled = true;
    }
    public void FalseAttack2()
    {
        rangeAt2.enabled = false;
    }


    public void SkillAttack2()
    {
        isAttack = true;
        state = State.attack2;
    }
    public void EventAttack()
    {
        isAttack = false;
        state = State.idle;
    }

    public void Death(int damage)
    {
        if(heath > 0)
        {
            heath -= damage;

            if(heath <= 0)
            {
                isDeath = true;
                state = State.death;

                StartCoroutine(LoadGame());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("demon"))
        {
            collision.GetComponent<Demon>().Damage(damage);
        }
    }

    public IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
