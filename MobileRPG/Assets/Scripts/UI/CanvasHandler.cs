using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasHandler : MonoBehaviour
{
    GameObject UIRoot;
    Canvas MainUI;
    Canvas BagUI;
    Canvas notificationScreen;
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
        MainUI = UIRoot.GetComponent<UIHandler>().MainUI;
        BagUI = UIRoot.GetComponent<UIHandler>().BagUI;
        notificationScreen = UIRoot.GetComponent<UIHandler>().notificationScreen;
    }

    public void OpenBag () {
        Debug.Log("Bag Open!");
        PauseGame(true);
        MainUI.enabled = false;
        BagUI.enabled = true;
    }

    public void CloseBag () {
        Debug.Log("Bag Closed!");
        PauseGame(false);
        BagUI.enabled = false;
        MainUI.enabled = true;
        ClearInvSlots();
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
    public void OpenNotificationScreen(string title, string text, Sprite image) {
        // TMP_Text closeBtn = notificationScreen.transform.Find("CloseButton").GetComponent<TMP_Text>();
        TMP_Text nTitle = notificationScreen.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text nText = notificationScreen.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        Image nImage = notificationScreen.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>();

        CloseBag();
        PauseGame(true);

        notificationScreen.enabled = true;
        nTitle.text = title;
        nText.text = text;
        nImage.sprite = image;
    }

    public void CloseNotificationScreen() {
        notificationScreen.enabled = false;
        PauseGame(false);
    }
}
