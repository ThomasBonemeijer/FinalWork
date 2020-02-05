using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovementHandler : MonoBehaviour
{
    public Animator animator;
    public GameObject legs;
    public GameObject target;
    public GameObject mageSpell;
    public Transform attackPoint;
    public string demonType = "mage";
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float aggroRange = 10;
    public float attackRange = 4;
    public bool targetInRange = false;
    public bool targetInAttackRange = false;
    bool attack = false;
    Vector3 idleLocation;
    public bool isAtIdleLocation = true;
    Path path;
    int currentWaypoint = 0;
    // bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        idleLocation = rb.position;

        InvokeRepeating("UpdatePath", 0, .5f);
        InvokeRepeating("Attack", 0, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimationHandler();
        CheckIfInRange();
        PathHandler();
    }

    void OnPathComplete(Path p) {
        if(!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath() {
        if (seeker.IsDone() && targetInRange == true) {
            if (targetInAttackRange == false) {
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
                attack = false;
            } else {
                seeker.StartPath(rb.position, rb.position, OnPathComplete);
                seeker.IsDone();
                rb.velocity = new Vector2(0, 0);
                attack = true;
            }
        } else if (seeker.IsDone() && targetInRange == false) {
            if (isAtIdleLocation == false) {
                seeker.StartPath(rb.position, idleLocation, OnPathComplete);
            } else {
                return;
            }
        }
    }

    void CheckIfInRange() {
        float targetDist = Vector2.Distance(transform.position, target.transform.position);

        if (targetDist <= aggroRange) {
            targetInRange = true;
            if (targetDist <= attackRange) {
                targetInAttackRange = true;
            } else {
                targetInAttackRange = false;
            }
        } else {
            targetInRange = false;
        }

        if (Vector2.Distance(rb.position, idleLocation) < 1f) {
            isAtIdleLocation = true;
        } else if (rb.position != (Vector2)idleLocation) {
            isAtIdleLocation = false;
        }
    }

    void PathHandler() {
        if (path == null) {
            return;
        } else {
            if (currentWaypoint >= path.vectorPath.Count) {
                // reachedEndOfPath = true;
                return;
            } else {
                // reachedEndOfPath = false;
            }
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }

     void AnimationHandler() {

        if (animator.GetBool("IsRunning") == true) {
            legs.transform.GetChild(0).gameObject.SetActive(false);
            legs.transform.GetChild(1).gameObject.SetActive(true);
        } else if (animator.GetBool("IsRunning") == false) {
            legs.transform.GetChild(0).gameObject.SetActive(true);
            legs.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (rb.velocity != new Vector2(0f, 0f)) {
            animator.SetBool("IsRunning", true);
        } else {
            animator.SetBool("IsRunning", false);
        }
    }

    void Attack() {
        if (attack == true) {
            if (demonType == "mage") {
                animator.SetBool("IsMelee", false);
                animator.SetBool("IsMage", true);
                animator.SetBool("IsAttacking", true);
                Instantiate(mageSpell, attackPoint.position, attackPoint.rotation);
            } else if (demonType == "melee") {
                animator.SetBool("IsMage", false);
                animator.SetBool("IsMelee", true);
            }
        } else {
            animator.SetBool("IsAttacking", false);
        }
    }
}
