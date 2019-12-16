using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Joystick joystick;
    public Animator animator;

    void Update()
    {
        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");

        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate(){
            rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }
}
