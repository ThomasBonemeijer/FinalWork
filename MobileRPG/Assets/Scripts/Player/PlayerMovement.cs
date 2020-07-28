using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isDead;
    public float movespeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    Joystick leftJoystick;
    Joystick rightJoystick;
    public Animator animator;
    public ParticleSystem pfx;

    void Start() {
        leftJoystick = GameObject.Find("UI").GetComponent<UIHandler>().leftJoyStick;
        rightJoystick = GameObject.Find("UI").GetComponent<UIHandler>().rightJoyStick;
    }
    void Update()
    {
        isDead = GetComponent<PlayerHandler>().playerIsDead;
        if (isDead != true) {
            animator.SetBool("IsDead", false);
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
        float angle = Mathf.Atan2(leftJoystick.Direction.x, leftJoystick.Direction.y) * Mathf.Rad2Deg;
        Vector3 newAngle = new Vector3(0, 0, angle);
        pfx.transform.rotation = Quaternion.Euler(0, 0, -angle - 180f);

        if (animator.GetFloat("Speed") != 0) {
            pfx.gameObject.SetActive(true);
        } else {
            pfx.gameObject.SetActive(false);
        }
    }

    void FixedUpdate() {
        if(isDead != true) {
            rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
        }
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
            // Debug.Log("Checkpoint reached");
            GetComponent<PlayerHandler>().SavePlayer();
            Destroy(col.gameObject);
        }
    }

    public void PlayerDieAnimation() {
        animator.SetBool("IsDead", true);
    }
}
