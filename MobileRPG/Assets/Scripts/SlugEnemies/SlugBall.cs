using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugBall : MonoBehaviour
{
    Rigidbody2D rb2D;
    float thrust = 300f;
    public GameObject smallSlug;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
        rb2D.AddForce(transform.up * thrust);
        InvokeRepeating("SpawnSmallSlug", .8f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSmallSlug() {
        Debug.Log("Small Slug!");
        Instantiate(smallSlug, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
