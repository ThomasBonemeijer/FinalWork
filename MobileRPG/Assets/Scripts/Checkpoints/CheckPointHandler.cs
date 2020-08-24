using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointHandler : MonoBehaviour
{
    public bool isTutorialCheckpoint = false;
    bool isActiveTutorialCheckpoint = false;
    public bool isActiveCheckpoint;
    public GameObject theFire;
    public Transform checkPointPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveCheckpoint();
        SetVisuals();

    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "Player") {
            col.GetComponent<PlayerHandler>().lastCheckPointPosition = checkPointPos.position;
            col.GetComponent<PlayerHandler>().SavePlayer();
        }
    }

    void SetActiveCheckpoint() {
            if (checkPointPos.position == GameObject.Find("Player").GetComponent<PlayerHandler>().lastCheckPointPosition) {
                isActiveCheckpoint = true;
            } else {
                isActiveCheckpoint = false;
            }
    }

    void SetVisuals() {
            if (isActiveCheckpoint == true) {
                theFire.SetActive(true);
            } else {
                theFire.SetActive(false);
        }
    }

    public void ClearCheckpoint() {
        isActiveCheckpoint = false;
        var thePlayerHandlerScript = GameObject.Find("Player").GetComponent<PlayerHandler>();
        if (thePlayerHandlerScript != null) {
            thePlayerHandlerScript.lastCheckPointPosition = new Vector3(0, 0, 0);  
            thePlayerHandlerScript.SavePlayer();
        }
    }
}
