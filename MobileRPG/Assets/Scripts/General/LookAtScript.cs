using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    public Transform target;
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null) {
            target = GameObject.Find("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // LookAtFunction();
    }

    void LookAtFunction() {
        transform.right = target.position - transform.position;
    }
}
