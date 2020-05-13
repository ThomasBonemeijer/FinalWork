using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("destroySelf", delay, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void destroySelf() {
        Destroy(gameObject);
    }
}
