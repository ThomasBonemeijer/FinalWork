using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlug : MonoBehaviour
{
    public int health = 100;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject smallSlugBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void spawnSmallSlugs() {
        Instantiate(smallSlugBall, spawn1.transform.position, spawn1.transform.rotation);
        Instantiate(smallSlugBall, spawn2.transform.position, spawn2.transform.rotation);
    }
}
