using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public Weapon weapon;

    private enum State
    {
        Normal,
        Dashing
    }

    private State state;
    Vector2 moveDirection;
    Vector2 mousePosition;
    private float dashSpeed;
    private Vector2 dashDirection;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Normal:
                float moveX = Input.GetAxisRaw("Horizontal");
                float moveY = Input.GetAxisRaw("Vertical");

                
                if(Input.GetMouseButtonDown(0))
                {
                    weapon.ShootFireball();
                }
                if(Input.GetMouseButtonDown(1))
                {
                    weapon.ShootBlueFireball();
                }

                moveDirection = new Vector2(moveX, moveY).normalized;
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


                //Dash
                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    dashDirection = moveDirection;
                    dashSpeed = 80f;
                    state = State.Dashing;
                }
                break;

            case State.Dashing:
                float dashSpeedDropMultiplier = 5f;
                dashSpeed -= dashSpeed * dashSpeedDropMultiplier * Time.deltaTime;

                float dashSpeedMinimum = 50f;
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

                Vector2 aimDirection = mousePosition - rb.position;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = aimAngle;
                break;
            case State.Dashing:
                rb.velocity = dashDirection * dashSpeed;
                break;
        }
    }
}
