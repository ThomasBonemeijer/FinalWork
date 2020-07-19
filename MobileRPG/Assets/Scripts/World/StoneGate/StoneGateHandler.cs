using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGateHandler : MonoBehaviour
{
    public bool hasBeenOpened;
    GameObject player;
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
        Invoke("SetDoorStatus", .01f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetDoorStatus() {
        hasBeenOpened = player.GetComponent<PlayerResourceHandler>().hasOpenedStoneGate;
        animator.SetBool("HasBeenOpened", hasBeenOpened);
    }

    public void CheckGate() {
        if (hasBeenOpened == false) {
            for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i].name == "FullTablet") {
                    Inventory.instance.Remove(Inventory.instance.items[i]);
                    OpenGate();
                    return;
                }
            }
            Debug.Log ("Requires tablet!");
        }
    }

    void OpenGate() {
            leftGate.GetComponent<SpriteRenderer>().sprite = leftGateActiveSprite;
            rightGate.GetComponent<SpriteRenderer>().sprite = rightGateActiveSprite;
            animator.SetBool("IsOpen", true);
            stoneGateCanvas.enabled = false;
            StartCoroutine(DelayedOpenGate(1.5f));
    }

    IEnumerator DelayedOpenGate(float time)
    {
        yield return new WaitForSeconds(time);
    
        player.GetComponent<PlayerResourceHandler>().hasOpenedStoneGate = true;
        player.GetComponent<PlayerHandler>().SavePlayer();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Player" && hasBeenOpened == false) {
            if (animator.GetBool("IsOpen") == false) {
                stoneGateCanvas.enabled = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        if (col.name == "Player" && hasBeenOpened == false) {
            stoneGateCanvas.enabled = false;
        }
    }
}
