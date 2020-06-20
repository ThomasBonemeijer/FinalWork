using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleHandler : MonoBehaviour
{
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Invoke("StopApple", 1f);
    }

    void StopApple() {
        rb2D.gravityScale = 0;
        rb2D.velocity = Vector2.zero;
    }

}
