using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSlug : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        InvokeRepeating("Blowup", 1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Blowup() {
        animator.SetTrigger("Blow");
    }
}
