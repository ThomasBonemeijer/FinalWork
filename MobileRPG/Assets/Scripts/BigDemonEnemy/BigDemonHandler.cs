using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDemonHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject aggroCheckPoint;
    
    public float aggroRange = 10f;
    public Animator animator;
    bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        InvokeRepeating("Attack", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDitance();
    }

    void CheckDitance () {
        float distance = Vector2.Distance(aggroCheckPoint.transform.position, player.transform.position);
        if (distance <= aggroRange) {
            animator.SetBool("IsAttacking", true);
            isAttacking = true;
        } else {
            animator.SetBool("IsAttacking", false);
            isAttacking = false;
        }
    }

    void Attack() {
        if (isAttacking == true) {
            // Debug.Log("Big demon attack!");
        }
    }
}
