using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointHandler : MonoBehaviour
{
    public bool isActiveCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveCheckpoint();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "Player") {
            col.GetComponent<PlayerHandler>().lastCheckPointPosition = transform.position;
            col.GetComponent<PlayerHandler>().SavePlayer();
        }
    }

    void SetActiveCheckpoint() {
        if (transform.position == GameObject.Find("Player").GetComponent<PlayerHandler>().lastCheckPointPosition) {
            isActiveCheckpoint = true;
        } else {
            isActiveCheckpoint = false;
        }
    }
}
