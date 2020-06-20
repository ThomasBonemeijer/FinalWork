using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFXHandler : MonoBehaviour
{
    ParticleSystem pfx;
    void Start()
    {
        pfx = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pfx != null) {
            if(!pfx.IsAlive()) {
                Destroy(gameObject);
            }
        }
    }
}
