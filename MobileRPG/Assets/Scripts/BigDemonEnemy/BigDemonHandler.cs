using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDemonHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject aggroCheckPoint;
    public GameObject theCastPoint;
    public GameObject theSpell;
    public float maxHealth = 125f;
    public float health;
    
    public float aggroRange = 10f;
    public Animator animator;
    bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
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
            Instantiate(theSpell, theCastPoint.transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Bullet")) {
            Debug.Log("BulletHit!");
            Destroy(col.gameObject);
            health -= 25;
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
