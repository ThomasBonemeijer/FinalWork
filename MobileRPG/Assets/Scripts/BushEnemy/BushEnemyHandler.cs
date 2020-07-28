using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BushEnemyHandler : MonoBehaviour
{
    public GameObject damageCanvas;
    public GameObject infoPoint;
    public float maxHealth = 50f;
    public float health;
    GameObject player;
    float playerDistance;
    float closeAggroRange = 4f;
    float farAggroRange = 15f;
    public bool hasBeenForceWokenUp = false;
    public bool isAttacking = false;
    public Animator animator;
    Vector3 defaultPos;
    float defaultPosRange = 2f;
    float defaultPosDistance;
    public float absoluteXScale;
    public List<GameObject> attackPoints;
    public List<GameObject> thorns;
    public List<GameObject> dropItems;

    float xScale;
    public string currentRange;
    Seeker seeker;
    Rigidbody2D rb;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    Path path;
    bool reachedEndOfPath = false;
    int currentWayPoint = 0;
    public ParticleSystem deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        absoluteXScale = Mathf.Abs(transform.localScale.x);
        health = maxHealth;
        xScale = transform.localScale.x;
        defaultPos = transform.position;
        player = GameObject.Find("Player");

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("DrawPath", 0, .5f);
        InvokeRepeating("Attack", 0, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        defaultPosDistance = Vector2.Distance(transform.position, defaultPos);
        HandleState();
        MoveOnPath();
    }

    void HandleState() {
        if (hasBeenForceWokenUp == false) {
            // if (isAttacking == false) {
                if(playerDistance < farAggroRange) {
                    currentRange = "far";
                    if (animator.GetBool("IsAwake") == false) {
                        if (playerDistance < closeAggroRange) {
                            currentRange = "close";
                            animator.SetBool("IsAwake", true);
                            isAttacking = true;
                        }
                    } else {
                        SetOrientation(player.transform.position);
                        if (playerDistance < closeAggroRange) {
                            currentRange = "close";
                            animator.SetBool("IsMoving", false);
                            isAttacking = true;
                        } else {
                            StopAllCoroutines();
                            animator.SetBool("IsMoving", true);
                        }
                    }

                } else {
                    SetOrientation(defaultPos);
                    currentRange = "out";
                    if (defaultPosDistance < defaultPosRange) {
                        animator.SetBool("IsMoving", false);
                        StartCoroutine(FallAsleep(5));
                    } else {
                        animator.SetBool("IsMoving", true);
                    }
                // }
            }
        } else {
            currentRange = "far";
            SetOrientation(player.transform.position);
            animator.SetBool("IsAwake", true);
            StartCoroutine(DelayedMove(.5f));
        }
    }

    IEnumerator DelayedMove(float time)
 {
     yield return new WaitForSeconds(time);
 
     animator.SetBool("IsMoving", true);
 }

    IEnumerator FallAsleep(float time)
 {
     yield return new WaitForSeconds(time);
 
     animator.SetBool("IsAwake", false);
 }

    void SetOrientation(Vector3 target) {
            if (animator.GetBool("IsAwake") == true) {
                if(target.x > transform.position.x) {
                    transform.localScale = new Vector3(-absoluteXScale, Mathf.Abs(-xScale), 1);
                } else {
                    transform.localScale = new Vector3(absoluteXScale, Mathf.Abs(xScale), 1);
                }
            }
    }

    void DrawPath() {
        if(animator.GetBool("IsAwake") == true) {
            if (currentRange == "out") {
                seeker.StartPath(rb.position, defaultPos, OnPathComplete);
            } else if (currentRange == "far") {
                seeker.StartPath(rb.position, player.transform.position, OnPathComplete);
            } else if (currentRange == "close") {

            }
        }
    }

    void MoveOnPath() {
        if (path == null) {
            return;
        } if (currentWayPoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (animator.GetBool("IsMoving") == true) {
            rb.AddForce(force);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDistance) {
            currentWayPoint++;
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWayPoint = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Bullet")) {
            if (animator.GetBool("IsAwake") == false && hasBeenForceWokenUp == false) {
                ForceWakeUp();
            }
            TakeDamage(25, col.gameObject, true);
        } else if (col.CompareTag("Knife")) {
            if (animator.GetBool("IsAwake") == false && hasBeenForceWokenUp == false) {
                ForceWakeUp();
            }
            TakeDamage(25, col.gameObject, false);
        }
    }

    private void TakeDamage(int damage, GameObject theCol, bool isBullet) {
        // Debug.Log("BulletHit!");
        var instantiatedDamageCanvas = Instantiate(damageCanvas, infoPoint.transform.position, Quaternion.identity);
        instantiatedDamageCanvas.GetComponent<DamageInfoCanvas>().ShowDamageNumbers(damage);

        if (isBullet == true) {
            Destroy(theCol);
        }
        health -= damage;
        if (health <= 0) {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Instantiate (dropItems[Random.Range(0, dropItems.Count)], transform.position, Quaternion.identity);
        }
    }

    void ForceWakeUp() {
        StopAllCoroutines();
        hasBeenForceWokenUp = true;
        StartCoroutine(ForceWakeUpFollowUp(5));
    }

    IEnumerator ForceWakeUpFollowUp(float time)
    {
        yield return new WaitForSeconds(time);
    
        hasBeenForceWokenUp = false;
    }

    public void ResetEnemyStats() {
        health = maxHealth;
    }

    void SetAttackPoints() {
        attackPoints[0].transform.rotation = Quaternion.Euler(1, 1, 45);
        attackPoints[1].transform.rotation = Quaternion.Euler(1, 1, 135);
    }

    void Attack() {
        SetAttackPoints();
        if (isAttacking == true && animator.GetBool("IsMoving") == false) {
            // Debug.Log("Attack!");
            animator.SetTrigger("Attack");
            foreach(GameObject attackPoint in attackPoints) {
                Instantiate(thorns[0], attackPoint.transform.position, attackPoint.transform.rotation);
            }
            isAttacking = false;
        }
    }
}
