using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFXRotationHandler : MonoBehaviour
{
    public GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, parentObject.transform.rotation.z - 180f);
    }
}
