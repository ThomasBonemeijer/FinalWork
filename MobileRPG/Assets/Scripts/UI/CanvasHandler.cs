using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasHandler : MonoBehaviour
{
    public bool hasCompletedLevel;
    GameObject UIRoot;
    Canvas DeathUI;
    Canvas MainUI;
    Canvas BagUI;
    Canvas NotificationUI;
    Canvas ChoiceUI;
    public GameObject cs1;
    public GameObject cs2;

    public GameObject knifeLSlot;
    public GameObject gunLSlot;

    void Start() {
        cs1 = GameObject.Find("UI").GetComponent<UIHandler>().craftingSlot1;
        cs2 = GameObject.Find("UI").GetComponent<UIHandler>().craftingSlot2;
        knifeLSlot = GameObject.Find("UI").GetComponent<UIHandler>().knifeLoadoutSlot;
        gunLSlot = GameObject.Find("UI").GetComponent<UIHandler>().gunLoadoutSlot;
        UIRoot = GameObject.Find("UI");

        DeathUI = UIRoot.GetComponent<UIHandler>().deathUI;
        MainUI = UIRoot.GetComponent<UIHandler>().MainUI;
        BagUI = UIRoot.GetComponent<UIHandler>().BagUI;
        NotificationUI = UIRoot.GetComponent<UIHandler>().NotificationUI;
        ChoiceUI = UIRoot.GetComponent<UIHandler>().ChoiceUI;
    }

    public void CloseAllUI(){
        DeathUI.enabled = false;
        MainUI.enabled = false;
        NotificationUI.enabled = false;
        BagUI.enabled = false;
        ClearInvSlots();
    }

    public void OpenBag () {
        // Debug.Log("Bag Open!");
        PauseGame(true);
        DeathUI.enabled = false;
        MainUI.enabled = false;
        NotificationUI.enabled = false;
        BagUI.enabled = true;
    }

    public void CloseBag () {
        // Debug.Log("Bag Closed!");
        PauseGame(false);
        DeathUI.enabled = false;
        BagUI.enabled = false;
        NotificationUI.enabled = false;
        MainUI.enabled = true;
        ClearInvSlots();
    }

    public void ShowDeathScreen() {
        PauseGame(false);
        DeathUI.enabled = true;
        BagUI.enabled = false;
        NotificationUI.enabled = false;
        MainUI.enabled = false;
        ClearInvSlots();
    }

    public void HideDeathScreen() {
        PauseGame(false);
        DeathUI.enabled = false;
        BagUI.enabled = false;
        NotificationUI.enabled = false;
        MainUI.enabled = true;
    }



    public void PauseGame(bool pause) {
        if (pause == true) {
            Time.timeScale = 0;
        } else if (pause == false) {
            Time.timeScale = 1;
        }
    }

    // Clears the crafting slots when closing the bag
    public void ClearInvSlots() {
        cs1.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
        cs2.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
        BagUI.transform.Find("CraftingInventory").GetComponent<CraftingHandler>().ResetCraftingOutput();
        knifeLSlot.transform.GetChild(0).GetComponent<IconItem>().ClearLoadoutSlot();
        gunLSlot.transform.GetChild(0).GetComponent<IconItem>().ClearLoadoutSlot();
    }

    // Open the notification screen and set the content
    public void OpenNotificationScreen(Sprite NImage) {
        PauseGame(true);
        BagUI.enabled = false;
        MainUI.enabled = false;
        NotificationUI.enabled = true;
        NotificationUI.transform.GetChild(1).GetComponent<Image>().sprite = NImage;
        // Debug.Log("Notificationscreen opened");
    }

    public void CloseNotificationScreen() {
        PauseGame(false);
        NotificationUI.enabled = false;
        MainUI.enabled = true;

        if (hasCompletedLevel == true) {
            GameObject.Find("Player").GetComponent<PlayerHandler>().ResetPlayer();
            GameObject.Find("GenSettings").GetComponent<GenSettingsScript>().LoadScene("MainMenu");
        }
        // Debug.Log("Notificationscreen closed");
    }

    public void OpenChoiceScreen(GameObject theChoiceObject, Sprite theNoteImg) {
        PauseGame(true);
        BagUI.enabled = false;
        MainUI.enabled = false;
        ChoiceUI.enabled = true;
        ChoiceUI.GetComponent<ChoiceScreenHandler>().choiceObject = theChoiceObject;
        ChoiceUI.GetComponent<ChoiceScreenHandler>().backGImage.sprite = theNoteImg;

        // NotificationUI.transform.GetChild(1).GetComponent<Image>().sprite = NImage;
        // Debug.Log("Notificationscreen opened");
    }

    public void CloseChoiceScreen() {
        PauseGame(false);
        ChoiceUI.enabled = false;
        MainUI.enabled = true;
        ChoiceUI.GetComponent<ChoiceScreenHandler>().choiceObject = null;
        // Debug.Log("Notificationscreen closed");
    }

    public void SetHasCompletedLevel() {
        hasCompletedLevel = true;
    }
}
