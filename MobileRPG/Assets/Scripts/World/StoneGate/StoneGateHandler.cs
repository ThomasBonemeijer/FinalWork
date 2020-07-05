using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGateHandler : MonoBehaviour
{
    GameObject player;
    bool leftTabletAdded = false;
    bool rightTabletAdded = false;
    public GameObject leftGate;
    public GameObject rightGate;
    public Sprite leftGateActiveSprite;
    public Sprite rightGateActiveSprite;
    public Sprite leftGateInactiveSprite;
    public Sprite rightGateInactiveSprite;
    public Animator animator;
    public Canvas stoneGateCanvas;
    // Start is called before the first frame update
    void Start()
    {
        stoneGateCanvas.enabled = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckGate() {
        Debug.Log("Checkd!");
        leftGate.GetComponent<SpriteRenderer>().sprite = leftGateActiveSprite;
        rightGate.GetComponent<SpriteRenderer>().sprite = rightGateActiveSprite;
        animator.SetBool("IsOpen", true);
        stoneGateCanvas.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Player") {
            if (animator.GetBool("IsOpen") == false) {
                stoneGateCanvas.enabled = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        if (col.name == "Player") {
            stoneGateCanvas.enabled = false;
        }
    }
}
