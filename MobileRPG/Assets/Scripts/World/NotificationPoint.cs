using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPoint : MonoBehaviour
{
    public bool isTutorialNotification;
    public bool hasObjective;
    public GameObject gameManager;
    public Sprite NotificationImage;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Player") {
            gameManager.GetComponent<CanvasHandler>().OpenNotificationScreen(NotificationImage);
            if (isTutorialNotification == true) {
                SetTutorialObjective();
            }
            Destroy(gameObject);
        }
    }

    void SetTutorialObjective() {
        var theTutorialManagerScript = GameObject.Find("TutorialManager").GetComponent<TutorialManagerScript>();
        if (hasObjective == true) {
            if (theTutorialManagerScript.currentObjective == 0) {
                GameObject.Find("CurrenObjectiveHolder").GetComponent<ObjectiveHandler>().SetObjective("Gather some materials");
                GameObject.Find("TutorialManager").GetComponent<TutorialManagerScript>().currentObjective += 1;
            } else if (theTutorialManagerScript.currentObjective == 2){
                GameObject.Find("CurrenObjectiveHolder").GetComponent<ObjectiveHandler>().SetObjective("Craft lantern fuel & ammo");
                GameObject.Find("TutorialManager").GetComponent<TutorialManagerScript>().currentObjective += 1;
            } else if (theTutorialManagerScript.currentObjective == 4){
                GameObject.Find("CurrenObjectiveHolder").GetComponent<ObjectiveHandler>().SetObjective("Add lantern fuel & ammo to loadout");
                GameObject.Find("TutorialManager").GetComponent<TutorialManagerScript>().currentObjective += 1;
            } else if (theTutorialManagerScript.currentObjective == 6){
                GameObject.Find("CurrenObjectiveHolder").GetComponent<ObjectiveHandler>().SetObjective("Destroy dangerous foe");
                GameObject.Find("TutorialManager").GetComponent<TutorialManagerScript>().currentObjective += 1;
            } else if (theTutorialManagerScript.currentObjective == 8){
                GameObject.Find("CurrenObjectiveHolder").GetComponent<ObjectiveHandler>().SetObjective("Activate checkpoint");
                GameObject.Find("TutorialManager").GetComponent<TutorialManagerScript>().currentObjective += 1;
            }

        }
    }
}
