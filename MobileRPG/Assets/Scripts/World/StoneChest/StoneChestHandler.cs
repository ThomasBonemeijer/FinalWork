using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneChestHandler : MonoBehaviour
{
    public bool hasBeenOpened;
    GameObject player;
    public GameObject chestItem;
    public Canvas useBtnCanvas;
    public Animator animator;
    public GameObject spawnItem;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Invoke("CheckIfHasBeenOpened",.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckIfHasBeenOpened() {
        if (player.GetComponent<PlayerResourceHandler>().openedChestsList.Contains(gameObject.name)) {
            hasBeenOpened = true;
            animator.SetBool("HasBeenOpened", hasBeenOpened);
        }
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (col.name == player.name && animator.GetBool("IsOpen") == false && hasBeenOpened == false) {
            useBtnCanvas.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (col.name == player.name && hasBeenOpened == false) {
            useBtnCanvas.enabled = false;
        }
    }

    public void OpenChest() {
        if (hasBeenOpened == false) {
            useBtnCanvas.enabled = false;
            animator.SetBool("IsOpen", true);
            player.GetComponent<PlayerResourceHandler>().AddChestnameToList(gameObject.name);
            StartCoroutine(DelayedOpenChest(1f));
            StartCoroutine(SpawnTheItem(.5f));
        }
    }

    IEnumerator DelayedOpenChest(float time)
    {
        yield return new WaitForSeconds(time);
    
        hasBeenOpened = true;
        player.GetComponent<PlayerHandler>().SavePlayer();
        animator.SetBool("HasBeenOpened", hasBeenOpened);
    }

    IEnumerator SpawnTheItem(float time){
        yield return new WaitForSeconds(time);
        var theInstantiation = Instantiate(spawnItem, transform.position, Quaternion.identity);
        theInstantiation.GetComponent<FreshSpawnItemHandler>().theItem = chestItem;
    }
}
