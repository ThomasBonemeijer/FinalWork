using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public GameObject gatheringIndicationsHolder;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject notification1;
    public GameObject notification2;
    public GameObject notification3;
    public List<Item> inventoryItemsList;

    public Item treeSap;
    public Item normalBullet;

    bool hasCompletedGatheringTutorial = false;
    bool hasCompletedCraftingTutorial = false;
    bool hasCompletedLoadoutTutorial = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player").GetComponent<PlayerHandler>().ResetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCompletedGatheringTutorial == false) {
            CheckGatheringTutorial();
        } else {
            if (hasCompletedCraftingTutorial == false) {
                CheckCraftingTutorial();
            } else {
                if (hasCompletedLoadoutTutorial == false) {
                    checkLoadoutTutorial();
                }
            }
        }
    }

    public void CheckGatheringTutorial() {
        if (gatheringIndicationsHolder.transform.childCount == 0) {
            Destroy(obstacle1);
            notification1.SetActive(true);
            hasCompletedGatheringTutorial = true;
        }
    }
    public void CheckCraftingTutorial() {
        inventoryItemsList = GameObject.Find("GameManager").GetComponent<Inventory>().items;
        if (inventoryItemsList.Contains(treeSap) || inventoryItemsList.Contains(normalBullet)) {
            Destroy(obstacle2);
            notification2.SetActive(true);
            hasCompletedCraftingTutorial = true;
        }
    }

    public void checkLoadoutTutorial() {
        if (GameObject.Find("Player").GetComponent<PlayerResourceHandler>().ammoCount != 0 || GameObject.Find("Player").GetComponent<PlayerResourceHandler>().fuelCount != 0) {
            Destroy(obstacle3);
            notification3.SetActive(true);
            hasCompletedLoadoutTutorial = true;
        }
    }
}
