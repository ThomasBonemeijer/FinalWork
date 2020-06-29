using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFloraHandler : MonoBehaviour
{
    GameObject player;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == player.name) {
            animator.SetBool("IsStandingOn", true);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.name == player.name) {
            animator.SetBool("IsStandingOn", false);
        }
    }
}
