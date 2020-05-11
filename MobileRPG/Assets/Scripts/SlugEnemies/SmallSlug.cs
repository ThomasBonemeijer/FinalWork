using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSlug : MonoBehaviour
{
    public float blowUpRange;
    Animator animator;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        animator = transform.GetComponent<Animator>();
        InvokeRepeating("Blowup", 1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > blowUpRange) {

        }
    }

    void Blowup() {
        animator.SetTrigger("Blow");
    }
}
