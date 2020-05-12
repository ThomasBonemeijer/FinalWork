using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SmallSlug : MonoBehaviour
{
    Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public float blastrange = 2f;
    bool isInBlastrange = false;
    bool hasBeenTriggered = false;
    bool hasBeenHit = false;
    Animator animator;

    Seeker seeker;
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player").transform;
        seeker = GetComponent<Seeker>();
        rb2D = GetComponent<Rigidbody2D>();

        // check if there is a player, if so create a path
        if (target != null) {
            InvokeRepeating("UpdatePath", 0f, .5f);
        } else {
            Debug.LogError("There is no player");
        }
    }

    // Check if the player is in blast range and if not, create a path to the player and move along that path (path updates every .5 seconds)
    void UpdatePath() {
        if (isInBlastrange == false && hasBeenHit == false) {
            seeker.StartPath(rb2D.position, target.position, OnPathComplete);
            if(target.position.x > rb2D.position.x) {
                transform.localScale = new Vector3(-1, 1, 1);
            } else if (target.position.x < rb2D.position.x) {
                transform.localScale = new Vector3(1, 1, 1);
            }
        } else if (isInBlastrange == true && hasBeenHit == false) {
            Blowup();
        } else {
            return;
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(rb2D.position, target.position) <= blastrange) {
            isInBlastrange = true;
        }

        if (isInBlastrange == false && hasBeenHit == false) {
            if (path == null)
            return;
        
            if (currentWaypoint >= path.vectorPath.Count) {
                reachedEndOfPath = true;
                return;
            } else {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb2D.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb2D.AddForce(force);

            float distance = Vector2.Distance(rb2D.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance) {
                currentWaypoint++;
            }
        }
    }

    void Blowup() {
        animator.SetTrigger("Blow");
        StartCoroutine(DestroySelf(1.05f, 3f));
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Bullet")) {
            hasBeenHit = true;
            rb2D.velocity = Vector3.zero;
            animator.SetTrigger("BlowFast");
            GetComponent<CircleCollider2D>().isTrigger = true;
            StartCoroutine(DestroySelf(0f, 1.95f));
        }
    }

    IEnumerator DestroySelf(float time1, float time2)
    {
        yield return new WaitForSeconds(time1);
        // check if the player is in blastrange and if so, take damage
        if (Vector2.Distance(rb2D.position, target.position) <= blastrange && hasBeenTriggered == false) {
            target.GetComponent<PlayerHandler>().takeDamage(25);
            GetComponent<CircleCollider2D>().isTrigger = true;
            hasBeenTriggered = true;
        }
        yield return new WaitForSeconds(time2);
        Destroy(gameObject);
    }
   
}
