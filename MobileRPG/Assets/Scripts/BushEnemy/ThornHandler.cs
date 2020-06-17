using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornHandler : MonoBehaviour
{
    float initialXPos;
    Rigidbody2D rb2D;
    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        initialXPos = transform.position.x;
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x > initialXPos) {
            rb2D.rotation -= 3f;
        } else {
            rb2D.rotation += 3f;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Player") {
            col.GetComponent<PlayerHandler>().takeDamage(10);
            Destroy(gameObject);
        }
    }
}
