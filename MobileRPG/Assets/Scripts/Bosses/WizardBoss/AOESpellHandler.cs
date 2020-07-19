using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESpellHandler : MonoBehaviour
{
    public Transform thePivotPoint;
    public GameObject theAferEffect;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnAndDestroy", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAndDestroy() {
        Instantiate(theAferEffect, thePivotPoint.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
