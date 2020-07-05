using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneChestHandler : MonoBehaviour
{
    GameObject player;
    public GameObject chestItem;
    public Canvas useBtnCanvas;
    public Animator animator;
    public GameObject spawnItem;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (col.name == player.name && animator.GetBool("IsOpen") == false) {
            useBtnCanvas.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (col.name == player.name) {
            useBtnCanvas.enabled = false;
        }
    }

    public void OpenChest() {
        useBtnCanvas.enabled = false;
        animator.SetBool("IsOpen", true);
        StartCoroutine(SpawnTheItem(.5f));
    }

    IEnumerator SpawnTheItem(float time){
        yield return new WaitForSeconds(time);
        var theInstantiation = Instantiate(spawnItem, transform.position, Quaternion.identity);
        theInstantiation.GetComponent<FreshSpawnItemHandler>().theItem = chestItem;
    }
}
