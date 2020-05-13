using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float thrust = 20f;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        InvokeRepeating("DestroyThis", 1.5f, 2f);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Enemy")) {
            BulletHit();
        }
    }

    void BulletHit() {
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void DestroyThis() {
        Destroy(gameObject);
    }
}
