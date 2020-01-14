using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    public Animator animator;

    void Update()
    {
        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");

        movement.x = leftJoystick.Horizontal;
        movement.y = leftJoystick.Vertical;
        
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("Vertical", Mathf.Sign(leftJoystick.Vertical));
        SaveLastVertPos();
    }

    void FixedUpdate(){
            rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }

    void SaveLastVertPos() {
        if (leftJoystick.Vertical > .01) {
            animator.SetFloat("LastVertPos", 1);
        } else if (leftJoystick.Vertical < -.01) {
            animator.SetFloat("LastVertPos", -1);
        } else if (leftJoystick.Vertical == 0) {
            return;
        }
    }
}
