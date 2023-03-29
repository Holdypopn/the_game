using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public ParticleSystem dust;
    private WeaponController wC;

    private enum State
    {
        Normal,
        Dashing
    }

    private State state;
    Vector2 moveDirection;
    private float dashSpeed;
    private Vector2 dashDirection;
    private Animator anim;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;
        anim = GetComponent<Animator>();
        wC = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Normal:
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Wall"), LayerMask.NameToLayer("EnemyLayer"), false);

                float moveX = Input.GetAxisRaw("Horizontal");
                float moveY = Input.GetAxisRaw("Vertical");
                moveDirection = new Vector2(moveX, moveY).normalized;

                if(moveX > 0 && !facingRight)
                    Flip();
                else if(moveX < 0 && facingRight)
                    Flip();

                if(moveX != 0 || moveY != 0)
                    anim.SetTrigger("onRunning");
                else
                    anim.SetTrigger("idle");                


                if(Input.GetMouseButtonDown(0))
                {
                    wC.attackPrimary();
                }
                if(Input.GetMouseButtonDown(1))
                {
                    wC.attackSecondary();
                }

                
                //Dash
                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    anim.SetTrigger("onDash");
                    dashDirection = moveDirection;
                    dashSpeed = 50f;
                    state = State.Dashing;
                }
                break;

            case State.Dashing:
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Wall"), LayerMask.NameToLayer("EnemyLayer"), true);
                float dashSpeedDropMultiplier = 4f;
                dashSpeed -= dashSpeed * dashSpeedDropMultiplier * Time.deltaTime;

                float dashSpeedMinimum = 20f;
                if(dashSpeed < dashSpeedMinimum)
                {
                    state = State.Normal;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch(state)
        {
            case State.Normal:
                rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
                break;
            case State.Dashing:
                rb.velocity = dashDirection * dashSpeed;
                break;
        }
    }

    void Flip()
    {
        CreateDust();
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    void CreateDust()
    {
        dust.Play();
    }
}
