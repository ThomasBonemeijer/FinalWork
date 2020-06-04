using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasHandler : MonoBehaviour
{
    GameObject UIRoot;
    Canvas DeathUI;
    Canvas MainUI;
    Canvas BagUI;
    Canvas NotificationUI;
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
    }

    public void CloseAllUI(){
        DeathUI.enabled = false;
        MainUI.enabled = false;
        NotificationUI.enabled = false;
        BagUI.enabled = false;
        ClearInvSlots();
    }

    public void OpenBag () {
        Debug.Log("Bag Open!");
        PauseGame(true);
        DeathUI.enabled = false;
        MainUI.enabled = false;
        NotificationUI.enabled = false;
        BagUI.enabled = true;
    }

    public void CloseBag () {
        Debug.Log("Bag Closed!");
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
        Debug.Log("Notificationscreen opened");
    }

    public void CloseNotificationScreen() {
        PauseGame(false);
        NotificationUI.enabled = false;
        MainUI.enabled = true;
        Debug.Log("Notificationscreen closed");
    }
}
