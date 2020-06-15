using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BushEnemyHandler : MonoBehaviour
{
    GameObject player;
    float playerDistance;
    float closeAggroRange = 8f;
    float farAggroRange = 15f;
    public Animator animator;
    Vector3 defaultPos;
    float xScale;
    Seeker seeker;
    Rigidbody2D rb;
    public float nextWaypointDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        xScale = transform.localScale.x;
        defaultPos = transform.position;
        player = GameObject.Find("Player");
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        HandleMovement();
    }

    void HandleMovement() {
        if(playerDistance < farAggroRange) {
            if (animator.GetBool("IsAwake") == false) {
                if (playerDistance < closeAggroRange) {
                    animator.SetBool("IsAwake", true);
                }
            } else {
                if (playerDistance < closeAggroRange) {
                    animator.SetBool("IsMoving", false);
                } else {
                    StopAllCoroutines();
                    animator.SetBool("IsMoving", true);
                    SetOrientation();
                }
            }

        } else {
            Debug.Log("Player is out of aggrorange");
            if (transform.position == defaultPos) {
                animator.SetBool("IsMoving", false);
                StartCoroutine(FallAsleep(5));
            } else {
                animator.SetBool("IsMoving", true);
            }
        }
    }

    IEnumerator FallAsleep(float time)
 {
     yield return new WaitForSeconds(time);
 
     animator.SetBool("IsAwake", false);
 }

    void SetOrientation() {
        if(player.transform.position.x > transform.position.x) {
            transform.localScale = new Vector3(-xScale, xScale, 1);
        } else {
            transform.localScale = new Vector3(xScale, xScale, 1);
        }
    }
}
