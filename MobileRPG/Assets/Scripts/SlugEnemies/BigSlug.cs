using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlug : MonoBehaviour
{
    public Transform player;
    public int health = 100;
    public float aggroRange = 10f;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject smallSlugBall;
    Animator animator;
    public bool isMoving = false;
    public string direction;
    Rigidbody2D rb2D;
    public float speed = 3f;
    public bool playerIsInRange = false;
    bool isSpawning = false;
    bool delayedSpawn = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb2D = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("Move", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // CHeck player distance
        if (Vector2.Distance(transform.position, player.position) < aggroRange) {
            playerIsInRange = true;
        } else {
            playerIsInRange = false;
        }

        // Spawn small slugs when player is in aggro range
        if (playerIsInRange == true && isSpawning == false) {
            spawnSmallSlugs();
        }

        // Handle the big slugs movement
        if (isMoving == true && playerIsInRange == false) {
            // Move right
            if (direction == "right") {
                rb2D.AddForce(transform.right * speed, ForceMode2D.Force);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } else if (direction == "left") {
                rb2D.AddForce(transform.right * -speed, ForceMode2D.Force);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    // If player is not in range and the slug is currently not spawning small sluggs: handle the movement and direction og the big slug
    void Move() {
        if (playerIsInRange == false && isSpawning == false) {
            float randomTime = Random.Range(2, 6);
            string[] directions = new string[] {"right", "left"};
            isMoving = !isMoving;
            direction = directions[Random.Range(0, 2)];
            animator.SetBool("IsMoving", isMoving);
            Invoke("Move", randomTime);
        }
    }
 
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Bullet")) {
            Debug.Log("BulletHit!");
            Destroy(col.gameObject);
            health -= 25;
            CheckHealth();
            spawnSmallSlugs();
        }
    }

    void CheckHealth() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    // Handles the big slug spawn animation using the animator and a delayed instantiotion of the slug balls (small slugs)
    void spawnSmallSlugs() {
        animator.SetBool ("IsMoving", false);
        animator.SetTrigger("Spawn");
        StartCoroutine(DelayedSlugspawn(1.05f, 1.5f));
    }

     IEnumerator DelayedSlugspawn(float time, float time2)
    {
        if (delayedSpawn)
            yield break;
    
        delayedSpawn = true;
        isSpawning = true;
    
        yield return new WaitForSeconds(time);
    
        Instantiate(smallSlugBall, spawn1.transform.position, spawn1.transform.rotation);
        Instantiate(smallSlugBall, spawn2.transform.position, spawn2.transform.rotation);

        yield return new WaitForSeconds(time2);
        Debug.Log("End of delay!");
        isSpawning = false;
        delayedSpawn = false;
    }
}
