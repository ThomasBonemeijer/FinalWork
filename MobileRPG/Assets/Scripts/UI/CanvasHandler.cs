using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasHandler : MonoBehaviour
{
    public Canvas MainUI;
    public Canvas BagUI;
    public Canvas notificationScreen;
    public GameObject cs1;
    public GameObject cs2;

    public GameObject knifeLSlot;
    public GameObject gunLSlot;

    public void OpenBag () {
        PauseGame(true);
        MainUI.enabled = false;
        BagUI.enabled = true;
    }

    public void CloseBag () {
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
