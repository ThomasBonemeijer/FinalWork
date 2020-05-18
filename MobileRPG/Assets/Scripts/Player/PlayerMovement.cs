using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    Joystick leftJoystick;
    Joystick rightJoystick;
    public Animator animator;

    void Start() {
        leftJoystick = GameObject.Find("UI").GetComponent<UIHandler>().leftJoyStick;
        rightJoystick = GameObject.Find("UI").GetComponent<UIHandler>().rightJoyStick;
    }
    void Update()
    {
        movement.x = leftJoystick.Horizontal;
        movement.y = leftJoystick.Vertical;

        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if (rightJoystick.Vertical == 0) {
            animator.SetFloat("Vertical", Mathf.Sign(leftJoystick.Vertical));
            animator.SetFloat("StaticVertical", leftJoystick.Vertical);
            SaveLastVertPos();
        } else {
            animator.SetFloat("Vertical", Mathf.Sign(rightJoystick.Vertical));
            animator.SetFloat("StaticVertical", rightJoystick.Vertical);
            SaveLastVertPos();
        }
    }

    void FixedUpdate() {
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

    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Checkpoint") {
            Debug.Log("Checkpoint reached");
            GetComponent<PlayerHandler>().SavePlayer();
            Destroy(col.gameObject);
        }
    }
}
