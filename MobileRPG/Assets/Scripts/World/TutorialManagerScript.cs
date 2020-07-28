using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerScript : MonoBehaviour
{
    bool hasTreesap;
    bool hasGunpowder;
    public int currentObjective = 0;
    public GameObject tutorialGatherMaterials;
    public GameObject tutorialEnemies;
    public GameObject tutCheckpoint;
    public List<GameObject> obstacleList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObjective == 1) {
            CheckGatheredMaterialsHolder();
        } else if (currentObjective == 3) {
            CheckForItems();
        } else if (currentObjective == 5) {
            CheckForResources();
        } else if (currentObjective == 7) {
            CheckEnemies();
        } else if (currentObjective == 9) {
            CheckCheckpoint();
        }
    }

    void CheckGatheredMaterialsHolder() {
        if (tutorialGatherMaterials.transform.childCount <= 0) {
            SetNextObjective("Continue through the tutorial");
            obstacleList[0].SetActive(false);
        }
    }

    void SetNextObjective(string nextObjective) {
        GameObject.Find("CurrenObjectiveHolder").GetComponent<ObjectiveHandler>().SetObjective(nextObjective);
        currentObjective += 1;
    }

    void CheckForItems() {
        for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i].name == "NormalBullet") {
                    hasGunpowder = true;
                }
                if (Inventory.instance.items[i].name == "TreeSap") {
                    hasTreesap = true;
                }
            }

            if (hasTreesap == true && hasGunpowder == true) {
                SetNextObjective("Continue through the tutorial");
                obstacleList[1].SetActive(false);
            }
    }

    void CheckForResources() {
        var playerResourceScript = GameObject.Find("Player").GetComponent<PlayerResourceHandler>();
        if (playerResourceScript.fuelCount > 0 && playerResourceScript.ammoCount > 0) {
            SetNextObjective("Continue through the tutorial");
            obstacleList[2].SetActive(false);
        }
    }

    void CheckEnemies() {
        if (tutorialEnemies.transform.childCount <= 0) {
            SetNextObjective("Continue through the tutorial");
            obstacleList[3].SetActive(false);
        }
    }

    void CheckCheckpoint() {
        if (tutCheckpoint.GetComponent<CheckPointHandler>().isActiveCheckpoint == true) {
            SetNextObjective("Take the boat to exit the tutorial");
        }
    }
}
