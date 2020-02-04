using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemyHandler : MonoBehaviour
{
    public Animator animator;
    public GameObject legs;
    public GameObject player;
    public float aggroRange = 10;
    public float speed = 5f;
    Vector3 idleLocation;

    void Start() {
        player = GameObject.Find("Player");
        idleLocation = transform.position;
    }

    void Update() {
        MoveToPlayer();
        AnimationHandler();
    }

    private void MoveToPlayer() {
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);
        if (playerDistance < aggroRange) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            animator.SetBool("IsRunning", true);
        } else if (playerDistance > aggroRange && transform.position != idleLocation) {
            transform.position = Vector2.MoveTowards(transform.position, idleLocation, (speed * Time.deltaTime) / 2);
            animator.SetBool("IsRunning", true);
        } else {
            animator.SetBool("IsRunning", false);
        }
    }

    private void AnimationHandler() {
        // if (transform.GetComponent<Rigidbody2D>().velocity != new Vector2(0f, 0f)) {
        //     animator.SetBool("IsRunning", true);
        // } else {
        //     animator.SetBool("IsRunning", false);
        // }

        if (animator.GetBool("IsRunning") == true) {
            legs.transform.GetChild(0).gameObject.SetActive(false);
            legs.transform.GetChild(1).gameObject.SetActive(true);
        } else if (animator.GetBool("IsRunning") == false) {
            legs.transform.GetChild(0).gameObject.SetActive(true);
            legs.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
