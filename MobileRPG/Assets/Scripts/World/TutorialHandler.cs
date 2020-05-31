using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public GameObject gatheringIndicationsHolder;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject notification1;
    public List<Item> inventoryItemsList;

    public Item treeSap;
    public Item normalBullet;

    bool hasCompletedGatheringTutorial = false;
    bool hasCompletedCraftingTutorial = false;
    bool hasCompletedLoadoutTutorial = false;
    // Start is called before the first frame update
    void Start()
    {

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
            hasCompletedCraftingTutorial = true;
        }
    }

    public void checkLoadoutTutorial() {
        
    }
}
